namespace MikroTikSetupWizard.Application.CurrentDevice;

public sealed record CurrentDeviceDto(
    string Identity,
    string IpAddress,
    string MacAddress,
    string Board,
    string RouterOsVersion,
    string DiscoveryMethod,
    bool IsConnected,
    bool IsAuthenticated);
