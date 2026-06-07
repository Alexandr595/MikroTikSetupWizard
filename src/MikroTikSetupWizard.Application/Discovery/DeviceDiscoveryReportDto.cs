namespace MikroTikSetupWizard.Application.Discovery;

public sealed record DeviceDiscoveryReportDto(
    IReadOnlyList<DeviceDiscoveryResultDto> Devices,
    IReadOnlyList<string> Recommendations);
