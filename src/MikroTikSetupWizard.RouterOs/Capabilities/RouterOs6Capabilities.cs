using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Capabilities;

public sealed record RouterOs6Capabilities()
    : RouterOsCapabilities(
        RouterOsMajorVersion.V6,
        SupportsInterfaceLists: true,
        SupportsOutInterfaceListNat: true);
