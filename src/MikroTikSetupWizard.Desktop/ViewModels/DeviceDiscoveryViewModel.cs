using System.Windows.Input;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class DeviceDiscoveryViewModel : ObservableObject
{
    private IReadOnlyList<DeviceDiscoveryResultDto> _devices = [];
    private IReadOnlyList<string> _recommendations =
    [
        "Обнаружение устройств будет добавлено на следующем этапе.",
        "Если устройство не найдено, проверьте VLAN, подсеть, кабель, PoE, порт и свитч.",
        "Neighbor Discovery или MAC Server могут быть отключены на MikroTik."
    ];
    private DeviceDiscoveryResultDto? _selectedDevice;
    private string _statusMessage = "Обнаружение устройств будет добавлено на следующем этапе.";

    public DeviceDiscoveryViewModel()
    {
        FindDevicesCommand = new RelayCommand(_ => ShowDiscoveryPlaceholder());
    }

    public ICommand FindDevicesCommand { get; }

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

    private void ShowDiscoveryPlaceholder()
    {
        Devices = [];
        SelectedDevice = null;
        StatusMessage = "Обнаружение устройств будет добавлено на следующем этапе.";
    }
}
