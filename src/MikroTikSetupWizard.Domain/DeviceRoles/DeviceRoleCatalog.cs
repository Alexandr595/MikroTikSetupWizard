using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Domain.DeviceRoles;

public static class DeviceRoleCatalog
{
    private static readonly IReadOnlyCollection<ModuleId> AllModules =
    [
        ModuleId.BasicNetwork,
        ModuleId.Nat,
        ModuleId.Firewall,
        ModuleId.Vpn,
        ModuleId.VpnUsers,
        ModuleId.PortForwarding
    ];

    private static readonly IReadOnlyCollection<DeviceRoleDescriptor> Roles =
    [
        new DeviceRoleDescriptor(
            DeviceRole.MainRouter,
            "Main Router",
            "Primary edge router for WAN, LAN, DHCP, NAT and baseline security.",
            new DeviceRolePolicy(
                allowedModules: AllModules,
                defaultEnabledModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Nat,
                    ModuleId.Firewall
                ]),
            [
                new DeviceRoleWarning(
                    "Existing default configuration",
                    "Running the generated script on a router with existing defaults can create duplicate lists, pools or firewall rules."),
                new DeviceRoleWarning(
                    "Administrative access",
                    "Firewall rules must keep management access available from the trusted LAN.")
            ]),

        new DeviceRoleDescriptor(
            DeviceRole.IntermediateRouter,
            "Intermediate Router",
            "Router placed behind another gateway for routed internal segments.",
            new DeviceRolePolicy(
                allowedModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall,
                    ModuleId.Vpn,
                    ModuleId.VpnUsers
                ],
                forbiddenModules:
                [
                    ModuleId.Nat,
                    ModuleId.PortForwarding
                ],
                defaultEnabledModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall
                ]),
            [
                new DeviceRoleWarning(
                    "Upstream routes required",
                    "The main router must know return routes for networks behind this device when NAT is not used."),
                new DeviceRoleWarning(
                    "DHCP conflicts",
                    "Avoid enabling another DHCP server in a network segment that already has one.")
            ]),

        new DeviceRoleDescriptor(
            DeviceRole.AccessPoint,
            "Access Point",
            "Bridge or wireless access point inside an existing routed network.",
            new DeviceRolePolicy(
                allowedModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall
                ],
                forbiddenModules:
                [
                    ModuleId.Nat,
                    ModuleId.Vpn,
                    ModuleId.VpnUsers,
                    ModuleId.PortForwarding
                ],
                defaultEnabledModules:
                [
                    ModuleId.BasicNetwork
                ]),
            [
                new DeviceRoleWarning(
                    "No edge routing",
                    "NAT, port forwarding and a second DHCP server are usually not appropriate for an access point."),
                new DeviceRoleWarning(
                    "Management address",
                    "The management IP must be reachable from the existing main network.")
            ]),

        new DeviceRoleDescriptor(
            DeviceRole.VpnGateway,
            "VPN Gateway",
            "Device that terminates VPN connections and routes clients to internal networks.",
            new DeviceRolePolicy(
                allowedModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall,
                    ModuleId.Vpn,
                    ModuleId.VpnUsers,
                    ModuleId.Nat
                ],
                forbiddenModules:
                [
                    ModuleId.PortForwarding
                ],
                defaultEnabledModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall,
                    ModuleId.Vpn,
                    ModuleId.VpnUsers
                ]),
            [
                new DeviceRoleWarning(
                    "Upstream port forwarding",
                    "If this device is not the main router, the upstream router must forward the VPN port to it."),
                new DeviceRoleWarning(
                    "VPN return routes",
                    "Internal networks need a return route to the VPN subnet unless VPN traffic is masqueraded.")
            ]),

        new DeviceRoleDescriptor(
            DeviceRole.CapsManController,
            "CAPsMAN Controller",
            "Central controller for MikroTik CAP wireless access points.",
            new DeviceRolePolicy(
                allowedModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall,
                    ModuleId.Nat,
                    ModuleId.Vpn,
                    ModuleId.VpnUsers,
                    ModuleId.PortForwarding
                ],
                defaultEnabledModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall
                ]),
            [
                new DeviceRoleWarning(
                    "RouterOS version differences",
                    "RouterOS 6 classic CAPsMAN and RouterOS 7 Wi-Fi use different command trees."),
                new DeviceRoleWarning(
                    "Package support",
                    "CAPsMAN availability depends on installed wireless or wifi packages and device support.")
            ]),

        new DeviceRoleDescriptor(
            DeviceRole.CapClient,
            "CAP Client",
            "Managed access point that receives wireless configuration from a CAPsMAN controller.",
            new DeviceRolePolicy(
                allowedModules:
                [
                    ModuleId.BasicNetwork,
                    ModuleId.Firewall
                ],
                forbiddenModules:
                [
                    ModuleId.Nat,
                    ModuleId.Vpn,
                    ModuleId.VpnUsers,
                    ModuleId.PortForwarding
                ],
                defaultEnabledModules:
                [
                    ModuleId.BasicNetwork
                ]),
            [
                new DeviceRoleWarning(
                    "Controller dependency",
                    "The device must be able to reach its CAPsMAN controller before CAP mode is useful."),
                new DeviceRoleWarning(
                    "Local Wi-Fi settings",
                    "Wireless settings can be overridden by CAPsMAN after the device joins the controller.")
            ])
    ];

    public static IReadOnlyCollection<DeviceRoleDescriptor> GetRoles()
    {
        return Roles;
    }
}
