namespace MikroTikSetupWizard.Application.Connections;

public sealed record DeviceConnectionResultDto(
    DeviceConnectionStatus Status,
    string Message,
    DeviceInfoDto? DeviceInfo)
{
    public bool IsSuccess => Status is DeviceConnectionStatus.Success
        or DeviceConnectionStatus.PartialSuccess;
}
