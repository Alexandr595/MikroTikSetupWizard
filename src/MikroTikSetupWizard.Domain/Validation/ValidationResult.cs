namespace MikroTikSetupWizard.Domain.Validation;

public sealed class ValidationResult
{
    private static readonly ValidationResult Empty = new(Array.Empty<ValidationIssue>());

    private ValidationResult(IReadOnlyList<ValidationIssue> issues)
    {
        Issues = issues;
    }

    public IReadOnlyList<ValidationIssue> Issues { get; }

    public bool IsValid => Issues.All(issue => issue.Severity != ValidationSeverity.Error);

    public static ValidationResult Success()
    {
        return Empty;
    }

    public static ValidationResult FromIssues(IEnumerable<ValidationIssue> issues)
    {
        var materializedIssues = issues.ToArray();
        return materializedIssues.Length == 0 ? Empty : new ValidationResult(materializedIssues);
    }
}
