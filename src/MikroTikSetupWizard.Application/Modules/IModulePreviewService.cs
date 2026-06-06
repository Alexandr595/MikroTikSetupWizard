using MikroTikSetupWizard.Domain.Modules;

namespace MikroTikSetupWizard.Application.Modules;

public interface IModulePreviewService
{
    IReadOnlyCollection<ModulePreview> BuildPreviews(
        IReadOnlyCollection<ModuleState> moduleStates);
}
