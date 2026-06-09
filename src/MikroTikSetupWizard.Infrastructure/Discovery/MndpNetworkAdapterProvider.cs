using System.Buffers.Binary;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MikroTikSetupWizard.Infrastructure.Discovery;

internal sealed record MndpNetworkAdapter(
    string Name,
    string Description,
    IPAddress Address,
    IPAddress SubnetMask,
    IPAddress BroadcastAddress,
    bool IsVirtual);

internal static class MndpNetworkAdapterProvider
{
    private static readonly string[] VirtualAdapterMarkers =
    [
        "vpn",
        "virtual",
        "virtualbox",
        "hyper-v",
        "vmware",
        "wsl",
        "tailscale",
        "wireguard",
        "zerotier"
    ];

    public static IReadOnlyList<MndpNetworkAdapter> GetActiveIpv4Adapters()
    {
        return NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(adapter => adapter.OperationalStatus == OperationalStatus.Up)
            .Where(adapter => adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .SelectMany(CreateAdapterEntries)
            .ToArray();
    }

    public static MndpNetworkAdapter? FindAdapterForAddress(
        IReadOnlyList<MndpNetworkAdapter> adapters,
        IPAddress address)
    {
        return adapters.FirstOrDefault(adapter => IsInSameSubnet(
            address,
            adapter.Address,
            adapter.SubnetMask));
    }

    private static IEnumerable<MndpNetworkAdapter> CreateAdapterEntries(NetworkInterface adapter)
    {
        var ipProperties = adapter.GetIPProperties();

        foreach (var unicastAddress in ipProperties.UnicastAddresses)
        {
            if (unicastAddress.Address.AddressFamily != AddressFamily.InterNetwork
                || unicastAddress.IPv4Mask is null)
            {
                continue;
            }

            yield return new MndpNetworkAdapter(
                Name: adapter.Name,
                Description: adapter.Description,
                Address: unicastAddress.Address,
                SubnetMask: unicastAddress.IPv4Mask,
                BroadcastAddress: GetBroadcastAddress(
                    unicastAddress.Address,
                    unicastAddress.IPv4Mask),
                IsVirtual: IsVirtualAdapter(adapter));
        }
    }

    private static bool IsVirtualAdapter(NetworkInterface adapter)
    {
        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
        {
            return true;
        }

        var name = adapter.Name.ToLowerInvariant();
        var description = adapter.Description.ToLowerInvariant();

        return VirtualAdapterMarkers.Any(marker =>
            name.Contains(marker, StringComparison.Ordinal)
            || description.Contains(marker, StringComparison.Ordinal));
    }

    private static IPAddress GetBroadcastAddress(IPAddress address, IPAddress mask)
    {
        var addressValue = ToUInt32(address);
        var maskValue = ToUInt32(mask);
        var broadcastValue = addressValue | ~maskValue;

        Span<byte> bytes = stackalloc byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(bytes, broadcastValue);

        return new IPAddress(bytes);
    }

    private static bool IsInSameSubnet(IPAddress address, IPAddress subnetAddress, IPAddress mask)
    {
        if (address.AddressFamily != AddressFamily.InterNetwork)
        {
            return false;
        }

        var addressValue = ToUInt32(address);
        var subnetAddressValue = ToUInt32(subnetAddress);
        var maskValue = ToUInt32(mask);

        return (addressValue & maskValue) == (subnetAddressValue & maskValue);
    }

    private static uint ToUInt32(IPAddress address)
    {
        return BinaryPrimitives.ReadUInt32BigEndian(address.GetAddressBytes());
    }
}
