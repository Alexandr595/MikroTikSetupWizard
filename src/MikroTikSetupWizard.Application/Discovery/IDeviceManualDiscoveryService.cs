namespace MikroTikSetupWizard.Application.Discovery;

public interface IDeviceManualDiscoveryService
{
    Task<DeviceDiscoveryResultDto> DiscoverAsync(
        ManualDeviceDiscoveryRequestDto request,
        CancellationToken cancellationToken = default);
}
