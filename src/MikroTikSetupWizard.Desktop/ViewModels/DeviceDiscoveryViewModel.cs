using System.Net;
using System.Net.Sockets;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class DeviceDiscoveryViewModel : ObservableObject
{
    private readonly IDeviceManualDiscoveryService _manualDiscoveryService;
    private IReadOnlyList<DeviceDiscoveryResultDto> _devices = [];
    private IReadOnlyList<string> _recommendations =
    [
        "Ручной ввод IP не сканирует сеть автоматически.",
        "Без MNDP или подключения с авторизацией identity, MAC и версия RouterOS могут быть неизвестны.",
        "Ping может быть запрещён firewall, даже если устройство доступно по TCP."
    ];
    private DeviceDiscoveryResultDto? _selectedDevice;
    private string _manualIpAddress = string.Empty;
    private string _statusMessage = "Введите IPv4 адрес MikroTik и нажмите \"Добавить по IP\".";

    public DeviceDiscoveryViewModel(IDeviceManualDiscoveryService manualDiscoveryService)
    {
        _manualDiscoveryService = manualDiscoveryService;
        FindDevicesCommand = new RelayCommand(_ => ShowDiscoveryPlaceholder());
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

    private async Task AddManualDeviceAsync()
    {
        var ipAddress = ManualIpAddress.Trim();

        if (!IsValidIpv4(ipAddress))
        {
            StatusMessage = "Укажите корректный IPv4 адрес.";
            return;
        }

        StatusMessage = "Проверяем доступность устройства...";

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
            StatusMessage = $"Устройство {device.IpAddress} добавлено. Статус: {device.ReachabilityStatus}.";
        }
        catch (Exception exception)
        {
            StatusMessage = exception.Message;
        }
    }

    private void ShowDiscoveryPlaceholder()
    {
        StatusMessage = "Автоматическое обнаружение MNDP/IP scan будет добавлено позже. Сейчас доступен ручной ввод IP.";
    }

    private static bool IsValidIpv4(string value)
    {
        return IPAddress.TryParse(value.Trim(), out var address)
            && address.AddressFamily == AddressFamily.InterNetwork;
    }
}
