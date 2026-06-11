using System.Net.Sockets;
using MikroTikSetupWizard.Application.Connections;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace MikroTikSetupWizard.Infrastructure.Ssh;

public sealed class SshDeviceConnectionService : IDeviceConnectionService
{
    private static readonly TimeSpan ConnectionTimeout = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan CommandTimeout = TimeSpan.FromSeconds(5);
    private const int SshPort = 22;

    public async Task<DeviceConnectionResultDto> CheckConnectionAsync(
        DeviceConnectionRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Method != DeviceConnectionMethod.Ssh)
        {
            return CreateResult(
                DeviceConnectionStatus.ProtocolError,
                "Поддерживается только SSH read-only.");
        }

        if (string.IsNullOrWhiteSpace(request.IpAddress)
            || string.IsNullOrWhiteSpace(request.Login))
        {
            return CreateResult(
                DeviceConnectionStatus.ProtocolError,
                "Укажите IP-адрес и логин.");
        }

        string? receivedFingerprint = null;
        string? receivedAlgorithm = null;
        var expectedFingerprint = NormalizeFingerprint(request.ExpectedHostKeyFingerprint);
        var hostKeyMismatch = false;

        using var connectionInfo = new PasswordConnectionInfo(
            request.IpAddress.Trim(),
            SshPort,
            request.Login.Trim(),
            request.Password)
        {
            Timeout = ConnectionTimeout
        };
        using var client = new SshClient(connectionInfo);

        client.HostKeyReceived += (_, eventArgs) =>
        {
            receivedFingerprint = NormalizeFingerprint(eventArgs.FingerPrintSHA256);
            receivedAlgorithm = eventArgs.HostKeyName;

            if (string.IsNullOrWhiteSpace(expectedFingerprint))
            {
                eventArgs.CanTrust = false;
                return;
            }

            hostKeyMismatch = !string.Equals(
                expectedFingerprint,
                receivedFingerprint,
                StringComparison.Ordinal);
            eventArgs.CanTrust = !hostKeyMismatch;
        };

        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(ConnectionTimeout);

        try
        {
            await client.ConnectAsync(timeoutSource.Token);

            if (!client.IsConnected)
            {
                return CreateResult(
                    DeviceConnectionStatus.ProtocolError,
                    "SSH-соединение не было установлено.",
                    receivedFingerprint,
                    receivedAlgorithm);
            }

            var result = await ReadDeviceInfoAsync(
                client,
                receivedFingerprint,
                receivedAlgorithm,
                cancellationToken);
            client.Disconnect();

            return result;
        }
        catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
        {
            if (!string.IsNullOrWhiteSpace(receivedFingerprint))
            {
                if (hostKeyMismatch)
                {
                    return CreateResult(
                        DeviceConnectionStatus.HostKeyMismatch,
                        "Ключ SSH-сервера не совпадает с подтверждённым fingerprint. Подключение остановлено.",
                        receivedFingerprint,
                        receivedAlgorithm);
                }

                if (string.IsNullOrWhiteSpace(expectedFingerprint))
                {
                    return CreateResult(
                        DeviceConnectionStatus.HostKeyConfirmationRequired,
                        "Подтвердите fingerprint SSH host key перед отправкой пароля.",
                        receivedFingerprint,
                        receivedAlgorithm);
                }
            }

            return MapConnectionError(
                exception,
                timeoutSource.IsCancellationRequested,
                receivedFingerprint,
                receivedAlgorithm);
        }
    }

    private static async Task<DeviceConnectionResultDto> ReadDeviceInfoAsync(
        SshClient client,
        string? fingerprint,
        string? algorithm,
        CancellationToken cancellationToken)
    {
        var identityCommand = await ExecuteReadOnlyCommandAsync(
            client,
            RouterOsReadOnlyCommandCatalog.Identity,
            cancellationToken);

        if (!identityCommand.IsSuccess)
        {
            return CreateRequiredCommandFailure(
                identityCommand,
                "Не удалось прочитать identity устройства.",
                fingerprint,
                algorithm);
        }

        var resourceCommand = await ExecuteReadOnlyCommandAsync(
            client,
            RouterOsReadOnlyCommandCatalog.Resource,
            cancellationToken);

        if (!resourceCommand.IsSuccess)
        {
            return CreateRequiredCommandFailure(
                resourceCommand,
                "Не удалось прочитать сведения RouterOS.",
                fingerprint,
                algorithm);
        }

        var identity = RouterOsSshOutputParser.ParseIdentity(identityCommand.Output);
        var resource = RouterOsSshOutputParser.ParseResource(resourceCommand.Output);

        if (string.IsNullOrWhiteSpace(identity)
            || string.IsNullOrWhiteSpace(resource.Version))
        {
            return CreateResult(
                DeviceConnectionStatus.ProtocolError,
                "RouterOS вернул неполные обязательные сведения об устройстве.",
                fingerprint,
                algorithm);
        }

        var warnings = new List<string>();
        var boardName = resource.BoardName;
        IReadOnlyList<DeviceInterfaceDto> interfaces = [];

        var routerBoardCommand = await ExecuteReadOnlyCommandAsync(
            client,
            RouterOsReadOnlyCommandCatalog.RouterBoard,
            cancellationToken);

        if (routerBoardCommand.IsSuccess)
        {
            boardName = RouterOsSshOutputParser.ParseBoardName(routerBoardCommand.Output)
                ?? boardName;

            if (string.IsNullOrWhiteSpace(boardName))
            {
                warnings.Add("Модель RouterBOARD не распознана.");
            }
        }
        else
        {
            warnings.Add(routerBoardCommand.IsPermissionDenied
                ? "Недостаточно прав для чтения RouterBOARD."
                : "Сведения RouterBOARD недоступны.");
        }

        var interfacesCommand = await ExecuteReadOnlyCommandAsync(
            client,
            RouterOsReadOnlyCommandCatalog.Interfaces,
            cancellationToken);

        if (interfacesCommand.IsSuccess)
        {
            interfaces = RouterOsSshOutputParser.ParseInterfaces(interfacesCommand.Output);

            if (interfaces.Count == 0)
            {
                warnings.Add("Список интерфейсов не удалось распознать.");
            }
        }
        else
        {
            warnings.Add(interfacesCommand.IsPermissionDenied
                ? "Недостаточно прав для чтения интерфейсов."
                : "Список интерфейсов недоступен.");
        }

        var status = warnings.Count == 0
            ? DeviceConnectionStatus.Success
            : DeviceConnectionStatus.PartialSuccess;
        var message = warnings.Count == 0
            ? "SSH-подключение успешно. Информация RouterOS прочитана в режиме read-only."
            : $"SSH-подключение успешно. Получены частичные данные: {string.Join(" ", warnings)}";
        var deviceInfo = new DeviceInfoDto(
            Identity: identity,
            RouterOsVersion: resource.Version,
            BoardName: string.IsNullOrWhiteSpace(boardName) ? "Неизвестно" : boardName,
            Uptime: resource.Uptime,
            Interfaces: interfaces);

        return new DeviceConnectionResultDto(
            status,
            message,
            deviceInfo,
            HostKeyFingerprint: FormatFingerprint(fingerprint),
            HostKeyAlgorithm: algorithm);
    }

    private static async Task<ReadOnlyCommandResult> ExecuteReadOnlyCommandAsync(
        SshClient client,
        string commandText,
        CancellationToken cancellationToken)
    {
        try
        {
            using var command = client.CreateCommand(commandText);
            command.CommandTimeout = CommandTimeout;
            await command.ExecuteAsync(cancellationToken);

            var output = command.Result ?? string.Empty;
            var error = command.Error ?? string.Empty;
            var isPermissionDenied = ContainsPermissionDenied(output)
                || ContainsPermissionDenied(error);
            var hasFailed = isPermissionDenied
                || command.ExitStatus is not null and not 0
                || !string.IsNullOrWhiteSpace(error);

            return new ReadOnlyCommandResult(
                IsSuccess: !hasFailed,
                IsPermissionDenied: isPermissionDenied,
                IsTimedOut: false,
                Output: output);
        }
        catch (SshOperationTimeoutException)
        {
            return new ReadOnlyCommandResult(false, false, true, string.Empty);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (SshException)
        {
            return new ReadOnlyCommandResult(false, false, false, string.Empty);
        }
    }

    private static DeviceConnectionResultDto CreateRequiredCommandFailure(
        ReadOnlyCommandResult commandResult,
        string message,
        string? fingerprint,
        string? algorithm)
    {
        return CreateResult(
            commandResult.IsTimedOut
                ? DeviceConnectionStatus.Timeout
                : commandResult.IsPermissionDenied
                    ? DeviceConnectionStatus.PermissionDenied
                    : DeviceConnectionStatus.ProtocolError,
            commandResult.IsTimedOut
                ? "Превышено время ожидания read-only команды RouterOS."
                : commandResult.IsPermissionDenied
                    ? "Недостаточно прав для чтения обязательных сведений RouterOS."
                    : message,
            fingerprint,
            algorithm);
    }

    private static bool ContainsPermissionDenied(string value)
    {
        return value.Contains("permission denied", StringComparison.OrdinalIgnoreCase)
            || value.Contains("not enough permissions", StringComparison.OrdinalIgnoreCase)
            || value.Contains("not permitted", StringComparison.OrdinalIgnoreCase);
    }

    private static DeviceConnectionResultDto MapConnectionError(
        Exception exception,
        bool timedOut,
        string? fingerprint,
        string? algorithm)
    {
        if (timedOut || exception is OperationCanceledException)
        {
            return CreateResult(
                DeviceConnectionStatus.Timeout,
                "Превышено время ожидания SSH-подключения.",
                fingerprint,
                algorithm);
        }

        if (exception is SshAuthenticationException)
        {
            return CreateResult(
                DeviceConnectionStatus.InvalidCredentials,
                "Неверный логин или пароль.",
                fingerprint,
                algorithm);
        }

        var socketException = FindSocketException(exception);

        if (socketException?.SocketErrorCode == SocketError.ConnectionRefused)
        {
            return CreateResult(
                DeviceConnectionStatus.PortClosed,
                "SSH-порт 22 закрыт или сервис SSH отключён.",
                fingerprint,
                algorithm);
        }

        if (socketException is not null)
        {
            return CreateResult(
                DeviceConnectionStatus.Unreachable,
                "Устройство недоступно по SSH.",
                fingerprint,
                algorithm);
        }

        if (exception is SshConnectionException)
        {
            return CreateResult(
                DeviceConnectionStatus.ProtocolError,
                "SSH-соединение завершилось ошибкой протокола.",
                fingerprint,
                algorithm);
        }

        return CreateResult(
            DeviceConnectionStatus.ProtocolError,
            "Не удалось установить SSH-соединение.",
            fingerprint,
            algorithm);
    }

    private static SocketException? FindSocketException(Exception exception)
    {
        for (var current = exception; current is not null; current = current.InnerException!)
        {
            if (current is SocketException socketException)
            {
                return socketException;
            }

            if (current.InnerException is null)
            {
                break;
            }
        }

        return null;
    }

    private static DeviceConnectionResultDto CreateResult(
        DeviceConnectionStatus status,
        string message,
        string? fingerprint = null,
        string? algorithm = null)
    {
        return new DeviceConnectionResultDto(
            status,
            message,
            DeviceInfo: null,
            HostKeyFingerprint: FormatFingerprint(fingerprint),
            HostKeyAlgorithm: algorithm);
    }

    private static string? NormalizeFingerprint(string? fingerprint)
    {
        if (string.IsNullOrWhiteSpace(fingerprint))
        {
            return null;
        }

        return fingerprint.Trim()
            .Replace("SHA256:", string.Empty, StringComparison.OrdinalIgnoreCase)
            .TrimEnd('=');
    }

    private static string? FormatFingerprint(string? fingerprint)
    {
        var normalizedFingerprint = NormalizeFingerprint(fingerprint);
        return normalizedFingerprint is null ? null : $"SHA256:{normalizedFingerprint}";
    }

    private sealed record ReadOnlyCommandResult(
        bool IsSuccess,
        bool IsPermissionDenied,
        bool IsTimedOut,
        string Output);
}
