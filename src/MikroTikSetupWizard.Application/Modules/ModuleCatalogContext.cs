using MikroTikSetupWizard.Domain.DeviceRoles;
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Application.Modules;

public sealed class ModuleCatalogContext
{
    public ModuleCatalogContext(
        DeviceRole deviceRole,
        RouterOsMajorVersion? routerOsVersion = null,
        bool advancedMode = false)
    {
        DeviceRole = deviceRole;
        RouterOsVersion = routerOsVersion;
        AdvancedMode = advancedMode;
    }

    public DeviceRole DeviceRole { get; }

    public RouterOsMajorVersion? RouterOsVersion { get; }

    public bool AdvancedMode { get; }
}
