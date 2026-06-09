using System.Net;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class DeviceDiscoveryViewModel : ObservableObject
{
    private readonly IDeviceDiscoveryService _deviceDiscoveryService;
    private readonly IDeviceManualDiscoveryService _manualDiscoveryService;
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

    public DeviceDiscoveryViewModel(
        IDeviceDiscoveryService deviceDiscoveryService,
        IDeviceManualDiscoveryService manualDiscoveryService)
    {
        _deviceDiscoveryService = deviceDiscoveryService;
        _manualDiscoveryService = manualDiscoveryService;
        FindDevicesCommand = new RelayCommand(
            async _ => await FindNearbyDevicesAsync(),
            _ => !IsDiscoveryInProgress);
        AddManualDeviceCommand = new RelayCommand(async _ => await AddManualDeviceAsync());
    }

    public ICommand FindDevicesCommand { get; }

    public ICommand AddManualDeviceCommand { get; }

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
