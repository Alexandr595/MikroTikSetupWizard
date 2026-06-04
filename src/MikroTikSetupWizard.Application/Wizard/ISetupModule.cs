namespace MikroTikSetupWizard.Application.Wizard;

public interface ISetupModule
{
    string Id { get; }

    string DisplayName { get; }

    IReadOnlyCollection<string> RequiredFeatures { get; }
}
