namespace MikroTikSetupWizard.Application.Services;

public sealed class SetupScenarioService
{
    public IReadOnlyList<string> GetAvailableScenarios()
    {
        return
        [
            "basic"
        ];
    }
}
