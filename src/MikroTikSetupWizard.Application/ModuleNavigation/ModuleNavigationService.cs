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
                GetRoleName(role.Role),
                GetRoleDescription(role.Role)))
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
            GetModuleName(item.Descriptor.ModuleId),
            GetModuleDescription(item.Descriptor.ModuleId),
            GetStatus(item),
            item.IsAllowed,
            item.IsDefaultEnabled,
            LocalizeDisabledReason(item),
            LocalizeWarning(item));
    }

    private static string GetStatus(ModuleCatalogItem item)
    {
        if (!item.IsAllowed)
        {
            return "Заблокирован";
        }

        if (!string.IsNullOrWhiteSpace(item.Warning))
        {
            return "Предупреждение";
        }

        return item.IsDefaultEnabled ? "Включён" : "Отключён";
    }

    private static string GetRoleName(DeviceRole role)
    {
        return role switch
        {
            DeviceRole.MainRouter => "Главный роутер",
            DeviceRole.IntermediateRouter => "Промежуточный роутер",
            DeviceRole.AccessPoint => "Точка доступа",
            DeviceRole.VpnGateway => "VPN-шлюз",
            DeviceRole.CapsManController => "CAPsMAN-контроллер",
            DeviceRole.CapClient => "CAP-клиент",
            _ => role.ToString()
        };
    }

    private static string GetRoleDescription(DeviceRole role)
    {
        return role switch
        {
            DeviceRole.MainRouter => "Основной шлюз сети: WAN, LAN, NAT, DHCP и базовая защита.",
            DeviceRole.IntermediateRouter => "Маршрутизатор внутри существующей сети.",
            DeviceRole.AccessPoint => "Точка доступа или мост без роли главного шлюза.",
            DeviceRole.VpnGateway => "Устройство для подключения VPN-клиентов к сети.",
            DeviceRole.CapsManController => "Центральный контроллер MikroTik CAP-точек.",
            DeviceRole.CapClient => "Управляемая точка доступа CAPsMAN.",
            _ => string.Empty
        };
    }

    private static string GetModuleName(ModuleId moduleId)
    {
        if (moduleId == ModuleId.BasicNetwork)
        {
            return "Базовая сеть";
        }

        if (moduleId == ModuleId.Nat)
        {
            return "NAT";
        }

        if (moduleId == ModuleId.Firewall)
        {
            return "Firewall";
        }

        if (moduleId == ModuleId.Vpn)
        {
            return "VPN";
        }

        if (moduleId == ModuleId.VpnUsers)
        {
            return "VPN-пользователи";
        }

        if (moduleId == ModuleId.PortForwarding)
        {
            return "Проброс портов";
        }

        return moduleId.ToString();
    }

    private static string GetModuleDescription(ModuleId moduleId)
    {
        if (moduleId == ModuleId.BasicNetwork)
        {
            return "LAN, WAN, DHCP и DNS.";
        }

        if (moduleId == ModuleId.Nat)
        {
            return "Выход в интернет через masquerade.";
        }

        if (moduleId == ModuleId.Firewall)
        {
            return "Базовая защита и доступ к управлению.";
        }

        if (moduleId == ModuleId.Vpn)
        {
            return "Основа удалённого доступа.";
        }

        if (moduleId == ModuleId.VpnUsers)
        {
            return "Учётные записи и peers VPN.";
        }

        if (moduleId == ModuleId.PortForwarding)
        {
            return "Публикация внутренних сервисов.";
        }

        return string.Empty;
    }

    private static string? LocalizeDisabledReason(ModuleCatalogItem item)
    {
        return string.IsNullOrWhiteSpace(item.DisabledReason)
            ? null
            : "Недоступно для выбранной роли.";
    }

    private static string? LocalizeWarning(ModuleCatalogItem item)
    {
        return string.IsNullOrWhiteSpace(item.Warning)
            ? null
            : "Требует осторожности для выбранной роли.";
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
