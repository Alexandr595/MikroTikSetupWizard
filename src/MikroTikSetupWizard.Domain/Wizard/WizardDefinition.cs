namespace MikroTikSetupWizard.Domain.Wizard;

public sealed class WizardDefinition
{
    public WizardDefinition(IReadOnlyCollection<WizardStep> steps)
    {
        Steps = steps;
    }

    public IReadOnlyCollection<WizardStep> Steps { get; }
}
