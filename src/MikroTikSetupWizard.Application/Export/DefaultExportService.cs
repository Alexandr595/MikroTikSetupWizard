using System.Text;

namespace MikroTikSetupWizard.Application.Export;

internal sealed class DefaultExportService : IExportService
{
    public async Task SaveTextAsync(string path, string content, CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(path, content, Encoding.UTF8, cancellationToken);
    }
}
