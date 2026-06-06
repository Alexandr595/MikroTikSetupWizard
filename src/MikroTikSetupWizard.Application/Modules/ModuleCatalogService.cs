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
}
