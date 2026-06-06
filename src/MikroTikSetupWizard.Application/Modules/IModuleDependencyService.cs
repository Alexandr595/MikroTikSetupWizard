using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Application.Modules;

public interface IModuleDependencyService
{
    IReadOnlyCollection<ModuleValidationIssue> ValidateDependencies(
        IReadOnlyCollection<ModuleState> moduleStates,
        IReadOnlyCollection<ModuleDescriptor> moduleDescriptors);
}
