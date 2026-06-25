namespace MikroTikSetupWizard.Application.Connections;

public sealed class ConnectionManager : IConnectionManager
{
    private const string FoundationMessage =
        "Прямое подключение через API-SSL будет добавлено следующим этапом. Сейчас доступен экспорт .rsc или legacy SSH read-only в расширенном режиме.";

    public Task<DeviceConnectionResult> ConnectAsync(
        DeviceConnectionProfile profile,
        IReadOnlyList<ConnectionTransportAvailabilityDto>? availability = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(profile);

        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(profile.IpAddress))
        {
            return Task.FromResult(CreateResult(
                DeviceConnectionTransport.Unknown,
                DeviceConnectionManagerStatus.Unreachable,
                "Для подключения требуется IP-адрес текущего устройства."));
        }

        var selectedTransport = SelectTransport(profile, availability);
        var warnings = BuildWarnings(profile, selectedTransport);

        return Task.FromResult(new DeviceConnectionResult(
            selectedTransport,
            DeviceConnectionManagerStatus.TransportUnavailable,
            warnings,
            DeviceInfo: null,
            FoundationMessage));
    }

    private static DeviceConnectionTransport SelectTransport(
        DeviceConnectionProfile profile,
        IReadOnlyList<ConnectionTransportAvailabilityDto>? availability)
    {
        if (profile.Transport is not DeviceConnectionTransport.Unknown)
        {
            return profile.Transport;
        }

        if (availability?.Any(item =>
                item.Transport == DeviceConnectionTransport.ApiSsl
                && item.IsAvailable) == true)
        {
            return DeviceConnectionTransport.ApiSsl;
        }

        if (profile.AllowInsecureTransport
            && availability?.Any(item =>
                item.Transport == DeviceConnectionTransport.Api
                && item.IsAvailable) == true)
        {
            return DeviceConnectionTransport.Api;
        }

        return DeviceConnectionTransport.ApiSsl;
    }

    private static IReadOnlyList<string> BuildWarnings(
        DeviceConnectionProfile profile,
        DeviceConnectionTransport selectedTransport)
    {
        var warnings = new List<string>
        {
            "SSH не используется автоматически и остаётся только legacy/advanced read-only способом."
        };

        if (selectedTransport == DeviceConnectionTransport.Api
            || profile.AllowInsecureTransport)
        {
            warnings.Add("API без TLS допустим только в доверенной локальной сети после явного подтверждения пользователя.");
        }

        return warnings;
    }

    private static DeviceConnectionResult CreateResult(
        DeviceConnectionTransport transport,
        DeviceConnectionManagerStatus status,
        string message)
    {
        return new DeviceConnectionResult(
            transport,
            status,
            Warnings: [],
            DeviceInfo: null,
            message);
    }
}
