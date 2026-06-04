using MikroTikSetupWizard.Domain.Models;

namespace MikroTikSetupWizard.Application.Profiles;

public interface IRouterProfileRepository
{
    Task<IReadOnlyList<RouterProfile>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(RouterProfile profile, CancellationToken cancellationToken = default);
}
