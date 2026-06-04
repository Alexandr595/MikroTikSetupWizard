using System.Net;
using System.Net.Sockets;

namespace MikroTikSetupWizard.Domain.Models;

public static class Ipv4AddressMath
{
    public static bool TryParse(string value, out uint address)
    {
        address = 0;

        if (!IPAddress.TryParse(value?.Trim(), out var ipAddress))
        {
            return false;
        }

        if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
        {
            return false;
        }

        var bytes = ipAddress.GetAddressBytes();
        address = ((uint)bytes[0] << 24)
            | ((uint)bytes[1] << 16)
            | ((uint)bytes[2] << 8)
            | bytes[3];

        return true;
    }

    public static bool IsValidPrefixLength(int prefixLength)
    {
        return prefixLength is >= 1 and <= 32;
    }

    public static bool IsInSameNetwork(string candidate, string gateway, int prefixLength)
    {
        if (!TryParse(candidate, out var candidateAddress)
            || !TryParse(gateway, out var gatewayAddress)
            || !IsValidPrefixLength(prefixLength))
        {
            return false;
        }

        var mask = GetMask(prefixLength);
        return (candidateAddress & mask) == (gatewayAddress & mask);
    }

    public static bool IsLessThanOrEqual(string left, string right)
    {
        return TryParse(left, out var leftAddress)
            && TryParse(right, out var rightAddress)
            && leftAddress <= rightAddress;
    }

    public static string GetNetworkCidr(string gateway, int prefixLength)
    {
        if (!TryParse(gateway, out var gatewayAddress) || !IsValidPrefixLength(prefixLength))
        {
            throw new ArgumentException("Invalid IPv4 address or prefix length.", nameof(gateway));
        }

        var networkAddress = gatewayAddress & GetMask(prefixLength);
        return $"{ToDottedDecimal(networkAddress)}/{prefixLength}";
    }

    private static uint GetMask(int prefixLength)
    {
        return prefixLength == 0 ? 0 : uint.MaxValue << (32 - prefixLength);
    }

    private static string ToDottedDecimal(uint address)
    {
        return string.Join(
            ".",
            (address >> 24) & 0xFF,
            (address >> 16) & 0xFF,
            (address >> 8) & 0xFF,
            address & 0xFF);
    }
}
