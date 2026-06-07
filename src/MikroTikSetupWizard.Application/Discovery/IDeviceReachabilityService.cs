namespace MikroTikSetupWizard.Application.Discovery;

public interface IDeviceReachabilityService
{
    Task<DeviceDiscoveryResultDto> CheckReachabilityAsync(
        DeviceDiscoveryResultDto device,
        CancellationToken cancellationToken = default);
}
