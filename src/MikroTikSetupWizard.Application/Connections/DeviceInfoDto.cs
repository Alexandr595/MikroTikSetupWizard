namespace MikroTikSetupWizard.Application.Connections;

public sealed record DeviceInfoDto(
    string Identity,
    string RouterOsVersion,
    string BoardName,
    string? Uptime,
    IReadOnlyList<DeviceInterfaceDto> Interfaces);
