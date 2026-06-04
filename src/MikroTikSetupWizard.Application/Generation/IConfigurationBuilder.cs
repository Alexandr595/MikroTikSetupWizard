using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Generation;

public interface IConfigurationBuilder
{
    ConfigurationPlan Build(BasicSetupRequest request);
}
