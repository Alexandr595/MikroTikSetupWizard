namespace MikroTikSetupWizard.Application.Diagnostics;

public sealed record DeviceDiagnosticsResultDto(
    string Identity,
    string IpAddress,
    string MacAddress,
    string BoardName,
    string RouterOsVersion,
    string DiscoveryMethod,
    ServiceAvailabilityDto Ping,
    IReadOnlyList<ServiceAvailabilityDto> Services,
    IReadOnlyList<string> Recommendations);
