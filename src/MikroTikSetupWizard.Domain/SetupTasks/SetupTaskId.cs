namespace MikroTikSetupWizard.Domain.SetupTasks;

public readonly record struct SetupTaskId(string Value)
{
    public static readonly SetupTaskId HomeRouter = new("home-router");

    public static readonly SetupTaskId OfficeRouter = new("office-router");

    public static readonly SetupTaskId AccessPoint = new("access-point");

    public static readonly SetupTaskId VpnGateway = new("vpn-gateway");

    public static readonly SetupTaskId SiteToSiteVpn = new("site-to-site-vpn");

    public override string ToString()
    {
        return Value;
    }
}
