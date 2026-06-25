namespace MikroTikSetupWizard.Application.ConfigurationApply;

public sealed record DeviceConnectionProfile(
    string IpAddress,
    string Login,
    string? Password,
    ConfigurationApplyTransport Transport,
    bool AllowInsecureLocalApi = false,
    string? ExpectedDeviceIdentity = null,
    string? ExpectedMacAddress = null);
