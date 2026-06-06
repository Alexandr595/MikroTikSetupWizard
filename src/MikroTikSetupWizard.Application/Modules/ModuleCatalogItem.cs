using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Application.Modules;

public sealed class ModuleCatalogItem
{
    public ModuleCatalogItem(
        ModuleDescriptor descriptor,
        bool isAllowed,
        bool isDefaultEnabled,
        string? disabledReason = null,
        string? warning = null)
    {
        Descriptor = descriptor;
        IsAllowed = isAllowed;
        IsDefaultEnabled = isDefaultEnabled;
        DisabledReason = disabledReason;
        Warning = warning;
    }

    public ModuleDescriptor Descriptor { get; }

    public bool IsAllowed { get; }

    public bool IsDefaultEnabled { get; }

    public string? DisabledReason { get; }

    public string? Warning { get; }
}
