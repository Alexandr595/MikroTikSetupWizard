namespace MikroTikSetupWizard.Application.Connections;

public sealed class DeviceConnectionRequestDto
{
    public required string IpAddress { get; init; }

    public required string Login { get; init; }

    public string Password { get; init; } = string.Empty;

    public DeviceConnectionMethod Method { get; init; } = DeviceConnectionMethod.Ssh;

    public string? ExpectedHostKeyFingerprint { get; init; }

    public override string ToString()
    {
        return $"{Method}: {Login}@{IpAddress}";
    }
}
