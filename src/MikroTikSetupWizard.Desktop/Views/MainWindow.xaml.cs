using System.Windows;
using System.Windows.Input;
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

    private void OfficeAdminPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is WizardViewModel viewModel)
        {
            viewModel.OfficeRouterWizard.Input.AdminPassword = OfficeAdminPasswordBox.Password;
        }
    }

    private void TitleBar_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            ToggleWindowState();
            return;
        }

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleWindowState();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ToggleWindowState()
    {
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
    }
}
