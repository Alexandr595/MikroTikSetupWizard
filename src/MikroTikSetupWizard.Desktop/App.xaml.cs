using System.Windows;
using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.CurrentDevice;
using MikroTikSetupWizard.Application.ModuleNavigation;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Application.SetupTasks;
using MikroTikSetupWizard.Desktop.Dialogs;
using MikroTikSetupWizard.Desktop.ViewModels;
using MikroTikSetupWizard.Desktop.Views;
using MikroTikSetupWizard.Infrastructure.Diagnostics;
using MikroTikSetupWizard.Infrastructure.Discovery;
using MikroTikSetupWizard.Infrastructure.Ssh;

namespace MikroTikSetupWizard.Desktop;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var reachabilityService = new DeviceReachabilityService();
        var manualDiscoveryService = new ManualDeviceDiscoveryService(reachabilityService);
        var deviceDiscoveryService = new MndpDeviceDiscoveryService();
        var deviceConnectionService = new SshDeviceConnectionService();
        var connectionManager = new ConnectionManager();
        var deviceDiagnosticsService = new DeviceDiagnosticsService();
        var currentDeviceService = new CurrentDeviceService();

        var window = new MainWindow
        {
            DataContext = new WizardViewModel(
                new MikroTikSetupWizardService(),
                new SaveFileDialogService(),
                new ModuleNavigationService(),
                new SetupTaskCatalogService(),
                deviceDiscoveryService,
                manualDiscoveryService,
                deviceConnectionService,
                connectionManager,
                deviceDiagnosticsService,
                currentDeviceService)
        };

        window.Show();
    }
}

