namespace MikroTikSetupWizard.Application.Connections;

public sealed record DeviceConnectionResult(
    DeviceConnectionTransport TransportUsed,
    DeviceConnectionManagerStatus Status,
    IReadOnlyList<string> Warnings,
    DeviceInfoDto? DeviceInfo,
    string Message)
{
    public bool IsSuccess => Status == DeviceConnectionManagerStatus.Success;
}
