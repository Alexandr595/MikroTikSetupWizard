namespace MikroTikSetupWizard.Domain.Discovery;

public sealed class DiscoveredMikroTikDevice
{
    public DiscoveredMikroTikDevice(
        string identity,
        string? ipAddress,
        string macAddress,
        string? routerOsVersion,
        string interfaceName,
        DiscoveryMethod discoveryMethod,
        bool isReachableByIp,
        bool isReachableByMac,
        IReadOnlyCollection<string>? notes = null)
    {
        Identity = identity;
        IpAddress = ipAddress;
        MacAddress = macAddress;
        RouterOsVersion = routerOsVersion;
        InterfaceName = interfaceName;
        DiscoveryMethod = discoveryMethod;
        IsReachableByIp = isReachableByIp;
        IsReachableByMac = isReachableByMac;
        Notes = notes ?? Array.Empty<string>();
    }

    public string Identity { get; }

    public string? IpAddress { get; }

    public string MacAddress { get; }

    public string? RouterOsVersion { get; }

    public string InterfaceName { get; }

    public DiscoveryMethod DiscoveryMethod { get; }

    public bool IsReachableByIp { get; }

    public bool IsReachableByMac { get; }

    public IReadOnlyCollection<string> Notes { get; }
}
