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
        var script = new StringBuilder();

        script.AppendLine("# MikroTik Setup Wizard - Access Point");
        script.AppendLine($"/system identity set name=\"{deviceName}\"");
        script.AppendLine();
        script.AppendLine($"/interface bridge add name=\"{bridgeName}\" protocol-mode=rstp comment=\"MikroTik Setup Wizard\"");
        script.AppendLine(":foreach interfaceId in=[/interface ethernet find] do={");
        script.AppendLine("    :local interfaceName [/interface ethernet get $interfaceId name]");
        script.AppendLine($"    /interface bridge port add bridge=\"{bridgeName}\" interface=$interfaceName comment=\"MikroTik Setup Wizard\"");
        script.AppendLine("}");

        if (input.UseDhcpClient)
        {
            script.AppendLine();
            script.AppendLine($"/ip dhcp-client add interface=\"{bridgeName}\" disabled=no comment=\"MikroTik Setup Wizard\"");
        }
        else
        {
            script.AppendLine();
            script.AppendLine($"/ip address add address={input.ManagementIpAddress.Trim()}/{input.ManagementPrefixLength} interface=\"{bridgeName}\" comment=\"MikroTik Setup Wizard\"");
            script.AppendLine($"/ip route add dst-address=0.0.0.0/0 gateway={input.DefaultGateway.Trim()} comment=\"MikroTik Setup Wizard\"");
            script.AppendLine($"/ip dns set servers={NormalizeDnsServers(input.DnsServers)}");
        }

        if (!string.IsNullOrWhiteSpace(input.Ssid))
        {
            script.AppendLine();
            script.AppendLine($":if ([:len [/interface wireless find where default-name=wlan1]] > 0) do={{");
            script.AppendLine($"    /interface wireless set [find where default-name=wlan1] mode=ap-bridge ssid=\"{ssid}\" disabled=no");

            if (!string.IsNullOrWhiteSpace(input.WifiPassword))
            {
                script.AppendLine($"    /interface wireless security-profiles set [find default=yes] mode=dynamic-keys authentication-types=wpa2-psk wpa2-pre-shared-key=\"{wifiPassword}\"");
            }

            script.AppendLine($"    /interface bridge port add bridge=\"{bridgeName}\" interface=wlan1 comment=\"MikroTik Setup Wizard\"");
            script.AppendLine("}");
            script.AppendLine($":if ([:len [/interface wifi find where default-name=wifi1]] > 0) do={{");
            script.AppendLine($"    /interface wifi set [find where default-name=wifi1] configuration.mode=ap configuration.ssid=\"{ssid}\" disabled=no");
            script.AppendLine($"    /interface bridge port add bridge=\"{bridgeName}\" interface=wifi1 comment=\"MikroTik Setup Wizard\"");
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
