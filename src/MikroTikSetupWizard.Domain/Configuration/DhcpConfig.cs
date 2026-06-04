namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record DhcpConfig(
    string ServerName,
    string PoolName,
    string PoolStart,
    string PoolEnd,
    string InterfaceName);
