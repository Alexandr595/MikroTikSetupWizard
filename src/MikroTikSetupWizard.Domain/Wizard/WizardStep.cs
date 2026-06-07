namespace MikroTikSetupWizard.Domain.Wizard;

public sealed class WizardStep
{
    public WizardStep(
        string id,
        string title,
        string description,
        IReadOnlyCollection<string>? fieldIds = null)
    {
        Id = id;
        Title = title;
        Description = description;
        FieldIds = fieldIds ?? Array.Empty<string>();
    }

    public string Id { get; }

    public string Title { get; }

    public string Description { get; }

    public IReadOnlyCollection<string> FieldIds { get; }
}
