namespace MikroTikSetupWizard.Infrastructure.Logging;

public interface ILogService
{
    void Info(string message);

    void Error(string message, Exception exception);
}
