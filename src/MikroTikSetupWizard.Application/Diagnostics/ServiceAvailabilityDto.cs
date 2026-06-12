namespace MikroTikSetupWizard.Application.Diagnostics;

public sealed record ServiceAvailabilityDto(
    string Name,
    int? Port,
    ServiceAvailabilityStatus Status,
    string Details);
