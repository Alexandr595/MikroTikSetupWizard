namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record IpAddressConfig(string Address, int PrefixLength, string InterfaceName);
