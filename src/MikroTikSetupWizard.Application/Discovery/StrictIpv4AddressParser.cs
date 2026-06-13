using System.Globalization;
using System.Net;

namespace MikroTikSetupWizard.Application.Discovery;

public static class StrictIpv4AddressParser
{
    public static bool TryParse(string? value, out IPAddress address)
    {
        address = IPAddress.None;

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var parts = value.Trim().Split('.');

        if (parts.Length != 4)
        {
            return false;
        }

        var octets = new byte[4];

        for (var index = 0; index < parts.Length; index++)
        {
            var part = parts[index];

            if (part.Length is < 1 or > 3
                || part.Length > 1 && part[0] == '0'
                || part.Any(character => character is < '0' or > '9')
                || !byte.TryParse(
                    part,
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out octets[index]))
            {
                return false;
            }
        }

        address = new IPAddress(octets);
        return true;
    }
}
