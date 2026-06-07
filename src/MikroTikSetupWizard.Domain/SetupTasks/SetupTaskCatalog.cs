using MikroTikSetupWizard.Domain.DeviceRoles;
using MikroTikSetupWizard.Domain.Modules;
using MikroTikSetupWizard.Domain.Wizard;

namespace MikroTikSetupWizard.Domain.SetupTasks;

public static class SetupTaskCatalog
{
    private static readonly IReadOnlyCollection<SetupTask> Tasks =
    [
        new SetupTask(
            SetupTaskId.HomeRouter,
            "Home Router",
            "Simple edge router setup for a home network.",
            DeviceRole.MainRouter,
            [
                ModuleId.BasicNetwork,
                ModuleId.Nat,
                ModuleId.Firewall
            ],
            CreateWizard(
                new WizardStep("internet", "Internet connection", "Collect WAN connection basics."),
                new WizardStep("lan", "Home network", "Collect LAN, DHCP and DNS basics."),
                new WizardStep("review", "Review", "Validate the planned configuration."),
                new WizardStep("result", "Result", "Generate the configuration result."))),

        new SetupTask(
            SetupTaskId.OfficeRouter,
            "Office Router",
            "Baseline router setup for a small office network.",
            DeviceRole.MainRouter,
            [
                ModuleId.BasicNetwork,
                ModuleId.Nat,
                ModuleId.Firewall
            ],
            CreateWizard(
                new WizardStep("internet", "Internet connection", "Collect WAN connection basics."),
                new WizardStep("lan", "Office network", "Collect LAN, DHCP and DNS basics."),
                new WizardStep("security", "Security baseline", "Review administrative access assumptions."),
                new WizardStep("result", "Result", "Generate the configuration result."))),

        new SetupTask(
            SetupTaskId.AccessPoint,
            "Access Point",
            "Access point setup inside an existing network.",
            DeviceRole.AccessPoint,
            [
                ModuleId.BasicNetwork
            ],
            CreateWizard(
                new WizardStep("wireless", "Wireless network", "Collect SSID, password and country basics."),
                new WizardStep("management-ip", "Management IP", "Choose automatic or static management addressing."),
                new WizardStep("review", "Review", "Validate access point assumptions."),
                new WizardStep("result", "Result", "Generate the configuration result."))),

        new SetupTask(
            SetupTaskId.VpnGateway,
            "VPN Gateway",
            "VPN access setup for remote users.",
            DeviceRole.VpnGateway,
            [
                ModuleId.BasicNetwork,
                ModuleId.Firewall,
                ModuleId.Vpn,
                ModuleId.VpnUsers
            ],
            CreateWizard(
                new WizardStep("network", "Gateway network", "Collect gateway addressing assumptions."),
                new WizardStep("vpn", "VPN access", "Collect VPN access requirements."),
                new WizardStep("users", "VPN users", "Collect VPN user assumptions."),
                new WizardStep("result", "Result", "Generate the configuration result."))),

        new SetupTask(
            SetupTaskId.SiteToSiteVpn,
            "Site-to-Site VPN",
            "VPN tunnel setup between two office networks.",
            DeviceRole.VpnGateway,
            [
                ModuleId.BasicNetwork,
                ModuleId.Firewall,
                ModuleId.Vpn
            ],
            CreateWizard(
                new WizardStep("local-site", "Local site", "Collect local network assumptions."),
                new WizardStep("remote-site", "Remote site", "Collect remote network assumptions."),
                new WizardStep("vpn", "VPN tunnel", "Collect tunnel requirements."),
                new WizardStep("result", "Result", "Generate the configuration result.")))
    ];

    public static IReadOnlyCollection<SetupTask> GetTasks()
    {
        return Tasks;
    }

    private static WizardDefinition CreateWizard(params WizardStep[] steps)
    {
        return new WizardDefinition(steps);
    }
}
