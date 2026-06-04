using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Services;

public sealed class ConfigurationPlanService
{
    private readonly IConfigurationBuilder _builder;

    public ConfigurationPlanService(IConfigurationBuilder builder)
    {
        _builder = builder;
    }

    public ConfigurationPlan Build(BasicSetupRequest request)
    {
        return _builder.Build(request);
    }
}
