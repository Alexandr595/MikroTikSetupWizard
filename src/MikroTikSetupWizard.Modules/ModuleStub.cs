using MikroTikSetupWizard.Application.Wizard;

namespace MikroTikSetupWizard.Modules;

public abstract class ModuleStub : ISetupModule
{
    protected ModuleStub(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public string Id { get; }

    public string DisplayName { get; }

    public IReadOnlyCollection<string> RequiredFeatures { get; } = Array.Empty<string>();
}
