using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.Diagnostics;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Application.DeviceContext;

public interface IDeviceContextService
{
    DeviceContextDto? Current { get; }

    bool HasDevice { get; }

    event EventHandler? DeviceContextChanged;

    void Select(DeviceDiscoveryResultDto device);

    void UpdateDiagnostics(DeviceDiagnosticsResultDto diagnosticsResult);

    void UpdateConnection(DeviceConnectionResult connectionResult);

    void UpdateDeviceInfo(DeviceInfoDto deviceInfo);

    void Clear();
}
