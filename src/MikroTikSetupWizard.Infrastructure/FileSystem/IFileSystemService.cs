namespace MikroTikSetupWizard.Infrastructure.FileSystem;

public interface IFileSystemService
{
    Task WriteTextAsync(string path, string content, CancellationToken cancellationToken = default);
}
