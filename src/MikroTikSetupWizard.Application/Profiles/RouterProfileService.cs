using MikroTikSetupWizard.Domain.Models;

namespace MikroTikSetupWizard.Application.Profiles;

public sealed class RouterProfileService
{
    private readonly IRouterProfileRepository _repository;

    public RouterProfileService(IRouterProfileRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<RouterProfile>> GetProfilesAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}
