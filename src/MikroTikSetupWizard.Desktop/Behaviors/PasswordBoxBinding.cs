using System.Windows;
using System.Windows.Controls;

namespace MikroTikSetupWizard.Desktop.Behaviors;

public static class PasswordBoxBinding
{
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
        "IsEnabled",
        typeof(bool),
        typeof(PasswordBoxBinding),
        new PropertyMetadata(false, OnIsEnabledChanged));

    public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
        "Password",
        typeof(string),
        typeof(PasswordBoxBinding),
        new FrameworkPropertyMetadata(
            string.Empty,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnPasswordChanged));

    private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached(
        "IsUpdating",
        typeof(bool),
        typeof(PasswordBoxBinding));

    public static void SetIsEnabled(DependencyObject element, bool value)
    {
        element.SetValue(IsEnabledProperty, value);
    }

    public static bool GetIsEnabled(DependencyObject element)
    {
        return (bool)element.GetValue(IsEnabledProperty);
    }

    public static void SetPassword(DependencyObject element, string value)
    {
        element.SetValue(PasswordProperty, value);
    }

    public static string GetPassword(DependencyObject element)
    {
        return (string)element.GetValue(PasswordProperty);
    }

    private static void OnIsEnabledChanged(
        DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is not PasswordBox passwordBox)
        {
            return;
        }

        if ((bool)eventArgs.OldValue)
        {
            passwordBox.PasswordChanged -= OnPasswordBoxPasswordChanged;
        }

        if ((bool)eventArgs.NewValue)
        {
            passwordBox.PasswordChanged += OnPasswordBoxPasswordChanged;
        }
    }

    private static void OnPasswordChanged(
        DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is not PasswordBox passwordBox
            || (bool)passwordBox.GetValue(IsUpdatingProperty))
        {
            return;
        }

        passwordBox.PasswordChanged -= OnPasswordBoxPasswordChanged;
        passwordBox.Password = eventArgs.NewValue as string ?? string.Empty;
        passwordBox.PasswordChanged += OnPasswordBoxPasswordChanged;
    }

    private static void OnPasswordBoxPasswordChanged(object sender, RoutedEventArgs eventArgs)
    {
        if (sender is not PasswordBox passwordBox)
        {
            return;
        }

        passwordBox.SetValue(IsUpdatingProperty, true);
        SetPassword(passwordBox, passwordBox.Password);
        passwordBox.SetValue(IsUpdatingProperty, false);
    }
}
