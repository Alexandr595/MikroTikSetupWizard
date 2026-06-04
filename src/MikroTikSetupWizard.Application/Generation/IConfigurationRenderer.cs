using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Application.Generation;

public interface IConfigurationRenderer
{
    string Render(ConfigurationPlan plan);
}
