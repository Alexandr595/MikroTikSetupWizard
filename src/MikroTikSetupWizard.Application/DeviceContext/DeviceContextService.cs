using MikroTikSetupWizard.Application.Connections;
using MikroTikSetupWizard.Application.CurrentDevice;
using MikroTikSetupWizard.Application.Diagnostics;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Application.DeviceContext;

public sealed class DeviceContextService : IDeviceContextService, ICurrentDeviceService
{
    private const string UnknownValue = "Неизвестно";

    public DeviceContextDto? Current { get; private set; }

    public bool HasDevice => Current is not null;

    public event EventHandler? DeviceContextChanged;

    event EventHandler? ICurrentDeviceService.CurrentDeviceChanged
    {
        add => DeviceContextChanged += value;
        remove => DeviceContextChanged -= value;
    }

    CurrentDeviceDto? ICurrentDeviceService.Current => Current is null
        ? null
        : new CurrentDeviceDto(
            Current.Identity,
            Current.IpAddress,
            Current.MacAddress,
            Current.Board,
            Current.RouterOsVersion,
            Current.DiscoveryMethod,
            Current.IsConnected,
            Current.IsAuthenticated);

    bool ICurrentDeviceService.HasCurrentDevice => HasDevice;

    public void Select(DeviceDiscoveryResultDto device)
    {
        ArgumentNullException.ThrowIfNull(device);

        Current = new DeviceContextDto(
            Normalize(device.Identity),
            Normalize(device.IpAddress),
            Normalize(device.MacAddress),
            Board: UnknownValue,
            Normalize(device.RouterOsVersion),
            NormalizeDiscoveryMethod(device.DiscoveryMethod),
            DiagnosticsResult: null,
            ConnectionState: null,
            DeviceInfo: null,
            TransportAvailability: [],
            IsSelected: true,
            IsConnected: false,
            IsAuthenticated: false);

        RaiseChanged();
    }

    public void UpdateDiagnostics(DeviceDiagnosticsResultDto diagnosticsResult)
    {
        ArgumentNullException.ThrowIfNull(diagnosticsResult);

        if (Current is null || !IsSameDevice(Current.IpAddress, diagnosticsResult.IpAddress))
        {
            return;
        }

        Current = Current with
        {
            Identity = PreferKnown(diagnosticsResult.Identity, Current.Identity),
            MacAddress = PreferKnown(diagnosticsResult.MacAddress, Current.MacAddress),
            Board = PreferKnown(diagnosticsResult.BoardName, Current.Board),
            RouterOsVersion = PreferKnown(diagnosticsResult.RouterOsVersion, Current.RouterOsVersion),
            DiscoveryMethod = PreferKnown(diagnosticsResult.DiscoveryMethod, Current.DiscoveryMethod),
            DiagnosticsResult = diagnosticsResult
        };

        RaiseChanged();
    }

    public void UpdateConnection(DeviceConnectionResult connectionResult)
    {
        ArgumentNullException.ThrowIfNull(connectionResult);

        if (Current is null)
        {
            return;
        }

        var isConnected = connectionResult.IsSuccess;
        Current = Current with
        {
            ConnectionState = new DeviceConnectionStateDto(
                connectionResult.TransportUsed,
                connectionResult.Status,
                connectionResult.Message,
                connectionResult.Warnings,
                DateTimeOffset.Now),
            DeviceInfo = connectionResult.DeviceInfo ?? Current.DeviceInfo,
            IsConnected = isConnected,
            IsAuthenticated = isConnected
        };

        if (connectionResult.DeviceInfo is not null)
        {
            ApplyDeviceInfo(connectionResult.DeviceInfo);
            return;
        }

        RaiseChanged();
    }

    public void UpdateDeviceInfo(DeviceInfoDto deviceInfo)
    {
        ArgumentNullException.ThrowIfNull(deviceInfo);

        if (Current is null)
        {
            return;
        }

        ApplyDeviceInfo(deviceInfo);
    }

    public void Clear()
    {
        if (Current is null)
        {
            return;
        }

        Current = null;
        RaiseChanged();
    }

    private void ApplyDeviceInfo(DeviceInfoDto deviceInfo)
    {
        if (Current is null)
        {
            return;
        }

        Current = Current with
        {
            Identity = PreferKnown(deviceInfo.Identity, Current.Identity),
            Board = PreferKnown(deviceInfo.BoardName, Current.Board),
            RouterOsVersion = PreferKnown(deviceInfo.RouterOsVersion, Current.RouterOsVersion),
            DeviceInfo = deviceInfo
        };

        RaiseChanged();
    }

    private static string Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            || string.Equals(value, "Unknown", StringComparison.OrdinalIgnoreCase)
            ? UnknownValue
            : value.Trim();
    }

    private static string NormalizeDiscoveryMethod(string? value)
    {
        return string.Equals(value, "NeighborDiscovery", StringComparison.OrdinalIgnoreCase)
            ? "MNDP"
            : Normalize(value);
    }

    private static string PreferKnown(string? candidate, string fallback)
    {
        var normalized = Normalize(candidate);
        return normalized == UnknownValue ? fallback : normalized;
    }

    private static bool IsSameDevice(string leftIp, string rightIp)
    {
        return !string.IsNullOrWhiteSpace(leftIp)
            && !string.IsNullOrWhiteSpace(rightIp)
            && string.Equals(leftIp.Trim(), rightIp.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    private void RaiseChanged()
    {
        DeviceContextChanged?.Invoke(this, EventArgs.Empty);
    }
}
