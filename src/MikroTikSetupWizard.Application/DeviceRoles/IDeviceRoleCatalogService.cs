using MikroTikSetupWizard.Domain.DeviceRoles;

namespace MikroTikSetupWizard.Application.DeviceRoles;

public interface IDeviceRoleCatalogService
{
    IReadOnlyCollection<DeviceRoleDescriptor> GetRoles();
}
