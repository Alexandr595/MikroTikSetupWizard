namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record VlanConfig(int VlanId, string Name, string ParentInterface);
