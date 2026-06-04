namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class WizardStepViewModel : ObservableObject
{
    private bool _isActive;

    public WizardStepViewModel(string id, string title)
    {
        Id = id;
        Title = title;
    }

    public string Id { get; }

    public string Title { get; }

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }
}
