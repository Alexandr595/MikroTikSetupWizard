namespace MikroTikSetupWizard.Application.SetupTasks;

public sealed record SetupTaskItemDto(
    string Id,
    string Name,
    string Description,
    bool IsAvailable,
    string Status);
