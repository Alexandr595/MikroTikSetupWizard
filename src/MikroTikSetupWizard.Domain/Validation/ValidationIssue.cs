namespace MikroTikSetupWizard.Domain.Validation;

public sealed record ValidationIssue(
    ValidationSeverity Severity,
    string Field,
    string Message);
