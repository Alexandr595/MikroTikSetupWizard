using System.Text;
using MikroTikSetupWizard.Application.Export;

namespace MikroTikSetupWizard.Infrastructure.Export;

public sealed class FileExportService : IExportService
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
