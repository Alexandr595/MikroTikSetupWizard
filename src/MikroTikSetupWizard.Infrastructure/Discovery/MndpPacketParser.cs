using System.Buffers.Binary;
using System.Net;
using System.Text;

namespace MikroTikSetupWizard.Infrastructure.Discovery;

internal sealed record MndpPacket(
    string? Identity,
    string? MacAddress,
    string? Ipv4Address,
    string? Version,
    string? Platform,
    string? Board,
    string? InterfaceName);

internal static class MndpPacketParser
{
    private const ushort MacAddressType = 1;
    private const ushort IdentityType = 5;
    private const ushort VersionType = 7;
    private const ushort PlatformType = 8;
    private const ushort BoardType = 12;
    private const ushort InterfaceNameType = 16;
    private const ushort Ipv4AddressType = 17;

    public static bool TryParse(ReadOnlySpan<byte> packet, out MndpPacket result)
    {
        if (packet.Length < 4)
        {
            result = new MndpPacket(null, null, null, null, null, null, null);
            return false;
        }

        if (TryParseTlv(packet[4..], out result))
        {
            return true;
        }

        return TryParseTlv(packet, out result);
    }

    private static bool TryParseTlv(ReadOnlySpan<byte> payload, out MndpPacket result)
    {
        string? identity = null;
        string? macAddress = null;
        string? ipv4Address = null;
        string? version = null;
        string? platform = null;
        string? board = null;
        string? interfaceName = null;
        var recognizedFields = 0;
        var offset = 0;

        while (offset + 4 <= payload.Length)
        {
            var type = BinaryPrimitives.ReadUInt16BigEndian(payload.Slice(offset, 2));
            var length = BinaryPrimitives.ReadUInt16BigEndian(payload.Slice(offset + 2, 2));
            offset += 4;

            if (length > payload.Length - offset)
            {
                break;
            }

            var value = payload.Slice(offset, length);
            offset += length;

            switch (type)
            {
                case MacAddressType:
                    macAddress = FormatMacAddress(value);
                    recognizedFields++;
                    break;
                case IdentityType:
                    identity = ReadString(value);
                    recognizedFields++;
                    break;
                case VersionType:
                    version = ReadString(value);
                    recognizedFields++;
                    break;
                case PlatformType:
                    platform = ReadString(value);
                    recognizedFields++;
                    break;
                case BoardType:
                    board = ReadString(value);
                    recognizedFields++;
                    break;
                case InterfaceNameType:
                    interfaceName = ReadString(value);
                    recognizedFields++;
                    break;
                case Ipv4AddressType:
                    ipv4Address = FormatIpv4Address(value);
                    recognizedFields++;
                    break;
            }
        }

        result = new MndpPacket(
            identity,
            macAddress,
            ipv4Address,
            version,
            platform,
            board,
            interfaceName);

        return recognizedFields > 0;
    }

    private static string? FormatMacAddress(ReadOnlySpan<byte> value)
    {
        if (value.Length < 6)
        {
            return null;
        }

        return string.Join(
            ":",
            value[..6].ToArray().Select(part => part.ToString("X2")));
    }

    private static string? FormatIpv4Address(ReadOnlySpan<byte> value)
    {
        if (value.Length < 4)
        {
            return null;
        }

        return new IPAddress(value[..4]).ToString();
    }

    private static string? ReadString(ReadOnlySpan<byte> value)
    {
        var text = Encoding.UTF8.GetString(value).Trim('\0', ' ', '\r', '\n', '\t');
        return string.IsNullOrWhiteSpace(text) ? null : text;
    }
}
