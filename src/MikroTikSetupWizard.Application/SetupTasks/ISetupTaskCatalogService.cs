using MikroTikSetupWizard.Domain.SetupTasks;

namespace MikroTikSetupWizard.Application.SetupTasks;

public interface ISetupTaskCatalogService
{
    IReadOnlyCollection<SetupTask> GetTasks();

    SetupTask? GetTask(SetupTaskId taskId);
}
