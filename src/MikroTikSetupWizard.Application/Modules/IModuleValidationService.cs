using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Application.Modules;

public interface IModuleValidationService
{
    IReadOnlyCollection<ModuleValidationResult> Validate(
        IReadOnlyCollection<ModuleState> moduleStates);
}
