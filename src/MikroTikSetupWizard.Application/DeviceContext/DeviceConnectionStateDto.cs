using MikroTikSetupWizard.Application.Connections;

namespace MikroTikSetupWizard.Application.DeviceContext;

public sealed record DeviceConnectionStateDto(
    DeviceConnectionTransport TransportUsed,
    DeviceConnectionManagerStatus Status,
    string Message,
    IReadOnlyList<string> Warnings,
    DateTimeOffset LastCheckedAt);
