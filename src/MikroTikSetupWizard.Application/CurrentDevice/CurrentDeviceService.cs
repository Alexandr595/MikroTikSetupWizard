using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Application.CurrentDevice;

public sealed class CurrentDeviceService : ICurrentDeviceService
{
    private const string UnknownValue = "Неизвестно";

    public CurrentDeviceDto? Current { get; private set; }

    public bool HasCurrentDevice => Current is not null;

    public event EventHandler? CurrentDeviceChanged;

    public void Select(DeviceDiscoveryResultDto device)
    {
        ArgumentNullException.ThrowIfNull(device);

        Current = new CurrentDeviceDto(
            Normalize(device.Identity),
            Normalize(device.IpAddress),
            Normalize(device.MacAddress),
            UnknownValue,
            Normalize(device.RouterOsVersion),
            NormalizeDiscoveryMethod(device.DiscoveryMethod),
            IsConnected: false,
            IsAuthenticated: false);

        CurrentDeviceChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Clear()
    {
        if (Current is null)
        {
            return;
        }

        Current = null;
        CurrentDeviceChanged?.Invoke(this, EventArgs.Empty);
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
        if (string.Equals(value, "NeighborDiscovery", StringComparison.OrdinalIgnoreCase))
        {
            return "MNDP";
        }

        return Normalize(value);
    }
}
