namespace MikroTikSetupWizard.Application.Connections;

public sealed record ConnectionTransportAvailabilityDto(
    DeviceConnectionTransport Transport,
    bool IsAvailable,
    string? Reason = null);
