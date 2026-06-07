namespace MikroTikSetupWizard.Domain.Wizard;

public sealed class WizardValidationResult
{
    public WizardValidationResult(bool isValid, IReadOnlyCollection<string>? messages = null)
    {
        IsValid = isValid;
        Messages = messages ?? Array.Empty<string>();
    }

    public bool IsValid { get; }

    public IReadOnlyCollection<string> Messages { get; }

    public static WizardValidationResult Success { get; } = new(true);
}
