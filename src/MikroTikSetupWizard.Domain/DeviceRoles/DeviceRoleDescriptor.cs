namespace MikroTikSetupWizard.Domain.DeviceRoles;

public sealed class DeviceRoleDescriptor
{
    public DeviceRoleDescriptor(
        DeviceRole role,
        string name,
        string description,
        DeviceRolePolicy policy,
        IReadOnlyCollection<DeviceRoleWarning>? warnings = null)
    {
        Role = role;
        Name = name;
        Description = description;
        Policy = policy;
        Warnings = warnings ?? Array.Empty<DeviceRoleWarning>();
    }

    public DeviceRole Role { get; }

    public string Name { get; }

    public string Description { get; }

    public DeviceRolePolicy Policy { get; }

    public IReadOnlyCollection<DeviceRoleWarning> Warnings { get; }
}
