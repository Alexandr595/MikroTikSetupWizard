namespace MikroTikSetupWizard.Application.Connections;

public sealed class DeviceConnectionProfile
{
    public required string IpAddress { get; init; }

    public string Login { get; init; } = "admin";

    public string Password { get; init; } = string.Empty;

    public DeviceConnectionTransport Transport { get; init; } = DeviceConnectionTransport.Unknown;

    public bool AllowInsecureTransport { get; init; }

    public string? ExpectedIdentity { get; init; }

    public string? ExpectedMac { get; init; }
}
