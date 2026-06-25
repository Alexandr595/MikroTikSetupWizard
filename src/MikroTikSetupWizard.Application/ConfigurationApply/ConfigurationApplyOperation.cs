namespace MikroTikSetupWizard.Application.ConfigurationApply;

public sealed record ConfigurationApplyOperation(
    string Id,
    string Title,
    string Description,
    string? RscPreview = null,
    bool RequiresConfirmation = true);
