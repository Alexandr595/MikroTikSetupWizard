namespace MikroTikSetupWizard.Application.Diagnostics;

public interface IDeviceDiagnosticsService
{
    Task<DeviceDiagnosticsResultDto> DiagnoseAsync(
        DeviceDiagnosticsRequestDto request,
        CancellationToken cancellationToken = default);
}
