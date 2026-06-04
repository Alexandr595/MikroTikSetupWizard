namespace MikroTikSetupWizard.Application.Setup;

public sealed class BasicSetupInputDto
{
    public string RouterName { get; init; } = string.Empty;

    public string RouterOsVersion { get; init; } = string.Empty;

    public string WanInterface { get; init; } = string.Empty;

    public string LanBridgeName { get; init; } = string.Empty;

    public string LanAddress { get; init; } = string.Empty;

    public int LanPrefixLength { get; init; }

    public string DhcpPoolStart { get; init; } = string.Empty;

    public string DhcpPoolEnd { get; init; } = string.Empty;

    public string DnsServers { get; init; } = string.Empty;

    public string AdminUserName { get; init; } = string.Empty;

    public string AdminPassword { get; init; } = string.Empty;

    public bool EnableNat { get; init; }

    public bool EnableBasicFirewall { get; init; }
}
