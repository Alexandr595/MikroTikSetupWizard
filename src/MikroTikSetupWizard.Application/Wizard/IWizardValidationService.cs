using MikroTikSetupWizard.Domain.Wizard;

namespace MikroTikSetupWizard.Application.Wizard;

public interface IWizardValidationService
{
    WizardValidationResult ValidateStep(WizardDefinition definition, WizardState state);

    WizardValidationResult Validate(WizardDefinition definition, WizardState state);
}
