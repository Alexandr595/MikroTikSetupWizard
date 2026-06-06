using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Domain.Modules;

public sealed record ModuleValidationIssue(
    ModuleId ModuleId,
    ValidationSeverity Severity,
    string Field,
    string Message);
