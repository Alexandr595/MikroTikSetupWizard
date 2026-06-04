using System.Windows;
using MikroTikSetupWizard.Desktop.ViewModels;

namespace MikroTikSetupWizard.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AdminPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is WizardViewModel viewModel)
        {
            viewModel.AdminPassword = AdminPasswordBox.Password;
        }
    }
}
