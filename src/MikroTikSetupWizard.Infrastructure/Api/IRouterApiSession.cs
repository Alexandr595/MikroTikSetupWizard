namespace MikroTikSetupWizard.Infrastructure.Api;

public interface IRouterApiSession : IAsyncDisposable
{
    Task SendCommandAsync(string path, IReadOnlyDictionary<string, string> parameters, CancellationToken cancellationToken = default);
}
