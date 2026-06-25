using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.Diagnostics;

namespace MikroTikSetupWizard.Application.DeviceContext;

public sealed record DeviceContextDto(
    string Identity,
    string IpAddress,
    string MacAddress,
    string Board,
    string RouterOsVersion,
    string DiscoveryMethod,
    DeviceDiagnosticsResultDto? DiagnosticsResult,
    DeviceConnectionStateDto? ConnectionState,
    DeviceInfoDto? DeviceInfo,
    IReadOnlyList<ConnectionTransportAvailabilityDto> TransportAvailability,
    bool IsSelected,
    bool IsConnected,
    bool IsAuthenticated);
