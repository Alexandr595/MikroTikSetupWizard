using MikroTikSetupWizard.Domain.Models;

namespace MikroTikSetupWizard.Infrastructure.Persistence;

public interface IRouterProfileStore
{
    Task<IReadOnlyList<RouterProfile>> LoadAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(RouterProfile profile, CancellationToken cancellationToken = default);
}
