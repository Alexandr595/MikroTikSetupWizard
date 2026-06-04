using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Capabilities;

public sealed record RouterOs7Capabilities()
    : RouterOsCapabilities(
        RouterOsMajorVersion.V7,
        SupportsInterfaceLists: true,
        SupportsOutInterfaceListNat: true);
