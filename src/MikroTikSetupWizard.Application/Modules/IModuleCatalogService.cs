using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Application.Modules;

public interface IModuleCatalogService
{
    IReadOnlyCollection<ModuleDescriptor> GetModules();

    IReadOnlyCollection<ModuleCatalogItem> GetModules(ModuleCatalogContext context);
}
