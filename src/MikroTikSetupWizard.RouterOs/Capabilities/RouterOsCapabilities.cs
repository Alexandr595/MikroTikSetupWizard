using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Capabilities;

public abstract record RouterOsCapabilities(
    RouterOsMajorVersion Version,
    bool SupportsInterfaceLists,
    bool SupportsOutInterfaceListNat)
{
    public static RouterOsCapabilities For(RouterOsMajorVersion version)
    {
        return version switch
        {
            RouterOsMajorVersion.V6 => new RouterOs6Capabilities(),
            RouterOsMajorVersion.V7 => new RouterOs7Capabilities(),
            _ => throw new ArgumentOutOfRangeException(nameof(version), version, "Unsupported RouterOS version.")
        };
    }
}
