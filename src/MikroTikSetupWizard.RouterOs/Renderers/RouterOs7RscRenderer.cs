using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.RouterOs.Renderers;

public sealed class RouterOs7RscRenderer : IConfigurationRenderer
{
    private readonly RouterOsRscRenderer _renderer = new();

    public string Render(ConfigurationPlan plan)
    {
        return _renderer.Render(plan);
    }
}
