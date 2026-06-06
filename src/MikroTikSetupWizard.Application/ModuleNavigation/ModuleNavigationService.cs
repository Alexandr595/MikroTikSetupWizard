using MikroTikSetupWizard.Application.DeviceRoles;
using MikroTikSetupWizard.Application.Modules;
using MikroTikSetupWizard.Domain.DeviceRoles;
using MikroTikSetupWizard.Domain.Modules;
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Application.ModuleNavigation;

public sealed class ModuleNavigationService : IModuleNavigationService
{
    private readonly IDeviceRoleCatalogService _deviceRoleCatalogService;
    private readonly IModuleCatalogService _moduleCatalogService;

    public ModuleNavigationService()
        : this(
            new DeviceRoleCatalogService(),
            new ModuleCatalogService())
    {
    }

    public ModuleNavigationService(
        IDeviceRoleCatalogService deviceRoleCatalogService,
        IModuleCatalogService moduleCatalogService)
    {
        _deviceRoleCatalogService = deviceRoleCatalogService;
        _moduleCatalogService = moduleCatalogService;
    }

    public IReadOnlyCollection<DeviceRoleOptionDto> GetDeviceRoles()
    {
        return _deviceRoleCatalogService
            .GetRoles()
            .Select(role => new DeviceRoleOptionDto(
                role.Role.ToString(),
                role.Name,
                role.Description))
            .ToArray();
    }

    public IReadOnlyCollection<ModuleNavigationItemDto> GetModules(
        string deviceRoleId,
        string? routerOsVersion,
        bool advancedMode = false)
    {
        var role = ParseDeviceRole(deviceRoleId);
        var version = ParseRouterOsVersion(routerOsVersion);
        var context = new ModuleCatalogContext(role, version, advancedMode);

        return _moduleCatalogService
            .GetModules(context)
            .Select(ToDto)
            .ToArray();
    }

    private static ModuleNavigationItemDto ToDto(ModuleCatalogItem item)
    {
        return new ModuleNavigationItemDto(
            item.Descriptor.ModuleId.ToString(),
            item.Descriptor.Name,
            item.Descriptor.Description,
            GetStatus(item),
            item.IsAllowed,
            item.IsDefaultEnabled,
            item.DisabledReason,
            item.Warning);
    }

    private static string GetStatus(ModuleCatalogItem item)
    {
        if (!item.IsAllowed)
        {
            return "locked";
        }

        if (!string.IsNullOrWhiteSpace(item.Warning))
        {
            return "warning";
        }

        return item.IsDefaultEnabled ? "enabled" : "disabled";
    }

    private static DeviceRole ParseDeviceRole(string deviceRoleId)
    {
        return Enum.TryParse<DeviceRole>(deviceRoleId, ignoreCase: true, out var role)
            ? role
            : DeviceRole.MainRouter;
    }

    private static RouterOsMajorVersion? ParseRouterOsVersion(string? routerOsVersion)
    {
        if (string.IsNullOrWhiteSpace(routerOsVersion))
        {
            return null;
        }

        return routerOsVersion.Contains('6')
            ? RouterOsMajorVersion.V6
            : RouterOsMajorVersion.V7;
    }
}
