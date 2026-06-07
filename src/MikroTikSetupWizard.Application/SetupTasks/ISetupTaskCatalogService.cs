namespace MikroTikSetupWizard.Application.SetupTasks;

public interface ISetupTaskCatalogService
{
    IReadOnlyCollection<SetupTaskItemDto> GetTasks();

    SetupTaskItemDto? GetTask(string taskId);
}
