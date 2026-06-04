namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record NatConfig(bool EnableMasquerade, string WanInterfaceList);
