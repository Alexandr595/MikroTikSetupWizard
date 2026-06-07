using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using MikroTikSetupWizard.Application.Discovery;

namespace MikroTikSetupWizard.Infrastructure.Discovery;

public sealed class DeviceReachabilityService : IDeviceReachabilityService
{
    private static readonly int[] TcpProbePorts = [8291, 8728, 22];
    private static readonly TimeSpan TcpProbeTimeout = TimeSpan.FromMilliseconds(700);
    private const int PingTimeoutMilliseconds = 900;

    public async Task<DeviceDiscoveryResultDto> CheckReachabilityAsync(
        DeviceDiscoveryResultDto device,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(device.IpAddress)
            || !IPAddress.TryParse(device.IpAddress, out var ipAddress)
            || ipAddress.AddressFamily != AddressFamily.InterNetwork)
        {
            return device with
            {
                IsReachableByIp = false,
                ReachabilityStatus = "Unknown",
                Notes = AppendNote(device.Notes, "IP адрес не указан или некорректен.")
            };
        }

        var notes = device.Notes.ToList();

        if (await IsPingReachableAsync(ipAddress, cancellationToken))
        {
            notes.Add("IP отвечает на ping. Это не подтверждает, что устройство является MikroTik.");

            return device with
            {
                IsReachableByIp = true,
                ReachabilityStatus = "Ping reachable",
                Notes = notes
            };
        }

        var reachablePort = await FindReachableTcpPortAsync(ipAddress, cancellationToken);

        if (reachablePort.HasValue)
        {
            var reachabilityStatus = IsMikroTikPort(reachablePort.Value)
                ? "MikroTik port reachable"
                : "Generic TCP reachable";

            notes.Add(IsMikroTikPort(reachablePort.Value)
                ? $"Доступен MikroTik-порт {reachablePort.Value} WinBox/API. Вероятно, это MikroTik, но без авторизации identity/version неизвестны."
                : "Доступен SSH-порт. Это не доказывает, что устройство MikroTik.");

            return device with
            {
                IsReachableByIp = true,
                ReachabilityStatus = reachabilityStatus,
                Notes = notes
            };
        }

        notes.Add("IP недоступен. Устройство не найдено по ping/TCP.");
        notes.Add("Возможны firewall, другая VLAN/подсеть или отключенные сервисы.");

        return device with
        {
            IsReachableByIp = false,
            ReachabilityStatus = "Unreachable",
            Notes = notes
        };
    }

    private static async Task<bool> IsPingReachableAsync(
        IPAddress ipAddress,
        CancellationToken cancellationToken)
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping
                .SendPingAsync(ipAddress, PingTimeoutMilliseconds)
                .WaitAsync(cancellationToken);

            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }

    private static async Task<int?> FindReachableTcpPortAsync(
        IPAddress ipAddress,
        CancellationToken cancellationToken)
    {
        foreach (var port in TcpProbePorts)
        {
            if (await IsTcpPortReachableAsync(ipAddress, port, cancellationToken))
            {
                return port;
            }
        }

        return null;
    }

    private static async Task<bool> IsTcpPortReachableAsync(
        IPAddress ipAddress,
        int port,
        CancellationToken cancellationToken)
    {
        try
        {
            using var tcpClient = new TcpClient();
            var connectTask = tcpClient.ConnectAsync(ipAddress, port);
            var timeoutTask = Task.Delay(TcpProbeTimeout, cancellationToken);
            var completedTask = await Task.WhenAny(connectTask, timeoutTask);

            if (completedTask != connectTask)
            {
                return false;
            }

            await connectTask;
            return tcpClient.Connected;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsMikroTikPort(int port)
    {
        return port is 8291 or 8728;
    }

    private static IReadOnlyList<string> AppendNote(
        IReadOnlyList<string> notes,
        string note)
    {
        return notes.Concat([note]).ToArray();
    }
}
