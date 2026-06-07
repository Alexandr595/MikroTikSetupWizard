using System.Windows;
using MikroTikSetupWizard.Application.ModuleNavigation;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Application.SetupTasks;
using MikroTikSetupWizard.Desktop.Dialogs;
using MikroTikSetupWizard.Desktop.ViewModels;
using MikroTikSetupWizard.Desktop.Views;
using MikroTikSetupWizard.Infrastructure.Discovery;

namespace MikroTikSetupWizard.Desktop;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var reachabilityService = new DeviceReachabilityService();
        var manualDiscoveryService = new ManualDeviceDiscoveryService(reachabilityService);

        var window = new MainWindow
        {
            DataContext = new WizardViewModel(
                new MikroTikSetupWizardService(),
                new SaveFileDialogService(),
                new ModuleNavigationService(),
                new SetupTaskCatalogService(),
                manualDiscoveryService)
        };

        window.Show();
    }
}
