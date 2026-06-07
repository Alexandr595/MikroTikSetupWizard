namespace MikroTikSetupWizard.Application.Discovery;

public sealed record DeviceDiscoveryResultDto(
    string Identity,
    string? IpAddress,
    string MacAddress,
    string? RouterOsVersion,
    string InterfaceName,
    string DiscoveryMethod,
    bool IsReachableByIp,
    bool IsReachableByMac,
    IReadOnlyList<string> Notes);
