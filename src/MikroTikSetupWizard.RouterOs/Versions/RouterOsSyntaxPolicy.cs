using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Versions;

public sealed class RouterOsSyntaxPolicy
{
    public RouterOsSyntaxPolicy(RouterOsMajorVersion version)
    {
        Version = version;
    }

    public RouterOsMajorVersion Version { get; }

    public string HeaderVersionLabel => Version switch
    {
        RouterOsMajorVersion.V6 => "RouterOS 6",
        RouterOsMajorVersion.V7 => "RouterOS 7",
        _ => "RouterOS"
    };
}
