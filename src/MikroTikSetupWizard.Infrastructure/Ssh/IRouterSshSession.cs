namespace MikroTikSetupWizard.Infrastructure.Ssh;

public interface IRouterSshSession : IAsyncDisposable
{
    Task ExecuteAsync(string command, CancellationToken cancellationToken = default);
}
