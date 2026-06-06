namespace MikroTikSetupWizard.Application.ModuleNavigation;

public interface IModuleNavigationService
{
    IReadOnlyCollection<DeviceRoleOptionDto> GetDeviceRoles();

    IReadOnlyCollection<ModuleNavigationItemDto> GetModules(
        string deviceRoleId,
        string? routerOsVersion,
        bool advancedMode = false);
}
