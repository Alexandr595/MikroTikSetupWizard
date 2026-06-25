namespace MikroTikSetupWizard.Application.ConfigurationApply;

public enum ConfigurationApplyStatus
{
    NotStarted,
    Succeeded,
    PartialSuccess,
    Failed,
    Cancelled,
    RequiresUserConfirmation,
    TransportUnavailable
}
