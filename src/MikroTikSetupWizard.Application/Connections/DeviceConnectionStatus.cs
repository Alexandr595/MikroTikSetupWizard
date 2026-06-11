namespace MikroTikSetupWizard.Application.Connections;

public enum DeviceConnectionStatus
{
    NotStarted,
    NotImplemented,
    HostKeyConfirmationRequired,
    HostKeyMismatch,
    Success,
    PartialSuccess,
    InvalidCredentials,
    PortClosed,
    Timeout,
    Unreachable,
    PermissionDenied,
    ProtocolError
}
