namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class AccessPointWizardInputViewModel : ObservableObject
{
    private string _deviceName = "MikroTik-AccessPoint";
    private string _bridgeName = "bridge-LAN";
    private bool _enableDhcpClient = true;
    private string _ssid = string.Empty;
    private string _wifiPassword = string.Empty;

    public string DeviceName
    {
        get => _deviceName;
        set => SetProperty(ref _deviceName, value);
    }

    public string BridgeName
    {
        get => _bridgeName;
        set => SetProperty(ref _bridgeName, value);
    }

    public bool EnableDhcpClient
    {
        get => _enableDhcpClient;
        set => SetProperty(ref _enableDhcpClient, value);
    }

    public string Ssid
    {
        get => _ssid;
        set => SetProperty(ref _ssid, value);
    }

    public string WifiPassword
    {
        get => _wifiPassword;
        set => SetProperty(ref _wifiPassword, value);
    }
}
