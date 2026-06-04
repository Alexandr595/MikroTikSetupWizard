using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Models;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Generation;

public sealed class ConfigurationBuilder : IConfigurationBuilder
{
    public ConfigurationPlan Build(BasicSetupRequest request)
    {
        var plan = new ConfigurationPlan(request.RouterName.Trim(), request.RouterOsVersion);

        AddSystemIdentity(plan, request);
        AddInterfaceLists(plan, request);
        AddLanAddressing(plan, request);
        AddDhcp(plan, request);
        AddDns(plan, request);
        AddUserHardening(plan, request);

        if (request.EnableNat)
        {
            AddNat(plan, request);
        }

        if (request.EnableBasicFirewall)
        {
            AddFirewallBaseline(plan);
        }

        return plan;
    }

    private static void AddSystemIdentity(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "system identity",
            "set",
            "Имя роутера",
            Param("name", request.RouterName.Trim())));
    }

    private static void AddInterfaceLists(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "interface list",
            "add",
            "Список WAN-интерфейсов",
            Param("name", "WAN")));

        plan.Add(Command(
            "interface list",
            "add",
            "Список LAN-интерфейсов",
            Param("name", "LAN")));

        plan.Add(Command(
            "interface list member",
            "add",
            "WAN-интерфейс",
            Param("list", "WAN"),
            Param("interface", request.WanInterface.Trim())));
    }

    private static void AddLanAddressing(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "interface bridge",
            "add",
            "LAN bridge",
            Param("name", request.LanBridgeName.Trim())));

        plan.Add(Command(
            "interface list member",
            "add",
            "LAN bridge в списке LAN",
            Param("list", "LAN"),
            Param("interface", request.LanBridgeName.Trim())));

        plan.Add(Command(
            "ip address",
            "add",
            "IP-адрес LAN gateway",
            Param("address", $"{request.LanAddress.Trim()}/{request.LanPrefixLength}"),
            Param("interface", request.LanBridgeName.Trim())));
    }

    private static void AddDhcp(ConfigurationPlan plan, BasicSetupRequest request)
    {
        var poolName = $"{request.LanBridgeName.Trim()}-pool";
        var serverName = $"{request.LanBridgeName.Trim()}-dhcp";
        var networkCidr = Ipv4AddressMath.GetNetworkCidr(request.LanAddress.Trim(), request.LanPrefixLength);

        plan.Add(Command(
            "ip pool",
            "add",
            "DHCP pool",
            Param("name", poolName),
            Param("ranges", $"{request.DhcpPoolStart.Trim()}-{request.DhcpPoolEnd.Trim()}")));

        plan.Add(Command(
            "ip dhcp-server",
            "add",
            "DHCP server",
            Param("name", serverName),
            Param("interface", request.LanBridgeName.Trim()),
            Param("address-pool", poolName),
            Param("disabled", "no")));

        plan.Add(Command(
            "ip dhcp-server network",
            "add",
            "DHCP network",
            Param("address", networkCidr),
            Param("gateway", request.LanAddress.Trim()),
            Param("dns-server", NormalizeDnsServers(request.DnsServers))));
    }

    private static void AddDns(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "ip dns",
            "set",
            "DNS forwarding",
            Param("allow-remote-requests", "yes"),
            Param("servers", NormalizeDnsServers(request.DnsServers))));
    }

    private static void AddUserHardening(ConfigurationPlan plan, BasicSetupRequest request)
    {
        var parameters = new List<ConfigurationParameter>();

        if (!string.Equals(request.AdminUserName.Trim(), "admin", StringComparison.OrdinalIgnoreCase))
        {
            parameters.Add(Param("name", request.AdminUserName.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(request.AdminPassword))
        {
            parameters.Add(Param("password", request.AdminPassword));
        }

        if (parameters.Count == 0)
        {
            return;
        }

        plan.Add(new ConfigurationCommand(
            "user",
            "set",
            parameters,
            "[find name=\"admin\"]",
            "Администратор"));
    }

    private static void AddNat(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "ip firewall nat",
            "add",
            "NAT masquerade для выхода в интернет",
            Param("chain", "srcnat"),
            Param("out-interface-list", "WAN"),
            Param("action", "masquerade")));
    }

    private static void AddFirewallBaseline(ConfigurationPlan plan)
    {
        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Разрешить установленные входящие соединения",
            Param("chain", "input"),
            Param("action", "accept"),
            Param("connection-state", "established,related,untracked")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Отклонить invalid",
            Param("chain", "input"),
            Param("action", "drop"),
            Param("connection-state", "invalid")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Разрешить ICMP",
            Param("chain", "input"),
            Param("action", "accept"),
            Param("protocol", "icmp")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Разрешить управление из LAN",
            Param("chain", "input"),
            Param("action", "accept"),
            Param("in-interface-list", "LAN")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Закрыть вход с WAN",
            Param("chain", "input"),
            Param("action", "drop"),
            Param("in-interface-list", "WAN")));
    }

    private static ConfigurationCommand Command(
        string section,
        string operation,
        string comment,
        params ConfigurationParameter[] parameters)
    {
        return new ConfigurationCommand(section, operation, parameters, Comment: comment);
    }

    private static ConfigurationParameter Param(string name, string? value)
    {
        return new ConfigurationParameter(name, value);
    }

    private static string NormalizeDnsServers(string dnsServers)
    {
        return string.Join(
            ",",
            dnsServers.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
    }
}
