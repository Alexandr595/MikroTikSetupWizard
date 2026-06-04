using MikroTikSetupWizard.Domain.Models;
using MikroTikSetupWizard.Domain.Scenarios;
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Validation;

public sealed class ValidationService : IConfigurationValidator<BasicSetupRequest>
{
    public ValidationResult Validate(BasicSetupRequest request)
    {
        var issues = new List<ValidationIssue>();

        Required(issues, request.RouterName, "RouterName", "Укажите имя роутера.");
        Required(issues, request.WanInterface, "WanInterface", "Укажите WAN-интерфейс.");
        Required(issues, request.LanBridgeName, "LanBridgeName", "Укажите имя LAN bridge.");
        Required(issues, request.AdminUserName, "AdminUserName", "Укажите имя администратора.");

        if (!Ipv4AddressMath.TryParse(request.LanAddress, out _))
        {
            issues.Add(Error("LanAddress", "LAN IP должен быть корректным IPv4-адресом."));
        }

        if (!Ipv4AddressMath.IsValidPrefixLength(request.LanPrefixLength))
        {
            issues.Add(Error("LanPrefixLength", "Префикс LAN должен быть от 1 до 32."));
        }

        ValidateDhcpAddress(issues, request.DhcpPoolStart, request, "DhcpPoolStart", "Начало DHCP-пула должно быть IPv4-адресом в LAN-сети.");
        ValidateDhcpAddress(issues, request.DhcpPoolEnd, request, "DhcpPoolEnd", "Конец DHCP-пула должен быть IPv4-адресом в LAN-сети.");

        if (Ipv4AddressMath.TryParse(request.DhcpPoolStart, out _)
            && Ipv4AddressMath.TryParse(request.DhcpPoolEnd, out _)
            && !Ipv4AddressMath.IsLessThanOrEqual(request.DhcpPoolStart, request.DhcpPoolEnd))
        {
            issues.Add(Error("DhcpPoolEnd", "Конец DHCP-пула должен быть больше или равен началу."));
        }

        ValidateDnsServers(issues, request.DnsServers);

        if (string.Equals(request.WanInterface.Trim(), request.LanBridgeName.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            issues.Add(Error("WanInterface", "WAN-интерфейс и LAN bridge должны иметь разные имена."));
        }

        if (string.IsNullOrWhiteSpace(request.AdminPassword))
        {
            issues.Add(new ValidationIssue(
                ValidationSeverity.Warning,
                "AdminPassword",
                "Пароль администратора пустой. Скрипт не будет менять пароль."));
        }

        return ValidationResult.FromIssues(issues);
    }

    private static void Required(List<ValidationIssue> issues, string value, string field, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            issues.Add(Error(field, message));
        }
    }

    private static void ValidateDhcpAddress(
        List<ValidationIssue> issues,
        string value,
        BasicSetupRequest request,
        string field,
        string message)
    {
        if (!Ipv4AddressMath.TryParse(value, out _)
            || !Ipv4AddressMath.TryParse(request.LanAddress, out _)
            || !Ipv4AddressMath.IsValidPrefixLength(request.LanPrefixLength)
            || !Ipv4AddressMath.IsInSameNetwork(value, request.LanAddress, request.LanPrefixLength))
        {
            issues.Add(Error(field, message));
        }
    }

    private static void ValidateDnsServers(List<ValidationIssue> issues, string dnsServers)
    {
        var servers = dnsServers
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (servers.Length == 0)
        {
            issues.Add(Error("DnsServers", "Укажите хотя бы один DNS-сервер."));
            return;
        }

        foreach (var server in servers)
        {
            if (!Ipv4AddressMath.TryParse(server, out _))
            {
                issues.Add(Error("DnsServers", $"DNS-сервер \"{server}\" должен быть IPv4-адресом."));
            }
        }
    }

    private static ValidationIssue Error(string field, string message)
    {
        return new ValidationIssue(ValidationSeverity.Error, field, message);
    }
}
