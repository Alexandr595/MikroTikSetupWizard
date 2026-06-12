namespace MikroTikSetupWizard.Application.Diagnostics;

public sealed record DeviceDiagnosticsRequestDto(
    string Identity,
    string IpAddress,
    string MacAddress,
    string? BoardName,
    string? RouterOsVersion,
    string DiscoveryMethod);
