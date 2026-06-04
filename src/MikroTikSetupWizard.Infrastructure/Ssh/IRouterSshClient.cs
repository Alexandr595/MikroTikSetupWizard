using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Infrastructure.Ssh;

public interface IRouterSshClient
{
    Task ApplyAsync(ConfigurationPlan plan, CancellationToken cancellationToken = default);
}
