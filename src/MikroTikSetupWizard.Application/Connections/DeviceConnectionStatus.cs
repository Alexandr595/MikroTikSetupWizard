namespace MikroTikSetupWizard.Application.Connections;

public enum DeviceConnectionStatus
{
    NotStarted,
    NotImplemented,
    Success,
    PartialSuccess,
    InvalidCredentials,
    PortClosed,
    Timeout,
    Unreachable,
    PermissionDenied,
    ProtocolError
}
