namespace MikroTikSetupWizard.Domain.Modules;

public sealed class ModuleState
{
    public ModuleState(
        ModuleId moduleId,
        bool enabled,
        ModuleValidationResult? validation = null,
        ModulePreview? preview = null)
    {
        ModuleId = moduleId;
        Enabled = enabled;
        Validation = validation;
        Preview = preview;
    }

    public ModuleId ModuleId { get; }

    public bool Enabled { get; }

    public ModuleValidationResult? Validation { get; }

    public ModulePreview? Preview { get; }
}
