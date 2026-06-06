using System.Windows;
using MikroTikSetupWizard.Application.ModuleNavigation;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Desktop.Dialogs;
using MikroTikSetupWizard.Desktop.ViewModels;
using MikroTikSetupWizard.Desktop.Views;

namespace MikroTikSetupWizard.Desktop;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var window = new MainWindow
        {
            DataContext = new WizardViewModel(
                new MikroTikSetupWizardService(),
                new SaveFileDialogService(),
                new ModuleNavigationService())
        };

        window.Show();
    }
}
