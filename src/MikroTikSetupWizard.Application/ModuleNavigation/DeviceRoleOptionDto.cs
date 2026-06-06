namespace MikroTikSetupWizard.Application.ModuleNavigation;

public sealed record DeviceRoleOptionDto(
    string Id,
    string Name,
    string Description)
{
    public override string ToString()
    {
        return Name;
    }
}
