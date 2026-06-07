namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class AccessPointWizardStepViewModel : ObservableObject
{
    private bool _isCurrent;
    private bool _isCompleted;
    private bool _hasIssues;

    public AccessPointWizardStepViewModel(
        string id,
        string title,
        string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public string Id { get; }

    public string Title { get; }

    public string Description { get; }

    public bool IsCurrent
    {
        get => _isCurrent;
        private set => SetProperty(ref _isCurrent, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        private set => SetProperty(ref _isCompleted, value);
    }

    public bool HasIssues
    {
        get => _hasIssues;
        set => SetProperty(ref _hasIssues, value);
    }

    public void UpdateState(int stepIndex, int currentStepIndex)
    {
        IsCurrent = stepIndex == currentStepIndex;
        IsCompleted = stepIndex < currentStepIndex;
    }
}
