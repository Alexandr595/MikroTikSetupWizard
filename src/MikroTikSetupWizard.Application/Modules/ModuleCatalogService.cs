using MikroTikSetupWizard.Domain.DeviceRoles;
using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Application.Modules;

public sealed class ModuleCatalogService : IModuleCatalogService
{
    private static readonly IReadOnlyCollection<ModuleDescriptor> Modules =
    [
        new ModuleDescriptor(
            ModuleId.BasicNetwork,
            "Basic Network",
            "Initial LAN, WAN, DHCP and DNS setup."),

        new ModuleDescriptor(
            ModuleId.Nat,
            "NAT",
            "Internet access through source NAT masquerade.",
            [
                new ModuleDependency(
                    ModuleId.BasicNetwork,
                    "NAT requires WAN interface list from Basic Network.")
            ]),

        new ModuleDescriptor(
            ModuleId.Firewall,
            "Firewall",
            "Baseline input and security rules.",
            [
                new ModuleDependency(
                    ModuleId.BasicNetwork,
                    "Firewall requires LAN and WAN interface lists from Basic Network.")
            ]),

        new ModuleDescriptor(
            ModuleId.Vpn,
            "VPN",
            "Remote access VPN foundation.",
            [
                new ModuleDependency(
                    ModuleId.BasicNetwork,
                    "VPN requires LAN network information from Basic Network.")
            ]),

        new ModuleDescriptor(
            ModuleId.VpnUsers,
            "VPN Users",
            "User and peer definitions for the selected VPN.",
            [
                new ModuleDependency(
                    ModuleId.Vpn,
                    "VPN users require an enabled VPN module.")
            ]),

        new ModuleDescriptor(
            ModuleId.PortForwarding,
            "Port Forwarding",
            "Destination NAT rules for publishing internal services.",
            [
                new ModuleDependency(
                    ModuleId.BasicNetwork,
                    "Port forwarding requires LAN and WAN information from Basic Network."),
                new ModuleDependency(
                    ModuleId.Nat,
                    "Port forwarding is configured in the NAT rule set.")
            ])
    ];

    public IReadOnlyCollection<ModuleDescriptor> GetModules()
    {
        return Modules;
    }

    public IReadOnlyCollection<ModuleCatalogItem> GetModules(ModuleCatalogContext context)
    {
        var role = DeviceRoleCatalog
            .GetRoles()
            .FirstOrDefault(descriptor => descriptor.Role == context.DeviceRole);

        if (role is null)
        {
            return Modules
                .Select(module => new ModuleCatalogItem(
                    module,
                    isAllowed: false,
                    isDefaultEnabled: false,
                    disabledReason: "Device role is not registered."))
                .ToArray();
        }

        return Modules
            .Select(module => BuildCatalogItem(module, role, context.AdvancedMode))
            .ToArray();
    }

    private static ModuleCatalogItem BuildCatalogItem(
        ModuleDescriptor module,
        DeviceRoleDescriptor role,
        bool advancedMode)
    {
        var policy = role.Policy;
        var forbidden = policy.ForbiddenModules.Contains(module.ModuleId);
        var explicitlyAllowed = policy.AllowedModules.Contains(module.ModuleId);
        var allowed = !forbidden && explicitlyAllowed;
        var defaultEnabled = allowed && policy.DefaultEnabledModules.Contains(module.ModuleId);

        return new ModuleCatalogItem(
            module,
            allowed,
            defaultEnabled,
            BuildDisabledReason(module, role, forbidden, explicitlyAllowed),
            BuildWarning(module, role, forbidden, explicitlyAllowed, advancedMode));
    }

    private static string? BuildDisabledReason(
        ModuleDescriptor module,
        DeviceRoleDescriptor role,
        bool forbidden,
        bool explicitlyAllowed)
    {
        if (forbidden)
        {
            return $"{module.Name} is not available for the {role.Name} role.";
        }

        if (!explicitlyAllowed)
        {
            return $"{module.Name} is not included in the {role.Name} role catalog.";
        }

        return null;
    }

    private static string? BuildWarning(
        ModuleDescriptor module,
        DeviceRoleDescriptor role,
        bool forbidden,
        bool explicitlyAllowed,
        bool advancedMode)
    {
        if (forbidden && advancedMode)
        {
            return $"{module.Name} is normally forbidden for {role.Name}. Use this role with care.";
        }

        if (!explicitlyAllowed && advancedMode)
        {
            return $"{module.Name} is outside the default catalog for {role.Name}.";
        }

        return null;
    }
}
