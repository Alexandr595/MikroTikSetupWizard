namespace MikroTikSetupWizard.Application.ModuleNavigation;

public sealed record ModuleNavigationItemDto(
    string Id,
    string Name,
    string Description,
    string Status,
    bool IsAllowed,
    bool IsDefaultEnabled,
    string? DisabledReason,
    string? Warning);
