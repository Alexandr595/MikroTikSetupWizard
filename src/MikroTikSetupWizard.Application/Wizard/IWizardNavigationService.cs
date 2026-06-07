using MikroTikSetupWizard.Domain.SetupTasks;
using MikroTikSetupWizard.Domain.Wizard;

namespace MikroTikSetupWizard.Application.Wizard;

public interface IWizardNavigationService
{
    WizardState Start(SetupTask task);

    WizardState MoveNext(WizardDefinition definition, WizardState state);

    WizardState MoveBack(WizardDefinition definition, WizardState state);
}
