namespace MikroTikSetupWizard.Domain.Modules;

public readonly record struct ModuleId(string Value)
{
    public static readonly ModuleId BasicNetwork = new("basic-network");

    public static readonly ModuleId Nat = new("nat");

    public static readonly ModuleId Firewall = new("firewall");

    public static readonly ModuleId Vpn = new("vpn");

    public static readonly ModuleId VpnUsers = new("vpn-users");

    public static readonly ModuleId PortForwarding = new("port-forwarding");

    public override string ToString()
    {
        return Value;
    }
}
