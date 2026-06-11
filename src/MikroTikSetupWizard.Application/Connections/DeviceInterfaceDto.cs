namespace MikroTikSetupWizard.Application.Connections;

public sealed record DeviceInterfaceDto(
    string Name,
    string Type,
    bool IsRunning,
    bool IsDisabled);
