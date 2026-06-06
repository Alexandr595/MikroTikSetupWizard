using MikroTikSetupWizard.Domain.DeviceRoles;

namespace MikroTikSetupWizard.Application.DeviceRoles;

public sealed class DeviceRoleCatalogService : IDeviceRoleCatalogService
{
    public IReadOnlyCollection<DeviceRoleDescriptor> GetRoles()
    {
        return DeviceRoleCatalog.GetRoles();
    }
}
