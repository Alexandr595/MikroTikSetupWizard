using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Application.CurrentDevice;

public interface ICurrentDeviceService
{
    CurrentDeviceDto? Current { get; }

    bool HasCurrentDevice { get; }

    event EventHandler? CurrentDeviceChanged;

    void Select(DeviceDiscoveryResultDto device);

    void Clear();
}
