namespace MikroTikSetupWizard.Infrastructure.Settings;

public interface IAppSettingsRepository
{
    Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default);
}
