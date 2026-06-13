using System.Net;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.Diagnostics;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class DeviceDiscoveryViewModel : ObservableObject
{
    private readonly IDeviceDiscoveryService _deviceDiscoveryService;
    private readonly IDeviceManualDiscoveryService _manualDiscoveryService;
    private readonly IDeviceConnectionService _deviceConnectionService;
    private readonly IDeviceDiagnosticsService _deviceDiagnosticsService;
    private IReadOnlyList<DeviceDiscoveryResultDto> _devices = [];
    private IReadOnlyList<DeviceDiscoveryCardViewModel> _deviceCards = [];
    private IReadOnlyList<string> _recommendations =
    [
        "MNDP ищет MikroTik только рядом, в одной L2-сети. Он не сканирует подсети и не проходит через роутеры/VLAN.",
        "Ручной ввод IP не сканирует сеть автоматически.",
        "Доступность по ping или TCP не подтверждает identity, MAC и версию RouterOS.",
        "MikroTik-порты 8291/8728 дают более сильный признак, но точное подтверждение появится только после MNDP или авторизованного подключения."
    ];
    private DeviceDiscoveryResultDto? _selectedDevice;
    private string _manualIpAddress = string.Empty;
    private string _statusMessage = "Введите IPv4 адрес и нажмите \"Добавить по IP\".";
    private bool _isDiscoveryInProgress;
    private string _connectionIp = string.Empty;
    private string _connectionLogin = "admin";
    private string _connectionStatusMessage = string.Empty;
    private DeviceInfoDto? _deviceInfo;
    private bool _isConnectionFormVisible;
    private string _connectionPassword = string.Empty;
    private string? _hostKeyFingerprint;
    private string? _hostKeyAlgorithm;
    private bool _isHostKeyConfirmationRequired;
    private bool _isConnectionInProgress;
    private DeviceDiagnosticsResultDto? _diagnosticsResult;
    private bool _isDiagnosticsInProgress;
    private string _diagnosticsStatusMessage = string.Empty;

    public DeviceDiscoveryViewModel(
        IDeviceDiscoveryService deviceDiscoveryService,
        IDeviceManualDiscoveryService manualDiscoveryService,
        IDeviceConnectionService deviceConnectionService,
        IDeviceDiagnosticsService deviceDiagnosticsService)
    {
        _deviceDiscoveryService = deviceDiscoveryService;
        _manualDiscoveryService = manualDiscoveryService;
        _deviceConnectionService = deviceConnectionService;
        _deviceDiagnosticsService = deviceDiagnosticsService;
        FindDevicesCommand = new RelayCommand(
            async _ => await FindNearbyDevicesAsync(),
            _ => !IsDiscoveryInProgress);
        AddManualDeviceCommand = new RelayCommand(async _ => await AddManualDeviceAsync());
        OpenConnectionFormCommand = new RelayCommand(OpenConnectionForm);
        ConnectToDeviceCommand = new RelayCommand(
            async _ => await ConnectToDeviceAsync(),
            _ => !IsConnectionInProgress);
        TrustHostKeyCommand = new RelayCommand(
            async _ => await TrustHostKeyAndConnectAsync(),
            _ => IsHostKeyConfirmationRequired && !IsConnectionInProgress);
        RunDeviceDiagnosticsCommand = new RelayCommand(
            async parameter => await RunDeviceDiagnosticsAsync(parameter),
            _ => !IsDiagnosticsInProgress);
    }

    public ICommand FindDevicesCommand { get; }

    public ICommand AddManualDeviceCommand { get; }

    public ICommand OpenConnectionFormCommand { get; }

    public ICommand ConnectToDeviceCommand { get; }

    public ICommand TrustHostKeyCommand { get; }

    public ICommand RunDeviceDiagnosticsCommand { get; }

    public string ManualIpAddress
    {
        get => _manualIpAddress;
        set => SetProperty(ref _manualIpAddress, value);
    }

    public IReadOnlyList<DeviceDiscoveryResultDto> Devices
    {
        get => _devices;
        private set
        {
            if (SetProperty(ref _devices, value))
            {
                OnPropertyChanged(nameof(HasDevices));
                OnPropertyChanged(nameof(HasNoDevices));
                RefreshDeviceCards();
            }
        }
    }

    public IReadOnlyList<DeviceDiscoveryCardViewModel> DeviceCards
    {
        get => _deviceCards;
        private set => SetProperty(ref _deviceCards, value);
    }

    public bool HasDevices => Devices.Count > 0;

    public bool HasNoDevices => !HasDevices;

    public DeviceDiscoveryResultDto? SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            if (!SetProperty(ref _selectedDevice, value))
            {
                return;
            }

            DiagnosticsResult = null;
            DiagnosticsStatusMessage = string.Empty;
        }
    }

    public DeviceDiagnosticsResultDto? DiagnosticsResult
    {
        get => _diagnosticsResult;
        private set
        {
            if (SetProperty(ref _diagnosticsResult, value))
            {
                OnPropertyChanged(nameof(HasDiagnosticsResult));
                RefreshDeviceCards();
            }
        }
    }

    public bool HasDiagnosticsResult => DiagnosticsResult is not null;

    public bool IsDiagnosticsInProgress
    {
        get => _isDiagnosticsInProgress;
        private set
        {
            if (SetProperty(ref _isDiagnosticsInProgress, value)
                && RunDeviceDiagnosticsCommand is RelayCommand diagnosticsCommand)
            {
                diagnosticsCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string DiagnosticsStatusMessage
    {
        get => _diagnosticsStatusMessage;
        private set => SetProperty(ref _diagnosticsStatusMessage, value);
    }

    public string ConnectionIp
    {
        get => _connectionIp;
        set => SetProperty(ref _connectionIp, value);
    }

    public string ConnectionLogin
    {
        get => _connectionLogin;
        set => SetProperty(ref _connectionLogin, value);
    }

    public string ConnectionMethodDisplay => "SSH read-only";

    public string ConnectionPassword
    {
        get => _connectionPassword;
        set => SetProperty(ref _connectionPassword, value);
    }

    public string ConnectionStatusMessage
    {
        get => _connectionStatusMessage;
        private set => SetProperty(ref _connectionStatusMessage, value);
    }

    public DeviceInfoDto? DeviceInfo
    {
        get => _deviceInfo;
        private set
        {
            if (SetProperty(ref _deviceInfo, value))
            {
                OnPropertyChanged(nameof(HasDeviceInfo));
                RefreshDeviceCards();
            }
        }
    }

    public bool HasDeviceInfo => DeviceInfo is not null;

    public bool IsConnectionFormVisible
    {
        get => _isConnectionFormVisible;
        private set => SetProperty(ref _isConnectionFormVisible, value);
    }

    public string? HostKeyFingerprint
    {
        get => _hostKeyFingerprint;
        private set
        {
            if (SetProperty(ref _hostKeyFingerprint, value))
            {
                OnPropertyChanged(nameof(HasHostKeyFingerprint));
            }
        }
    }

    public string? HostKeyAlgorithm
    {
        get => _hostKeyAlgorithm;
        private set => SetProperty(ref _hostKeyAlgorithm, value);
    }

    public bool HasHostKeyFingerprint => !string.IsNullOrWhiteSpace(HostKeyFingerprint);

    public bool IsHostKeyConfirmationRequired
    {
        get => _isHostKeyConfirmationRequired;
        private set
        {
            if (SetProperty(ref _isHostKeyConfirmationRequired, value)
                && TrustHostKeyCommand is RelayCommand trustHostKeyCommand)
            {
                trustHostKeyCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public bool IsConnectionInProgress
    {
        get => _isConnectionInProgress;
        private set
        {
            if (!SetProperty(ref _isConnectionInProgress, value))
            {
                return;
            }

            if (ConnectToDeviceCommand is RelayCommand connectCommand)
            {
                connectCommand.RaiseCanExecuteChanged();
            }

            if (TrustHostKeyCommand is RelayCommand trustCommand)
            {
                trustCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public IReadOnlyList<string> Recommendations
    {
        get => _recommendations;
        private set => SetProperty(ref _recommendations, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public bool IsDiscoveryInProgress
    {
        get => _isDiscoveryInProgress;
        private set
        {
            if (SetProperty(ref _isDiscoveryInProgress, value)
                && FindDevicesCommand is RelayCommand findDevicesCommand)
            {
                findDevicesCommand.RaiseCanExecuteChanged();
            }
        }
    }

    private async Task FindNearbyDevicesAsync()
    {
        if (IsDiscoveryInProgress)
        {
            return;
        }

        IsDiscoveryInProgress = true;
        StatusMessage = "Идёт поиск MikroTik рядом...";

        try
        {
            var devices = await _deviceDiscoveryService.FindDevicesAsync();
            Devices = MergeDevices(Devices, devices);
            SelectedDevice = devices.FirstOrDefault() ?? SelectedDevice;
            StatusMessage = devices.Count == 0
                ? "Устройства не найдены. MNDP работает только в одной L2-сети; проверьте Windows Firewall, VLAN и Neighbor Discovery на MikroTik."
                : $"Найдено {devices.Count} устройств.";
        }
        catch (Exception exception)
        {
            StatusMessage = $"Не удалось выполнить MNDP discovery. Проверьте Windows Firewall и активные сетевые адаптеры. {exception.Message}";
        }
        finally
        {
            IsDiscoveryInProgress = false;
        }
    }

    private async Task AddManualDeviceAsync()
    {
        var ipAddress = ManualIpAddress.Trim();

        if (!StrictIpv4AddressParser.TryParse(ipAddress, out _))
        {
            StatusMessage = "Введите IP в формате 192.168.1.1 без ведущих нулей.";
            return;
        }

        StatusMessage = "Проверяем доступность IP...";

        try
        {
            var device = await _manualDiscoveryService.DiscoverAsync(
                new ManualDeviceDiscoveryRequestDto(ipAddress));

            Devices = MergeDevices(Devices, [device]);

            SelectedDevice = device;
            StatusMessage = $"IP {device.IpAddress} проверен. Статус: {device.ReachabilityStatus}.";
        }
        catch (Exception exception)
        {
            StatusMessage = exception.Message;
        }
    }

    private void OpenConnectionForm(object? parameter)
    {
        if (parameter is not DeviceDiscoveryResultDto device
            || string.IsNullOrWhiteSpace(device.IpAddress))
        {
            StatusMessage = "Для подключения требуется IP-адрес устройства.";
            return;
        }

        SelectedDevice = device;
        ConnectionIp = device.IpAddress;
        DeviceInfo = null;
        ConnectionPassword = string.Empty;
        HostKeyFingerprint = null;
        HostKeyAlgorithm = null;
        IsHostKeyConfirmationRequired = false;
        ConnectionStatusMessage = "Введите пароль и проверьте SSH-подключение.";
        IsConnectionFormVisible = true;
    }

    private async Task ConnectToDeviceAsync()
    {
        await CheckConnectionAsync(expectedHostKeyFingerprint: null);
    }

    private async Task TrustHostKeyAndConnectAsync()
    {
        if (string.IsNullOrWhiteSpace(HostKeyFingerprint))
        {
            ConnectionStatusMessage = "Fingerprint SSH host key не получен.";
            return;
        }

        await CheckConnectionAsync(HostKeyFingerprint);
    }

    private async Task CheckConnectionAsync(string? expectedHostKeyFingerprint)
    {
        if (IsConnectionInProgress)
        {
            return;
        }

        if (!IsValidIpv4(ConnectionIp))
        {
            ConnectionStatusMessage = "Укажите корректный IPv4 адрес.";
            return;
        }

        if (string.IsNullOrWhiteSpace(ConnectionLogin))
        {
            ConnectionStatusMessage = "Укажите логин.";
            return;
        }

        if (string.IsNullOrEmpty(ConnectionPassword))
        {
            ConnectionStatusMessage = IsHostKeyConfirmationRequired
                ? "Введите пароль повторно, затем подтвердите host key."
                : "Введите пароль.";
            return;
        }

        IsConnectionInProgress = true;
        IsHostKeyConfirmationRequired = false;
        ConnectionStatusMessage = "Проверяем SSH host key...";

        try
        {
            var result = await _deviceConnectionService.CheckConnectionAsync(
                new DeviceConnectionRequestDto
                {
                    IpAddress = ConnectionIp.Trim(),
                    Login = ConnectionLogin.Trim(),
                    Password = ConnectionPassword,
                    Method = DeviceConnectionMethod.Ssh,
                    ExpectedHostKeyFingerprint = expectedHostKeyFingerprint
                });

            HostKeyFingerprint = result.HostKeyFingerprint;
            HostKeyAlgorithm = result.HostKeyAlgorithm;
            DeviceInfo = result.DeviceInfo;
            IsHostKeyConfirmationRequired =
                result.Status == DeviceConnectionStatus.HostKeyConfirmationRequired;
            ConnectionStatusMessage = result.Message;
        }
        catch
        {
            ConnectionStatusMessage = "Не удалось проверить SSH-подключение.";
            IsHostKeyConfirmationRequired = false;
        }
        finally
        {
            ConnectionPassword = string.Empty;
            IsConnectionInProgress = false;
        }
    }

    private async Task RunDeviceDiagnosticsAsync(object? parameter)
    {
        if (IsDiagnosticsInProgress)
        {
            return;
        }

        var device = parameter as DeviceDiscoveryResultDto ?? SelectedDevice;

        if (device is null || !IsValidIpv4(device.IpAddress ?? string.Empty))
        {
            DiagnosticsResult = null;
            DiagnosticsStatusMessage = "Для диагностики требуется корректный IPv4-адрес устройства.";
            return;
        }

        SelectedDevice = device;
        IsDiagnosticsInProgress = true;
        DiagnosticsStatusMessage = "Выполняется диагностика сетевых сервисов...";

        try
        {
            var knownDeviceInfo = string.Equals(
                ConnectionIp,
                device.IpAddress,
                StringComparison.OrdinalIgnoreCase)
                ? DeviceInfo
                : null;

            DiagnosticsResult = await _deviceDiagnosticsService.DiagnoseAsync(
                new DeviceDiagnosticsRequestDto(
                    device.Identity,
                    device.IpAddress!,
                    device.MacAddress,
                    knownDeviceInfo?.BoardName,
                    knownDeviceInfo?.RouterOsVersion ?? device.RouterOsVersion,
                    device.DiscoveryMethod));

            DiagnosticsStatusMessage = "Диагностика завершена.";
        }
        catch (Exception)
        {
            DiagnosticsResult = null;
            DiagnosticsStatusMessage = "Не удалось выполнить диагностику. Проверьте сетевой адаптер, адрес устройства и правила Windows Firewall.";
        }
        finally
        {
            IsDiagnosticsInProgress = false;
        }
    }

    private static bool IsValidIpv4(string value)
    {
        return IPAddress.TryParse(value.Trim(), out var address)
            && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    private static IReadOnlyList<DeviceDiscoveryResultDto> MergeDevices(
        IReadOnlyList<DeviceDiscoveryResultDto> existingDevices,
        IReadOnlyList<DeviceDiscoveryResultDto> newDevices)
    {
        var latestDevices = new List<DeviceDiscoveryResultDto>();

        foreach (var device in newDevices)
        {
            var duplicateIndex = latestDevices.FindIndex(
                existingDevice => IsSameDevice(existingDevice, device));

            if (duplicateIndex >= 0)
            {
                latestDevices[duplicateIndex] = device;
                continue;
            }

            latestDevices.Add(device);
        }

        var history = existingDevices.Where(
            existingDevice => !latestDevices.Any(
                latestDevice => IsSameDevice(existingDevice, latestDevice)));

        return latestDevices
            .Concat(history)
            .ToArray();
    }

    private static bool IsSameDevice(
        DeviceDiscoveryResultDto first,
        DeviceDiscoveryResultDto second)
    {
        return HasKnownValue(first.MacAddress)
            && HasKnownValue(second.MacAddress)
            && string.Equals(first.MacAddress, second.MacAddress, StringComparison.OrdinalIgnoreCase)
            || !string.IsNullOrWhiteSpace(first.IpAddress)
            && !string.IsNullOrWhiteSpace(second.IpAddress)
            && string.Equals(first.IpAddress, second.IpAddress, StringComparison.OrdinalIgnoreCase);
    }

    private static bool HasKnownValue(string? value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && !string.Equals(value, "Неизвестно", StringComparison.OrdinalIgnoreCase);
    }

    private void RefreshDeviceCards()
    {
        DeviceCards = Devices
            .Select(CreateDeviceCard)
            .ToArray();
    }

    private DeviceDiscoveryCardViewModel CreateDeviceCard(DeviceDiscoveryResultDto device)
    {
        var hasConnectedInfo = IsSameIpAddress(device.IpAddress, ConnectionIp)
            && DeviceInfo is not null;
        var hasDiagnostics = IsSameIpAddress(device.IpAddress, DiagnosticsResult?.IpAddress)
            && DiagnosticsResult is not null;

        var identity = FirstKnown(
            hasDiagnostics ? DiagnosticsResult?.Identity : null,
            hasConnectedInfo ? DeviceInfo?.Identity : null,
            device.Identity);
        var ipAddress = NormalizeDisplayValue(device.IpAddress);
        var boardName = FirstKnown(
            hasDiagnostics ? DiagnosticsResult?.BoardName : null,
            hasConnectedInfo ? DeviceInfo?.BoardName : null);
        var routerOsVersion = FirstKnown(
            hasDiagnostics ? DiagnosticsResult?.RouterOsVersion : null,
            hasConnectedInfo ? DeviceInfo?.RouterOsVersion : null,
            device.RouterOsVersion);

        return new DeviceDiscoveryCardViewModel(
            device,
            IsDisplayValueKnown(identity) ? identity : ipAddress,
            identity,
            ipAddress,
            NormalizeDisplayValue(device.MacAddress),
            boardName,
            routerOsVersion,
            NormalizeDisplayValue(device.InterfaceName),
            FormatDiscoveryMethod(device.DiscoveryMethod),
            FormatDeviceStatus(device.DiscoveryMethod, hasConnectedInfo, hasDiagnostics));
    }

    private static string FormatDeviceStatus(
        string discoveryMethod,
        bool hasConnectedInfo,
        bool hasDiagnostics)
    {
        if (hasDiagnostics)
        {
            return "Диагностика выполнена";
        }

        if (hasConnectedInfo)
        {
            return "Подключение успешно";
        }

        return IsNeighborDiscovery(discoveryMethod)
            ? "Найден через MNDP"
            : string.Equals(discoveryMethod, "Manual", StringComparison.OrdinalIgnoreCase)
                ? "Добавлен вручную"
                : "Статус неизвестен";
    }

    private static string FormatDiscoveryMethod(string discoveryMethod)
    {
        if (IsNeighborDiscovery(discoveryMethod))
        {
            return "MNDP";
        }

        return string.Equals(discoveryMethod, "Manual", StringComparison.OrdinalIgnoreCase)
            ? "Manual"
            : NormalizeDisplayValue(discoveryMethod);
    }

    private static bool IsNeighborDiscovery(string discoveryMethod)
    {
        return string.Equals(
                discoveryMethod,
                "NeighborDiscovery",
                StringComparison.OrdinalIgnoreCase)
            || string.Equals(discoveryMethod, "MNDP", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsSameIpAddress(string? first, string? second)
    {
        return IsDisplayValueKnown(first)
            && IsDisplayValueKnown(second)
            && string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
    }

    private static string FirstKnown(params string?[] values)
    {
        return values.FirstOrDefault(IsDisplayValueKnown)?.Trim() ?? "Неизвестно";
    }

    private static string NormalizeDisplayValue(string? value)
    {
        return IsDisplayValueKnown(value) ? value!.Trim() : "Неизвестно";
    }

    private static bool IsDisplayValueKnown(string? value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && !string.Equals(value, "Неизвестно", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(value, "Unknown", StringComparison.OrdinalIgnoreCase);
    }
}

public sealed record DeviceDiscoveryCardViewModel(
    DeviceDiscoveryResultDto Device,
    string Title,
    string Identity,
    string IpAddress,
    string MacAddress,
    string BoardName,
    string RouterOsVersion,
    string InterfaceName,
    string DiscoveryMethod,
    string Status);
