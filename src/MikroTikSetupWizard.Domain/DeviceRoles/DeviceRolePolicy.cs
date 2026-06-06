using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Domain.DeviceRoles;

public sealed class DeviceRolePolicy
{
    public DeviceRolePolicy(
        IReadOnlyCollection<ModuleId>? allowedModules = null,
        IReadOnlyCollection<ModuleId>? forbiddenModules = null,
        IReadOnlyCollection<ModuleId>? defaultEnabledModules = null)
    {
        AllowedModules = allowedModules ?? Array.Empty<ModuleId>();
        ForbiddenModules = forbiddenModules ?? Array.Empty<ModuleId>();
        DefaultEnabledModules = defaultEnabledModules ?? Array.Empty<ModuleId>();
    }

    public IReadOnlyCollection<ModuleId> AllowedModules { get; }

    public IReadOnlyCollection<ModuleId> ForbiddenModules { get; }

    public IReadOnlyCollection<ModuleId> DefaultEnabledModules { get; }
}
