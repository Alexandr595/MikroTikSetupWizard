namespace MikroTikSetupWizard.Desktop.Navigation;

public sealed class NavigationService : INavigationService
{
    public string CurrentStepId { get; private set; } = "basic";

    public void NavigateTo(string stepId)
    {
        CurrentStepId = stepId;
    }
}
