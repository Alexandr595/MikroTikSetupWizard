using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Generation;

public sealed record GeneratedConfiguration(
    ValidationResult Validation,
    ConfigurationPlan? Plan,
    string RscText)
{
    public bool IsSuccess => Validation.IsValid && Plan is not null;
}
