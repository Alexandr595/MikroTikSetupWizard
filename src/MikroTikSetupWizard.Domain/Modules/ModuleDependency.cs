namespace MikroTikSetupWizard.Domain.Modules;

public sealed record ModuleDependency(
    ModuleId RequiredModuleId,
    string Description);
