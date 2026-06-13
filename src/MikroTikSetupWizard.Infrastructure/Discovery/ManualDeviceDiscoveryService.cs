using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Infrastructure.Discovery;

public sealed class ManualDeviceDiscoveryService : IDeviceManualDiscoveryService
{
    private readonly IDeviceReachabilityService _reachabilityService;

    public ManualDeviceDiscoveryService(IDeviceReachabilityService reachabilityService)
    {
        _reachabilityService = reachabilityService;
    }

    public async Task<DeviceDiscoveryResultDto> DiscoverAsync(
        ManualDeviceDiscoveryRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var ipAddress = request.IpAddress.Trim();

        if (!StrictIpv4AddressParser.TryParse(ipAddress, out var parsedIpAddress))
        {
            throw new ArgumentException(
                "Введите IP в формате 192.168.1.1 без ведущих нулей.",
                nameof(request));
        }

        var device = new DeviceDiscoveryResultDto(
            Identity: "Неизвестно",
            IpAddress: parsedIpAddress.ToString(),
            MacAddress: "Неизвестно",
            RouterOsVersion: "Неизвестно",
            InterfaceName: string.IsNullOrWhiteSpace(request.InterfaceName) ? "Manual" : request.InterfaceName.Trim(),
            DiscoveryMethod: "Manual",
            IsReachableByIp: false,
            IsReachableByMac: false,
            ReachabilityStatus: "Unknown",
            Notes:
            [
                "IP добавлен вручную для безопасной проверки доступности.",
                "Identity, MAC и версия RouterOS неизвестны без MNDP или подключения с авторизацией."
            ]);

        return await _reachabilityService.CheckReachabilityAsync(device, cancellationToken);
    }
}
