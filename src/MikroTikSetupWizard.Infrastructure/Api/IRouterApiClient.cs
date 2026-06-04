using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Infrastructure.Api;

public interface IRouterApiClient
{
    Task ApplyAsync(ConfigurationPlan plan, CancellationToken cancellationToken = default);
}
