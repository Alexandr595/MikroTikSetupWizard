using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Modules;
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Application.Modules;

public interface IConfigurationComposer
{
    ConfigurationPlan Compose(
        string name,
        RouterOsMajorVersion routerOsVersion,
        IReadOnlyCollection<ModulePreview> modulePreviews);
}
