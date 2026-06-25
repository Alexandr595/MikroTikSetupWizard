namespace MikroTikSetupWizard.Application.ConfigurationApply;

public sealed record ConfigurationApplyPlan(
    string ScenarioName,
    string DeviceDisplayName,
    IReadOnlyList<ConfigurationApplyOperation> Operations,
    IReadOnlyList<string> Warnings,
    string? FallbackRscText = null,
    bool RequiresConfirmation = true);
