using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.SetupTasks;
using MikroTikSetupWizard.Domain.Wizard;

namespace MikroTikSetupWizard.Application.Wizard;

public interface IWizardConfigurationComposer
{
    GeneratedConfiguration Compose(SetupTask task, WizardState state);
}
