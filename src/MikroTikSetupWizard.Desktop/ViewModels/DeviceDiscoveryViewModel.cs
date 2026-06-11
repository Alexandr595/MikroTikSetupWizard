using System.Net;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class DeviceDiscoveryViewModel : ObservableObject
{
    private readonly IDeviceDiscoveryService _deviceDiscoveryService;
    private readonly IDeviceManualDiscoveryService _manualDiscoveryService;
    private readonly IDeviceConnectionService _deviceConnectionService;
    private IReadOnlyList<DeviceDiscoveryResultDto> _devices = [];
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

    public DeviceDiscoveryViewModel(
        IDeviceDiscoveryService deviceDiscoveryService,
        IDeviceManualDiscoveryService manualDiscoveryService,
        IDeviceConnectionService deviceConnectionService)
    {
        _deviceDiscoveryService = deviceDiscoveryService;
        _manualDiscoveryService = manualDiscoveryService;
        _deviceConnectionService = deviceConnectionService;
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
    }

    public ICommand FindDevicesCommand { get; }

    public ICommand AddManualDeviceCommand { get; }

    public ICommand OpenConnectionFormCommand { get; }

    public ICommand ConnectToDeviceCommand { get; }

    public ICommand TrustHostKeyCommand { get; }

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
            }
        }
    }

    public bool HasDevices => Devices.Count > 0;

    public bool HasNoDevices => !HasDevices;

    public DeviceDiscoveryResultDto? SelectedDevice
    {
        get => _selectedDevice;
        set => SetProperty(ref _selectedDevice, value);
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

        if (!IsValidIpv4(ipAddress))
        {
            StatusMessage = "Укажите корректный IPv4 адрес.";
            return;
        }

        StatusMessage = "Проверяем доступность IP...";

        try
        {
            var device = await _manualDiscoveryService.DiscoverAsync(
                new ManualDeviceDiscoveryRequestDto(ipAddress));

            Devices = Devices
                .Where(existingDevice => !string.Equals(
                    existingDevice.IpAddress,
                    device.IpAddress,
                    StringComparison.OrdinalIgnoreCase))
                .Append(device)
                .ToArray();

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

    private static bool IsValidIpv4(string value)
    {
        return IPAddress.TryParse(value.Trim(), out var address)
            && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    private static IReadOnlyList<DeviceDiscoveryResultDto> MergeDevices(
        IReadOnlyList<DeviceDiscoveryResultDto> existingDevices,
        IReadOnlyList<DeviceDiscoveryResultDto> newDevices)
    {
        var mergedDevices = existingDevices.ToList();

        foreach (var device in newDevices)
        {
            var existingIndex = mergedDevices.FindIndex(existingDevice => IsSameDevice(existingDevice, device));

            if (existingIndex >= 0)
            {
                mergedDevices[existingIndex] = device;
                continue;
            }

            mergedDevices.Add(device);
        }

        return mergedDevices.ToArray();
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
}
