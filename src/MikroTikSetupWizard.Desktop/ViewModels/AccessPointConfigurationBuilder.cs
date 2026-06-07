using System.Text;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class AccessPointConfigurationBuilder
{
    public string Build(AccessPointWizardInputViewModel input)
    {
        var deviceName = EscapeRouterOsString(input.DeviceName.Trim());
        var bridgeName = EscapeRouterOsString(input.BridgeName.Trim());
        var ssid = EscapeRouterOsString(input.Ssid.Trim());
        var wifiPassword = EscapeRouterOsString(input.WifiPassword.Trim());
        var managementAddress = $"{input.ManagementIpAddress.Trim()}/{input.ManagementPrefixLength}";
        var defaultGateway = input.DefaultGateway.Trim();
        var dnsServers = NormalizeDnsServers(input.DnsServers);
        var script = new StringBuilder();

        script.AppendLine("# MikroTik Setup Wizard - Access Point");
        script.AppendLine($"/system identity set name=\"{deviceName}\"");
        script.AppendLine();
        script.AppendLine($":if ([:len [/interface bridge find where name=\"{bridgeName}\"]] = 0) do={{");
        script.AppendLine($"    /interface bridge add name=\"{bridgeName}\" protocol-mode=rstp comment=\"MikroTik Setup Wizard\"");
        script.AppendLine("}");
        script.AppendLine(":foreach interfaceId in=[/interface ethernet find] do={");
        script.AppendLine("    :local interfaceName [/interface ethernet get $interfaceId name]");
        script.AppendLine("    :if ([:len [/interface bridge port find where interface=$interfaceName]] = 0) do={");
        script.AppendLine($"        /interface bridge port add bridge=\"{bridgeName}\" interface=$interfaceName comment=\"MikroTik Setup Wizard\"");
        script.AppendLine("    }");
        script.AppendLine("}");

        if (input.UseDhcpClient)
        {
            script.AppendLine();
            script.AppendLine($":if ([:len [/ip dhcp-client find where interface=\"{bridgeName}\"]] = 0) do={{");
            script.AppendLine($"    /ip dhcp-client add interface=\"{bridgeName}\" disabled=no comment=\"MikroTik Setup Wizard\"");
            script.AppendLine("}");
        }
        else
        {
            script.AppendLine();
            script.AppendLine($":if ([:len [/ip address find where address=\"{managementAddress}\" interface=\"{bridgeName}\"]] = 0) do={{");
            script.AppendLine($"    /ip address add address={managementAddress} interface=\"{bridgeName}\" comment=\"MikroTik Setup Wizard\"");
            script.AppendLine("}");
            script.AppendLine($"/ip route add dst-address=0.0.0.0/0 gateway={defaultGateway} comment=\"MikroTik Setup Wizard\"");
            script.AppendLine($"/ip dns set servers={dnsServers}");
        }

        if (input.UseRouterOs7Wifi)
        {
            script.AppendLine();
            script.AppendLine(":foreach wifiInterfaceId in=[/interface wifi find where default-name=wifi1] do={");
            script.AppendLine("    :local wifiInterfaceName [/interface wifi get $wifiInterfaceId name]");
            script.AppendLine($"    /interface wifi set $wifiInterfaceId configuration.mode=ap configuration.ssid=\"{ssid}\" security.authentication-types=wpa2-psk security.passphrase=\"{wifiPassword}\" disabled=no");
            script.AppendLine("    :if ([:len [/interface bridge port find where interface=$wifiInterfaceName]] = 0) do={");
            script.AppendLine($"        /interface bridge port add bridge=\"{bridgeName}\" interface=$wifiInterfaceName comment=\"MikroTik Setup Wizard\"");
            script.AppendLine("    }");
            script.AppendLine("}");
        }

        return script.ToString();
    }

    private static string EscapeRouterOsString(string value)
    {
        return value
            .Replace("\\", "\\\\", StringComparison.Ordinal)
            .Replace("\"", "\\\"", StringComparison.Ordinal);
    }

    private static string NormalizeDnsServers(string value)
    {
        return string.Join(
            ",",
            value.Split(
                new[] { ',', ';', ' ', '\r', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    }
}
