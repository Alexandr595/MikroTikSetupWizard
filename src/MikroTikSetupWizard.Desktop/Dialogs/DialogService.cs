using System.Windows;

namespace MikroTikSetupWizard.Desktop.Dialogs;

public sealed class DialogService : IDialogService
{
    public void ShowInfo(string title, string message)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
