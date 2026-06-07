using MikroTikSetupWizard.Application.Setup;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class OfficeRouterWizardInputViewModel : ObservableObject
{
    private string _routerName = "MikroTik-Office";
    private string _routerOsVersion = "RouterOS 7";
    private string _wanInterface = "ether1";
    private string _internetConnectionType = "DHCP";
    private string _dnsServers = "1.1.1.1,8.8.8.8";
    private string _lanBridgeName = "bridge-LAN";
    private string _lanAddress = "192.168.88.1";
    private int _lanPrefixLength = 24;
    private bool _dhcpEnabled = true;
    private string _dhcpPoolStart = "192.168.88.10";
    private string _dhcpPoolEnd = "192.168.88.254";
    private string _adminUserName = "admin";
    private string _adminPassword = string.Empty;
    private bool _enableNat = true;
    private bool _enableBasicFirewall = true;

    public IReadOnlyList<string> RouterOsVersions { get; } =
    [
        "RouterOS 7",
        "RouterOS 6"
    ];

    public IReadOnlyList<string> InternetConnectionTypes { get; } =
    [
        "DHCP",
        "Static IP",
        "PPPoE"
    ];

    public IReadOnlyList<int> PrefixLengths { get; } =
    [
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
        30
    ];

    public string RouterName
    {
        get => _routerName;
        set => SetProperty(ref _routerName, value);
    }

    public string RouterOsVersion
    {
        get => _routerOsVersion;
        set => SetProperty(ref _routerOsVersion, value);
    }

    public string WanInterface
    {
        get => _wanInterface;
        set => SetProperty(ref _wanInterface, value);
    }

    public string InternetConnectionType
    {
        get => _internetConnectionType;
        set => SetProperty(ref _internetConnectionType, value);
    }

    public string DnsServers
    {
        get => _dnsServers;
        set => SetProperty(ref _dnsServers, value);
    }

    public string LanBridgeName
    {
        get => _lanBridgeName;
        set => SetProperty(ref _lanBridgeName, value);
    }

    public string LanAddress
    {
        get => _lanAddress;
        set
        {
            if (SetProperty(ref _lanAddress, value))
            {
                ApplyPrefixFromCidr(value);
            }
        }
    }

    public int LanPrefixLength
    {
        get => _lanPrefixLength;
        set => SetProperty(ref _lanPrefixLength, value);
    }

    public bool DhcpEnabled
    {
        get => _dhcpEnabled;
        set => SetProperty(ref _dhcpEnabled, value);
    }

    public string DhcpPoolStart
    {
        get => _dhcpPoolStart;
        set => SetProperty(ref _dhcpPoolStart, value);
    }

    public string DhcpPoolEnd
    {
        get => _dhcpPoolEnd;
        set => SetProperty(ref _dhcpPoolEnd, value);
    }

    public string AdminUserName
    {
        get => _adminUserName;
        set => SetProperty(ref _adminUserName, value);
    }

    public string AdminPassword
    {
        get => _adminPassword;
        set
        {
            if (SetProperty(ref _adminPassword, value))
            {
                OnPropertyChanged(nameof(HasEmptyAdminPasswordWarning));
            }
        }
    }

    public bool EnableNat
    {
        get => _enableNat;
        set => SetProperty(ref _enableNat, value);
    }

    public bool EnableBasicFirewall
    {
        get => _enableBasicFirewall;
        set => SetProperty(ref _enableBasicFirewall, value);
    }

    public bool HasEmptyAdminPasswordWarning => string.IsNullOrWhiteSpace(AdminPassword);

    public BasicSetupInputDto ToBasicSetupInputDto()
    {
        var lanAddress = ParseLanAddress();

        return new BasicSetupInputDto
        {
            RouterName = RouterName,
            RouterOsVersion = RouterOsVersion,
            WanInterface = WanInterface,
            LanBridgeName = LanBridgeName,
            LanAddress = lanAddress.Address,
            LanPrefixLength = lanAddress.PrefixLength,
            DhcpPoolStart = DhcpPoolStart,
            DhcpPoolEnd = DhcpPoolEnd,
            DnsServers = DnsServers,
            AdminUserName = AdminUserName,
            AdminPassword = AdminPassword,
            EnableNat = EnableNat,
            EnableBasicFirewall = EnableBasicFirewall
        };
    }

    private void ApplyPrefixFromCidr(string value)
    {
        var parts = value.Split('/', StringSplitOptions.TrimEntries);

        if (parts.Length == 2 && int.TryParse(parts[1], out var prefixLength))
        {
            LanPrefixLength = prefixLength;
        }
    }

    private (string Address, int PrefixLength) ParseLanAddress()
    {
        var parts = LanAddress.Split('/', StringSplitOptions.TrimEntries);

        if (parts.Length == 1)
        {
            return (LanAddress, LanPrefixLength);
        }

        if (parts.Length == 2 && int.TryParse(parts[1], out var prefixLength))
        {
            return (parts[0], prefixLength);
        }

        return (parts[0], -1);
    }
}
