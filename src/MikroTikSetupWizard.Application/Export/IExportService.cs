namespace MikroTikSetupWizard.Application.Export;

public interface IExportService
{
    Task SaveTextAsync(string path, string content, CancellationToken cancellationToken = default);
}
