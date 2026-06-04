namespace MikroTikSetupWizard.Application.Setup;

public sealed record ValidationIssueDto(
    string Severity,
    string Field,
    string Message);
