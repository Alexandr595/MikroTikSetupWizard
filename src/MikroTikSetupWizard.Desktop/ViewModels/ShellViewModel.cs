namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class ShellViewModel : ObservableObject
{
    private string _title = "MikroTik Setup Wizard";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
