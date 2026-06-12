namespace MikroTikSetupWizard.Infrastructure.Diagnostics;

internal static class DeviceDiagnosticsPortCatalog
{
    public static IReadOnlyList<DeviceDiagnosticsPort> Ports { get; } =
    [
        new("WinBox", 8291),
        new("SSH", 22),
        new("API", 8728),
        new("API-SSL", 8729),
        new("HTTP", 80),
        new("HTTPS", 443)
    ];
}

internal sealed record DeviceDiagnosticsPort(string Name, int Port);
