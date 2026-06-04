namespace MikroTikSetupWizard.Application.Export;

public sealed class RscExportService
{
    private readonly IExportService _exportService;

    public RscExportService(IExportService exportService)
    {
        _exportService = exportService;
    }

    public Task SaveAsync(string path, string rscText, CancellationToken cancellationToken = default)
    {
        return _exportService.SaveTextAsync(path, rscText, cancellationToken);
    }
}
