namespace MikroTikSetupWizard.Application.ConfigurationApply;

public sealed record ConfigurationApplyResult(
    ConfigurationApplyStatus Status,
    string Message,
    IReadOnlyList<ConfigurationApplyOperation> AppliedOperations,
    ConfigurationApplyOperation? FailedOperation = null,
    IReadOnlyList<string>? Warnings = null);
