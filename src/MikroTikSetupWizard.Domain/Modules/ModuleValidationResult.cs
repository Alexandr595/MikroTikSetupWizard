using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Domain.Modules;

public sealed class ModuleValidationResult
{
    public ModuleValidationResult(
        ModuleId moduleId,
        IReadOnlyCollection<ModuleValidationIssue>? issues = null)
    {
        ModuleId = moduleId;
        Issues = issues ?? Array.Empty<ModuleValidationIssue>();
    }

    public ModuleId ModuleId { get; }

    public IReadOnlyCollection<ModuleValidationIssue> Issues { get; }

    public bool IsValid => Issues.All(issue => issue.Severity != ValidationSeverity.Error);
}
