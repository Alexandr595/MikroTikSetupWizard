namespace MikroTikSetupWizard.Application.Discovery;

public interface IDeviceDiscoveryReportService
{
    DeviceDiscoveryReportDto BuildReport(IReadOnlyList<DeviceDiscoveryResultDto> devices);
}
