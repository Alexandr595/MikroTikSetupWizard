using System.Net;
using System.Net.Sockets;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Infrastructure.Discovery;

public sealed class MndpDeviceDiscoveryService : IDeviceDiscoveryService
{
    private static readonly byte[] DiscoveryRequest = [0x00, 0x00, 0x00, 0x00];
    private static readonly TimeSpan DiscoveryTimeout = TimeSpan.FromSeconds(4);
    private const int MndpPort = 5678;

    public async Task<IReadOnlyList<DeviceDiscoveryResultDto>> FindDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        var adapters = MndpNetworkAdapterProvider.GetActiveIpv4Adapters();

        if (adapters.Count == 0)
        {
            throw new InvalidOperationException("Не найдено активных IPv4 сетевых адаптеров для MNDP discovery.");
        }

        using var udpClient = CreateUdpClient();
        var devices = new Dictionary<string, DeviceDiscoveryResultDto>(StringComparer.OrdinalIgnoreCase);
        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        timeoutSource.CancelAfter(DiscoveryTimeout);

        await SendDiscoveryRequestsAsync(udpClient, adapters, timeoutSource.Token);

        while (!timeoutSource.IsCancellationRequested)
        {
            UdpReceiveResult receiveResult;

            try
            {
                receiveResult = await udpClient.ReceiveAsync(timeoutSource.Token);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (SocketException)
            {
                break;
            }

            if (!MndpPacketParser.TryParse(receiveResult.Buffer, out var packet))
            {
                continue;
            }

            var adapter = MndpNetworkAdapterProvider.FindAdapterForAddress(
                adapters,
                receiveResult.RemoteEndPoint.Address);
            var device = CreateDevice(packet, receiveResult.RemoteEndPoint, adapter);
            devices[BuildDeviceKey(device)] = device;
        }

        return devices.Values
            .OrderBy(device => device.Identity)
            .ThenBy(device => device.IpAddress)
            .ToArray();
    }

    private static UdpClient CreateUdpClient()
    {
        var udpClient = new UdpClient(AddressFamily.InterNetwork);
        udpClient.EnableBroadcast = true;
        udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, MndpPort));

        return udpClient;
    }

    private static async Task SendDiscoveryRequestsAsync(
        UdpClient udpClient,
        IReadOnlyList<MndpNetworkAdapter> adapters,
        CancellationToken cancellationToken)
    {
        var broadcastEndpoints = adapters
            .Select(adapter => adapter.BroadcastAddress)
            .Append(IPAddress.Broadcast)
            .Distinct()
            .Select(address => new IPEndPoint(address, MndpPort))
            .ToArray();

        foreach (var endpoint in broadcastEndpoints)
        {
            for (var attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    await udpClient.SendAsync(DiscoveryRequest, endpoint, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }
    }

    private static DeviceDiscoveryResultDto CreateDevice(
        MndpPacket packet,
        IPEndPoint remoteEndPoint,
        MndpNetworkAdapter? adapter)
    {
        var notes = new List<string>
        {
            "Устройство найдено через MNDP в одной L2-сети. Авторизация не выполнялась."
        };

        if (adapter is not null)
        {
            notes.Add($"Локальный адаптер: {adapter.Name}.");

            if (adapter.IsVirtual)
            {
                notes.Add("Адаптер похож на VPN/виртуальный; результаты могут быть неполными.");
            }
        }
        else
        {
            notes.Add("Локальный адаптер не определён автоматически.");
        }

        if (!string.IsNullOrWhiteSpace(packet.InterfaceName))
        {
            notes.Add($"Интерфейс устройства: {packet.InterfaceName}.");
        }

        if (!string.IsNullOrWhiteSpace(packet.Platform)
            || !string.IsNullOrWhiteSpace(packet.Board))
        {
            notes.Add($"Платформа/board: {JoinKnownValues(packet.Platform, packet.Board)}.");
        }

        var ipAddress = packet.Ipv4Address ?? remoteEndPoint.Address.ToString();
        var macAddress = packet.MacAddress ?? "Неизвестно";

        return new DeviceDiscoveryResultDto(
            Identity: packet.Identity ?? "Неизвестно",
            IpAddress: ipAddress,
            MacAddress: macAddress,
            RouterOsVersion: packet.Version ?? "Неизвестно",
            InterfaceName: adapter?.Name ?? "MNDP",
            DiscoveryMethod: "NeighborDiscovery",
            IsReachableByIp: !string.IsNullOrWhiteSpace(ipAddress),
            IsReachableByMac: !string.IsNullOrWhiteSpace(packet.MacAddress),
            ReachabilityStatus: "NeighborDiscovery",
            Notes: notes);
    }

    private static string BuildDeviceKey(DeviceDiscoveryResultDto device)
    {
        if (!string.IsNullOrWhiteSpace(device.MacAddress)
            && !string.Equals(device.MacAddress, "Неизвестно", StringComparison.OrdinalIgnoreCase))
        {
            return $"mac:{device.MacAddress}";
        }

        if (!string.IsNullOrWhiteSpace(device.IpAddress))
        {
            return $"ip:{device.IpAddress}";
        }

        return $"identity:{device.Identity}|interface:{device.InterfaceName}";
    }

    private static string JoinKnownValues(params string?[] values)
    {
        return string.Join(
            ", ",
            values.Where(value => !string.IsNullOrWhiteSpace(value)));
    }
}
