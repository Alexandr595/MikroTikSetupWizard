namespace MikroTikSetupWizard.Application.Discovery;

public sealed record ManualDeviceDiscoveryRequestDto(
    string IpAddress,
    string? InterfaceName = null);
