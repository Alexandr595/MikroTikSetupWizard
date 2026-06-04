using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Validation;

public interface IConfigurationValidator<in TRequest>
{
    ValidationResult Validate(TRequest request);
}
