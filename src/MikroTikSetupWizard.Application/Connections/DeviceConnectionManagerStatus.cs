namespace MikroTikSetupWizard.Application.Connections;

public enum DeviceConnectionManagerStatus
{
    Success = 0,
    InvalidCredentials,
    TransportUnavailable,
    Timeout,
    Unreachable,
    PermissionDenied,
    CertificateConfirmationRequired,
    CertificateMismatch,
    ProtocolError,
    UserCancelled
}
