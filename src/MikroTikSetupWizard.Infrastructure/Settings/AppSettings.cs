namespace MikroTikSetupWizard.Infrastructure.Settings;

public sealed class AppSettings
{
    public string LastExportDirectory { get; init; } = string.Empty;

    public string Theme { get; init; } = "Dark";
}
