using System.IO;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Desktop.Dialogs;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class WizardViewModel : ObservableObject
{
    private readonly IMikroTikSetupWizardService _setupWizardService;
    private readonly ISaveFileDialogService _saveFileDialogService;

    private string _routerName = "MikroTik-Office";
    private string _selectedRouterOsVersion = "RouterOS 7";
    private string _wanInterface = "ether1";
    private string _lanBridgeName = "bridge-LAN";
    private string _lanAddress = "192.168.88.1";
    private int _selectedPrefixLength = 24;
    private string _dhcpPoolStart = "192.168.88.10";
    private string _dhcpPoolEnd = "192.168.88.254";
    private string _dnsServers = "1.1.1.1,8.8.8.8";
    private string _adminUserName = "admin";
    private string _adminPassword = string.Empty;
    private bool _enableNat = true;
    private bool _enableBasicFirewall = true;
    private string _generatedRsc = string.Empty;
    private string _validationMessage = string.Empty;
    private string _statusMessage = "Готово к генерации.";

    public WizardViewModel(
        IMikroTikSetupWizardService setupWizardService,
        ISaveFileDialogService saveFileDialogService)
    {
        _setupWizardService = setupWizardService;
        _saveFileDialogService = saveFileDialogService;

        ApplySmallOfficeProfileCommand = new RelayCommand(_ => ApplySmallOfficeProfile());
        GeneratePreviewCommand = new RelayCommand(_ => GeneratePreview());
        SaveFileCommand = new RelayCommand(async _ => await SaveFileAsync());
    }

    public IReadOnlyList<string> RouterOsVersions { get; } =
    [
        "RouterOS 7",
        "RouterOS 6"
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

    public ICommand GeneratePreviewCommand { get; }

    public ICommand SaveFileCommand { get; }

    public ICommand ApplySmallOfficeProfileCommand { get; }

    public string RouterName
    {
        get => _routerName;
        set => SetProperty(ref _routerName, value);
    }

    public string SelectedRouterOsVersion
    {
        get => _selectedRouterOsVersion;
        set => SetProperty(ref _selectedRouterOsVersion, value);
    }

    public string WanInterface
    {
        get => _wanInterface;
        set => SetProperty(ref _wanInterface, value);
    }

    public string LanBridgeName
    {
        get => _lanBridgeName;
        set => SetProperty(ref _lanBridgeName, value);
    }

    public string LanAddress
    {
        get => _lanAddress;
        set => SetProperty(ref _lanAddress, value);
    }

    public int SelectedPrefixLength
    {
        get => _selectedPrefixLength;
        set => SetProperty(ref _selectedPrefixLength, value);
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

    public string DnsServers
    {
        get => _dnsServers;
        set => SetProperty(ref _dnsServers, value);
    }

    public string AdminUserName
    {
        get => _adminUserName;
        set => SetProperty(ref _adminUserName, value);
    }

    public string AdminPassword
    {
        get => _adminPassword;
        set => SetProperty(ref _adminPassword, value);
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

    public string GeneratedRsc
    {
        get => _generatedRsc;
        private set => SetProperty(ref _generatedRsc, value);
    }

    public string ValidationMessage
    {
        get => _validationMessage;
        private set => SetProperty(ref _validationMessage, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    private void GeneratePreview()
    {
        var result = _setupWizardService.GeneratePreview(BuildInput());
        ValidationMessage = FormatValidation(result.Issues);

        if (!result.IsSuccess)
        {
            GeneratedRsc = string.Empty;
            StatusMessage = "Исправьте ошибки в параметрах.";
            return;
        }

        GeneratedRsc = result.RscText;
        StatusMessage = "Предпросмотр обновлён.";
    }

    private void ApplySmallOfficeProfile()
    {
        RouterName = "MikroTik-SmallOffice";
        SelectedRouterOsVersion = "RouterOS 7";
        WanInterface = "ether1";
        LanBridgeName = "bridge-LAN";
        LanAddress = "192.168.88.1/24";
        SelectedPrefixLength = 24;
        DhcpPoolStart = "192.168.88.10";
        DhcpPoolEnd = "192.168.88.254";
        DnsServers = "1.1.1.1,8.8.8.8";
        AdminUserName = "admin";
        EnableNat = true;
        EnableBasicFirewall = true;
        GeneratedRsc = string.Empty;
        ValidationMessage = string.Empty;
        StatusMessage = "Профиль \"Малый офис\" применён.";
    }

    private async Task SaveFileAsync()
    {
        if (string.IsNullOrWhiteSpace(GeneratedRsc))
        {
            GeneratePreview();
        }

        if (string.IsNullOrWhiteSpace(GeneratedRsc))
        {
            return;
        }

        var path = _saveFileDialogService.GetSaveFilePath(BuildDefaultFileName());

        if (string.IsNullOrWhiteSpace(path))
        {
            StatusMessage = "Сохранение отменено.";
            return;
        }

        try
        {
            await _setupWizardService.SaveRscAsync(path, GeneratedRsc);
            StatusMessage = $"Файл сохранён: {path}";
        }
        catch (Exception exception)
        {
            StatusMessage = "Не удалось сохранить файл.";
            ValidationMessage = exception.Message;
        }
    }

    private BasicSetupInputDto BuildInput()
    {
        var lanAddress = ParseLanAddress();

        return new BasicSetupInputDto
        {
            RouterName = RouterName,
            RouterOsVersion = SelectedRouterOsVersion,
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

    private (string Address, int PrefixLength) ParseLanAddress()
    {
        var parts = LanAddress.Split('/', StringSplitOptions.TrimEntries);

        if (parts.Length == 1)
        {
            return (LanAddress, SelectedPrefixLength);
        }

        if (parts.Length == 2 && int.TryParse(parts[1], out var prefixLength))
        {
            return (parts[0], prefixLength);
        }

        return (parts[0], -1);
    }

    private string BuildDefaultFileName()
    {
        var safeName = string.Join(
            "_",
            RouterName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

        if (string.IsNullOrWhiteSpace(safeName))
        {
            safeName = "mikrotik-config";
        }

        return $"{safeName.Trim()}.rsc";
    }

    private static string FormatValidation(IReadOnlyList<ValidationIssueDto> issues)
    {
        if (issues.Count == 0)
        {
            return "Ошибок нет.";
        }

        return string.Join(
            Environment.NewLine,
            issues.Select(issue => $"{issue.Severity}: {issue.Message}"));
    }
}
