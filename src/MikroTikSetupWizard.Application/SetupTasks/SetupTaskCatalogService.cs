using MikroTikSetupWizard.Domain.SetupTasks;

namespace MikroTikSetupWizard.Application.SetupTasks;

public sealed class SetupTaskCatalogService : ISetupTaskCatalogService
{
    public IReadOnlyCollection<SetupTaskItemDto> GetTasks()
    {
        return SetupTaskCatalog
            .GetTasks()
            .Select(ToDto)
            .ToArray();
    }

    public SetupTaskItemDto? GetTask(string taskId)
    {
        return GetTasks().FirstOrDefault(task => task.Id == taskId);
    }

    private static SetupTaskItemDto ToDto(SetupTask task)
    {
        var isAvailable = task.TaskId == SetupTaskId.OfficeRouter
            || task.TaskId == SetupTaskId.AccessPoint;

        return new SetupTaskItemDto(
            task.TaskId.ToString(),
            GetName(task.TaskId),
            GetDescription(task.TaskId),
            isAvailable,
            isAvailable ? "Доступно" : "Будет добавлено позже");
    }

    private static string GetName(SetupTaskId taskId)
    {
        if (taskId == SetupTaskId.HomeRouter)
        {
            return "Домашний роутер";
        }

        if (taskId == SetupTaskId.OfficeRouter)
        {
            return "Офисный роутер";
        }

        if (taskId == SetupTaskId.AccessPoint)
        {
            return "Точка доступа";
        }

        if (taskId == SetupTaskId.VpnGateway)
        {
            return "VPN-шлюз";
        }

        if (taskId == SetupTaskId.SiteToSiteVpn)
        {
            return "Соединить офисы через VPN";
        }

        return taskId.ToString();
    }

    private static string GetDescription(SetupTaskId taskId)
    {
        if (taskId == SetupTaskId.HomeRouter)
        {
            return "Базовый сценарий для домашней сети.";
        }

        if (taskId == SetupTaskId.OfficeRouter)
        {
            return "Текущий MVP: LAN, DHCP, DNS, NAT, firewall и предпросмотр .rsc.";
        }

        if (taskId == SetupTaskId.AccessPoint)
        {
            return "Сценарий для устройства внутри существующей сети.";
        }

        if (taskId == SetupTaskId.VpnGateway)
        {
            return "Сценарий для удалённого доступа через VPN.";
        }

        if (taskId == SetupTaskId.SiteToSiteVpn)
        {
            return "Сценарий для связи двух офисных сетей.";
        }

        return string.Empty;
    }
}
