using System.Net.Sockets;
using MikroTikSetupWizard.Application.Connections;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace MikroTikSetupWizard.Infrastructure.Ssh;

public sealed class SshDeviceConnectionService : IDeviceConnectionService
{
    private static readonly TimeSpan ConnectionTimeout = TimeSpan.FromSeconds(5);
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

            client.Disconnect();

            return CreateResult(
                DeviceConnectionStatus.Success,
                "Host key confirmed, read-only commands will be added next.",
                receivedFingerprint,
                receivedAlgorithm);
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
}
