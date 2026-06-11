namespace MikroTikSetupWizard.Application.Connections;

public sealed record DeviceConnectionResultDto(
    DeviceConnectionStatus Status,
    string Message,
    DeviceInfoDto? DeviceInfo,
    string? HostKeyFingerprint = null,
    string? HostKeyAlgorithm = null)
{
    public bool IsSuccess => Status is DeviceConnectionStatus.Success
        or DeviceConnectionStatus.PartialSuccess;
}
