using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using MikroTikSetupWizard.Application.Diagnostics;

namespace MikroTikSetupWizard.Infrastructure.Diagnostics;

public sealed class DeviceDiagnosticsService : IDeviceDiagnosticsService
{
    private static readonly TimeSpan TcpProbeTimeout = TimeSpan.FromMilliseconds(1500);
    private const int PingTimeoutMilliseconds = 1000;

    public async Task<DeviceDiagnosticsResultDto> DiagnoseAsync(
        DeviceDiagnosticsRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!IPAddress.TryParse(request.IpAddress, out var ipAddress)
            || ipAddress.AddressFamily != AddressFamily.InterNetwork)
        {
            return BuildUnknownResult(request, "IP-адрес отсутствует или имеет неверный формат.");
        }

        var pingTask = ProbePingAsync(ipAddress, cancellationToken);
        var serviceTasks = DeviceDiagnosticsPortCatalog.Ports
            .Select(service => ProbeServiceAsync(ipAddress, service, cancellationToken))
            .ToArray();

        var services = await Task.WhenAll(serviceTasks);
        var ping = await pingTask;
        var recommendations = BuildRecommendations(request, ping, services);

        return new DeviceDiagnosticsResultDto(
            Normalize(request.Identity),
            ipAddress.ToString(),
            Normalize(request.MacAddress),
            Normalize(request.BoardName),
            Normalize(request.RouterOsVersion),
            Normalize(request.DiscoveryMethod),
            ping,
            services,
            recommendations);
    }

    private static async Task<ServiceAvailabilityDto> ProbePingAsync(
        IPAddress ipAddress,
        CancellationToken cancellationToken)
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping
                .SendPingAsync(ipAddress, PingTimeoutMilliseconds)
                .WaitAsync(cancellationToken);

            return reply.Status switch
            {
                IPStatus.Success => new(
                    "Ping",
                    null,
                    ServiceAvailabilityStatus.Open,
                    "Устройство отвечает на ping."),
                IPStatus.TimedOut => new(
                    "Ping",
                    null,
                    ServiceAvailabilityStatus.Timeout,
                    "Ответ на ping не получен за отведённое время."),
                _ => new(
                    "Ping",
                    null,
                    ServiceAvailabilityStatus.Unknown,
                    $"Ping завершён со статусом {reply.Status}.")
            };
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception) when (exception is PingException or InvalidOperationException)
        {
            return new ServiceAvailabilityDto(
                "Ping",
                null,
                ServiceAvailabilityStatus.Unknown,
                "Не удалось выполнить ping.");
        }
    }

    private static async Task<ServiceAvailabilityDto> ProbeServiceAsync(
        IPAddress ipAddress,
        DeviceDiagnosticsPort service,
        CancellationToken cancellationToken)
    {
        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(TcpProbeTimeout);

        try
        {
            using var tcpClient = new TcpClient(ipAddress.AddressFamily);
            await tcpClient.ConnectAsync(ipAddress, service.Port, timeoutSource.Token);

            if (!tcpClient.Connected
                || tcpClient.Client.RemoteEndPoint is not IPEndPoint remoteEndPoint
                || !remoteEndPoint.Address.Equals(ipAddress)
                || remoteEndPoint.Port != service.Port)
            {
                return CreateServiceResult(
                    service,
                    ServiceAvailabilityStatus.Unknown,
                    "Соединение завершилось без подтверждения целевого адреса.");
            }

            return CreateServiceResult(
                service,
                ServiceAvailabilityStatus.Open,
                $"TCP-порт {service.Port} принимает соединение.");
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            return CreateServiceResult(
                service,
                ServiceAvailabilityStatus.Timeout,
                $"TCP-порт {service.Port} не ответил за отведённое время.");
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.ConnectionRefused)
        {
            return CreateServiceResult(
                service,
                ServiceAvailabilityStatus.Closed,
                $"TCP-порт {service.Port} отклонил соединение.");
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.TimedOut)
        {
            return CreateServiceResult(
                service,
                ServiceAvailabilityStatus.Timeout,
                $"TCP-порт {service.Port} не ответил за отведённое время.");
        }
        catch (SocketException)
        {
            return CreateServiceResult(
                service,
                ServiceAvailabilityStatus.Unknown,
                $"Не удалось определить состояние TCP-порта {service.Port}.");
        }
    }

    private static ServiceAvailabilityDto CreateServiceResult(
        DeviceDiagnosticsPort service,
        ServiceAvailabilityStatus status,
        string details)
    {
        return new ServiceAvailabilityDto(service.Name, service.Port, status, details);
    }

    private static DeviceDiagnosticsResultDto BuildUnknownResult(
        DeviceDiagnosticsRequestDto request,
        string reason)
    {
        var unknownServices = DeviceDiagnosticsPortCatalog.Ports
            .Select(service => CreateServiceResult(
                service,
                ServiceAvailabilityStatus.Unknown,
                reason))
            .ToArray();

        return new DeviceDiagnosticsResultDto(
            Normalize(request.Identity),
            Normalize(request.IpAddress),
            Normalize(request.MacAddress),
            Normalize(request.BoardName),
            Normalize(request.RouterOsVersion),
            Normalize(request.DiscoveryMethod),
            new ServiceAvailabilityDto(
                "Ping",
                null,
                ServiceAvailabilityStatus.Unknown,
                reason),
            unknownServices,
            [reason]);
    }

    private static IReadOnlyList<string> BuildRecommendations(
        DeviceDiagnosticsRequestDto request,
        ServiceAvailabilityDto ping,
        IReadOnlyList<ServiceAvailabilityDto> services)
    {
        var recommendations = new List<string>();
        var openServices = services
            .Where(service => service.Status == ServiceAvailabilityStatus.Open)
            .ToArray();

        if (FindService(services, "WinBox").Status == ServiceAvailabilityStatus.Open)
        {
            recommendations.Add("Устройство отвечает по WinBox, можно подключаться через WinBox.");
        }

        AddUnavailableServiceRecommendation(recommendations, FindService(services, "SSH"), "SSH");

        var api = FindService(services, "API");
        var apiSsl = FindService(services, "API-SSL");

        if (api.Status != ServiceAvailabilityStatus.Open
            && apiSsl.Status != ServiceAvailabilityStatus.Open)
        {
            recommendations.Add("API и API-SSL недоступны.");
        }

        if (ping.Status != ServiceAvailabilityStatus.Open && openServices.Length > 0)
        {
            recommendations.Add("Ping не отвечает, но один или несколько TCP-сервисов доступны. Возможно, ICMP ограничен firewall.");
        }

        if (string.Equals(
                request.DiscoveryMethod,
                "NeighborDiscovery",
                StringComparison.OrdinalIgnoreCase)
            && ping.Status != ServiceAvailabilityStatus.Open
            && openServices.Length == 0)
        {
            recommendations.Add("Устройство найдено через MNDP, но IP не отвечает. Возможны другая VLAN, неверный IP или ограничение firewall.");
        }

        if (ping.Status != ServiceAvailabilityStatus.Open
            && openServices.Length == 0)
        {
            recommendations.Add("Проверьте адрес устройства, VLAN, маршрут и правила firewall.");
        }

        if (FindService(services, "HTTP").Status == ServiceAvailabilityStatus.Open)
        {
            recommendations.Add("HTTP доступен. Проверьте, нужен ли незашифрованный web-доступ к устройству.");
        }

        if (recommendations.Count == 0)
        {
            recommendations.Add("Диагностика завершена. Открытые порты сами по себе не подтверждают тип устройства.");
        }

        return recommendations;
    }

    private static void AddUnavailableServiceRecommendation(
        ICollection<string> recommendations,
        ServiceAvailabilityDto service,
        string displayName)
    {
        if (service.Status == ServiceAvailabilityStatus.Closed)
        {
            recommendations.Add($"{displayName} отключён или порт закрыт.");
        }
        else if (service.Status == ServiceAvailabilityStatus.Timeout)
        {
            recommendations.Add($"{displayName} не ответил. Возможна фильтрация firewall.");
        }
    }

    private static ServiceAvailabilityDto FindService(
        IReadOnlyList<ServiceAvailabilityDto> services,
        string name)
    {
        return services.First(service => string.Equals(
            service.Name,
            name,
            StringComparison.Ordinal));
    }

    private static string Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? "Неизвестно" : value.Trim();
    }
}
