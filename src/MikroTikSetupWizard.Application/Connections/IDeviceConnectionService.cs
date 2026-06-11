namespace MikroTikSetupWizard.Application.Connections;

public interface IDeviceConnectionService
{
    Task<DeviceConnectionResultDto> CheckConnectionAsync(
        DeviceConnectionRequestDto request,
        CancellationToken cancellationToken = default);
}
