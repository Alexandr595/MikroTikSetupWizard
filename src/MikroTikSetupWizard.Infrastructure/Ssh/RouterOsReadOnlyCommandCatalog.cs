namespace MikroTikSetupWizard.Infrastructure.Ssh;

internal static class RouterOsReadOnlyCommandCatalog
{
    public const string Identity = "/system identity print";

    public const string Resource = "/system resource print";

    public const string RouterBoard = "/system routerboard print";

    public const string Interfaces = "/interface print detail without-paging";
}
