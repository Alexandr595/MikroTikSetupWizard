namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class AccessPointWizardInputViewModel : ObservableObject
{
    private string _deviceName = "MikroTik-AccessPoint";
    private string _bridgeName = "bridge-LAN";
    private bool _useDhcpClient = true;
    private string _managementIpAddress = "192.168.88.2";
    private int _managementPrefixLength = 24;
    private string _defaultGateway = "192.168.88.1";
    private string _dnsServers = "1.1.1.1,8.8.8.8";
    private string _ssid = string.Empty;
    private string _wifiPassword = string.Empty;

    public IReadOnlyList<int> PrefixLengths { get; } =
    [
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15,
        16,
        17,
        18,
        19,
        20,
        21,
        22,
        23,
        24,
        25,
        26,
        27,
        28,
        29,
        30,
        31,
        32
    ];

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

    public bool UseDhcpClient
    {
        get => _useDhcpClient;
        set
        {
            if (SetProperty(ref _useDhcpClient, value))
            {
                OnPropertyChanged(nameof(UseStaticManagementIp));
            }
        }
    }

    public bool UseStaticManagementIp
    {
        get => !UseDhcpClient;
        set => UseDhcpClient = !value;
    }

    public string ManagementIpAddress
    {
        get => _managementIpAddress;
        set => SetProperty(ref _managementIpAddress, value);
    }

    public int ManagementPrefixLength
    {
        get => _managementPrefixLength;
        set => SetProperty(ref _managementPrefixLength, value);
    }

    public string DefaultGateway
    {
        get => _defaultGateway;
        set => SetProperty(ref _defaultGateway, value);
    }

    public string DnsServers
    {
        get => _dnsServers;
        set => SetProperty(ref _dnsServers, value);
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
