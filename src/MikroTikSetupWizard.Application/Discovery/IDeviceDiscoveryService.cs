namespace MikroTikSetupWizard.Application.Discovery;

public interface IDeviceDiscoveryService
{
    Task<IReadOnlyList<DeviceDiscoveryResultDto>> FindDevicesAsync(
        CancellationToken cancellationToken = default);
}
