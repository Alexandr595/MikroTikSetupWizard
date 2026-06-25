using System.IO;
using System.Windows;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.CurrentDevice;
using MikroTikSetupWizard.Application.Diagnostics;
using MikroTikSetupWizard.Application.Discovery;
using MikroTikSetupWizard.Application.ModuleNavigation;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Application.SetupTasks;
using MikroTikSetupWizard.Desktop.Dialogs;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class WizardViewModel : ObservableObject
{
    private readonly IMikroTikSetupWizardService _setupWizardService;
    private readonly ISaveFileDialogService _saveFileDialogService;
    private readonly IModuleNavigationService _moduleNavigationService;
    private readonly ISetupTaskCatalogService _setupTaskCatalogService;
    private readonly ICurrentDeviceService _currentDeviceService;

    private bool _isHomeScreenVisible = true;
    private bool _isConfigureDeviceScreenVisible;
    private bool _isOfficeRouterWizardVisible;
    private bool _isAccessPointWizardVisible;
    private bool _isDiagnosticsScreenVisible;
    private bool _isWorkspaceVisible;
    private bool _isAdvancedModeActive;
    private GridLength _moduleNavigationColumnWidth = new(0);
    private Thickness _configurationPanelMargin = new(0);
    private string _workspaceTitle = "РћС„РёСЃРЅС‹Р№ СЂРѕСѓС‚РµСЂ";
    private string _workspaceDescription = "РўРµРєСѓС‰РёР№ MVP-СЃС†РµРЅР°СЂРёР№: Р±Р°Р·РѕРІР°СЏ СЃРµС‚СЊ Рё РїСЂРµРґРїСЂРѕСЃРјРѕС‚СЂ .rsc.";
    private IReadOnlyList<SetupTaskItemDto> _setupTasks = [];
    private string _routerName = "MikroTik-Office";
    private string _selectedRouterOsVersion = "RouterOS 7";
    private IReadOnlyList<DeviceRoleOptionDto> _deviceRoles = [];
    private DeviceRoleOptionDto? _selectedDeviceRole;
    private IReadOnlyList<ModuleNavigationItemDto> _moduleNavigationItems = [];
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
    private string _statusMessage = "Р“РѕС‚РѕРІРѕ Рє РіРµРЅРµСЂР°С†РёРё.";
    private CurrentDeviceDto? _currentDevice;

    public WizardViewModel(
        IMikroTikSetupWizardService setupWizardService,
        ISaveFileDialogService saveFileDialogService,
        IModuleNavigationService moduleNavigationService,
        ISetupTaskCatalogService setupTaskCatalogService,
        IDeviceDiscoveryService deviceDiscoveryService,
        IDeviceManualDiscoveryService manualDiscoveryService,
        IDeviceConnectionService deviceConnectionService,
        IDeviceDiagnosticsService deviceDiagnosticsService,
        ICurrentDeviceService currentDeviceService)
    {
        _setupWizardService = setupWizardService;
        _saveFileDialogService = saveFileDialogService;
        _moduleNavigationService = moduleNavigationService;
        _setupTaskCatalogService = setupTaskCatalogService;
        _currentDeviceService = currentDeviceService;
        _currentDeviceService.CurrentDeviceChanged += OnCurrentDeviceChanged;
        CurrentDevice = _currentDeviceService.Current;
        OfficeRouterWizard = new OfficeRouterWizardViewModel(_setupWizardService, _saveFileDialogService);
        AccessPointWizard = new AccessPointWizardViewModel(
            new AccessPointConfigurationBuilder(),
            _setupWizardService,
            _saveFileDialogService);
        DeviceDiscovery = new DeviceDiscoveryViewModel(
            deviceDiscoveryService,
            manualDiscoveryService,
            deviceConnectionService,
            deviceDiagnosticsService,
            currentDeviceService);

        ShowHomeCommand = new RelayCommand(_ => ShowHome());
        ShowConfigureDeviceCommand = new RelayCommand(_ => ShowConfigureDevice());
        ShowDiagnosticsCommand = new RelayCommand(_ => ShowDiagnostics());
        ShowAdvancedModeCommand = new RelayCommand(_ => ShowAdvancedMode());
        OpenCurrentDeviceConnectionCommand = new RelayCommand(_ => OpenCurrentDeviceConnection(), _ => HasCurrentDevice);
        OpenSetupTaskCommand = new RelayCommand(OpenSetupTask);
        ApplySmallOfficeProfileCommand = new RelayCommand(_ => ApplySmallOfficeProfile());
        GeneratePreviewCommand = new RelayCommand(_ => GeneratePreview());
        SaveFileCommand = new RelayCommand(async _ => await SaveFileAsync());

        SetupTasks = _setupTaskCatalogService.GetTasks().ToArray();
        DeviceRoles = _moduleNavigationService.GetDeviceRoles().ToArray();
        SelectedDeviceRole = DeviceRoles.FirstOrDefault();
        RefreshModuleNavigation();
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

    public ICommand ShowHomeCommand { get; }

    public ICommand ShowConfigureDeviceCommand { get; }

    public ICommand ShowDiagnosticsCommand { get; }

    public ICommand ShowAdvancedModeCommand { get; }

    public ICommand OpenCurrentDeviceConnectionCommand { get; }

    public ICommand OpenSetupTaskCommand { get; }

    public OfficeRouterWizardViewModel OfficeRouterWizard { get; }

    public AccessPointWizardViewModel AccessPointWizard { get; }

    public DeviceDiscoveryViewModel DeviceDiscovery { get; }

    public CurrentDeviceDto? CurrentDevice
    {
        get => _currentDevice;
        private set
        {
            if (!SetProperty(ref _currentDevice, value))
            {
                return;
            }

            OnPropertyChanged(nameof(HasCurrentDevice));
            OnPropertyChanged(nameof(HasNoCurrentDevice));

            if (OpenCurrentDeviceConnectionCommand is RelayCommand command)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }

    public bool HasCurrentDevice => CurrentDevice is not null;

    public bool HasNoCurrentDevice => !HasCurrentDevice;
    public IReadOnlyList<SetupTaskItemDto> SetupTasks
    {
        get => _setupTasks;
        private set => SetProperty(ref _setupTasks, value);
    }

    public bool IsHomeScreenVisible
    {
        get => _isHomeScreenVisible;
        private set => SetProperty(ref _isHomeScreenVisible, value);
    }

    public bool IsConfigureDeviceScreenVisible
    {
        get => _isConfigureDeviceScreenVisible;
        private set => SetProperty(ref _isConfigureDeviceScreenVisible, value);
    }

    public bool IsOfficeRouterWizardVisible
    {
        get => _isOfficeRouterWizardVisible;
        private set => SetProperty(ref _isOfficeRouterWizardVisible, value);
    }

    public bool IsAccessPointWizardVisible
    {
        get => _isAccessPointWizardVisible;
        private set => SetProperty(ref _isAccessPointWizardVisible, value);
    }

    public bool IsDiagnosticsScreenVisible
    {
        get => _isDiagnosticsScreenVisible;
        private set => SetProperty(ref _isDiagnosticsScreenVisible, value);
    }

    public bool IsWorkspaceVisible
    {
        get => _isWorkspaceVisible;
        private set => SetProperty(ref _isWorkspaceVisible, value);
    }

    public bool IsAdvancedModeActive
    {
        get => _isAdvancedModeActive;
        private set => SetProperty(ref _isAdvancedModeActive, value);
    }

    public GridLength ModuleNavigationColumnWidth
    {
        get => _moduleNavigationColumnWidth;
        private set => SetProperty(ref _moduleNavigationColumnWidth, value);
    }

    public Thickness ConfigurationPanelMargin
    {
        get => _configurationPanelMargin;
        private set => SetProperty(ref _configurationPanelMargin, value);
    }

    public string WorkspaceTitle
    {
        get => _workspaceTitle;
        private set => SetProperty(ref _workspaceTitle, value);
    }

    public string WorkspaceDescription
    {
        get => _workspaceDescription;
        private set => SetProperty(ref _workspaceDescription, value);
    }

    public IReadOnlyList<DeviceRoleOptionDto> DeviceRoles
    {
        get => _deviceRoles;
        private set => SetProperty(ref _deviceRoles, value);
    }

    public DeviceRoleOptionDto? SelectedDeviceRole
    {
        get => _selectedDeviceRole;
        set
        {
            if (SetProperty(ref _selectedDeviceRole, value))
            {
                RefreshModuleNavigation();
            }
        }
    }

    public IReadOnlyList<ModuleNavigationItemDto> ModuleNavigationItems
    {
        get => _moduleNavigationItems;
        private set => SetProperty(ref _moduleNavigationItems, value);
    }

    public string RouterName
    {
        get => _routerName;
        set => SetProperty(ref _routerName, value);
    }

    public string SelectedRouterOsVersion
    {
        get => _selectedRouterOsVersion;
        set
        {
            if (SetProperty(ref _selectedRouterOsVersion, value))
            {
                RefreshModuleNavigation();
            }
        }
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
            StatusMessage = "РСЃРїСЂР°РІСЊС‚Рµ РѕС€РёР±РєРё РІ РїР°СЂР°РјРµС‚СЂР°С….";
            return;
        }

        GeneratedRsc = result.RscText;
        StatusMessage = "РџСЂРµРґРїСЂРѕСЃРјРѕС‚СЂ РѕР±РЅРѕРІР»С‘РЅ.";
    }

    private void ShowHome()
    {
        ShowScreen(home: true);
    }

    private void ShowConfigureDevice()
    {
        ShowScreen(configureDevice: true);
    }

    private void OnCurrentDeviceChanged(object? sender, EventArgs e)
    {
        CurrentDevice = _currentDeviceService.Current;
    }

    private void OpenCurrentDeviceConnection()
    {
        if (CurrentDevice is null)
        {
            ShowDiagnostics();
            return;
        }

        ShowDiagnostics();
        DeviceDiscovery.OpenConnectionForm(CurrentDevice);
    }
    private void ShowDiagnostics()
    {
        ShowScreen(diagnostics: true);
    }

    private void ShowAdvancedMode()
    {
        IsAdvancedModeActive = true;
        ModuleNavigationColumnWidth = new GridLength(340);
        ConfigurationPanelMargin = new Thickness(20, 0, 0, 0);
        WorkspaceTitle = "Р Р°СЃС€РёСЂРµРЅРЅС‹Р№ СЂРµР¶РёРј";
        WorkspaceDescription = "РўРµС…РЅРёС‡РµСЃРєРёР№ СЂРµР¶РёРј СЃ Device Role, Module Navigation Рё С‚РµРєСѓС‰РёРј Basic Network MVP.";
        ShowScreen(workspace: true);
    }

    private void OpenSetupTask(object? parameter)
    {
        if (parameter is not SetupTaskItemDto task || !task.IsAvailable)
        {
            return;
        }

        if (task.Id == "access-point")
        {
            ShowScreen(accessPointWizard: true);
            return;
        }

        ShowScreen(officeRouterWizard: true);
    }

    private void ShowScreen(
        bool home = false,
        bool configureDevice = false,
        bool officeRouterWizard = false,
        bool accessPointWizard = false,
        bool diagnostics = false,
        bool workspace = false)
    {
        IsHomeScreenVisible = home;
        IsConfigureDeviceScreenVisible = configureDevice;
        IsOfficeRouterWizardVisible = officeRouterWizard;
        IsAccessPointWizardVisible = accessPointWizard;
        IsDiagnosticsScreenVisible = diagnostics;
        IsWorkspaceVisible = workspace;
    }

    private void RefreshModuleNavigation()
    {
        if (SelectedDeviceRole is null)
        {
            ModuleNavigationItems = [];
            return;
        }

        ModuleNavigationItems = _moduleNavigationService
            .GetModules(SelectedDeviceRole.Id, SelectedRouterOsVersion)
            .ToArray();
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
        StatusMessage = "РџСЂРѕС„РёР»СЊ \"РњР°Р»С‹Р№ РѕС„РёСЃ\" РїСЂРёРјРµРЅС‘РЅ.";
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
            StatusMessage = "РЎРѕС…СЂР°РЅРµРЅРёРµ РѕС‚РјРµРЅРµРЅРѕ.";
            return;
        }

        try
        {
            await _setupWizardService.SaveRscAsync(path, GeneratedRsc);
            StatusMessage = $"Р¤Р°Р№Р» СЃРѕС…СЂР°РЅС‘РЅ: {path}";
        }
        catch (Exception exception)
        {
            StatusMessage = "РќРµ СѓРґР°Р»РѕСЃСЊ СЃРѕС…СЂР°РЅРёС‚СЊ С„Р°Р№Р».";
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
            return "РћС€РёР±РѕРє РЅРµС‚.";
        }

        return string.Join(
            Environment.NewLine,
            issues.Select(issue => $"{issue.Severity}: {issue.Message}"));
    }
}

