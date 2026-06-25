namespace MikroTikSetupWizard.Application.Connections;

public interface IConnectionManager
{
    Task<DeviceConnectionResult> ConnectAsync(
        DeviceConnectionProfile profile,
        IReadOnlyList<ConnectionTransportAvailabilityDto>? availability = null,
        CancellationToken cancellationToken = default);
}
