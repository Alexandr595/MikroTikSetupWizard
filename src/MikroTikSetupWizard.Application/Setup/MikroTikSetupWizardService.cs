using MikroTikSetupWizard.Application.Export;
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Application.Services;
using MikroTikSetupWizard.Application.Validation;
using MikroTikSetupWizard.Domain.RouterOs;
using MikroTikSetupWizard.Domain.Scenarios;
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Setup;

public sealed class MikroTikSetupWizardService : IMikroTikSetupWizardService
{
    private readonly BasicSetupWorkflow _workflow;
    private readonly IExportService _exportService;

    public MikroTikSetupWizardService()
        : this(
            new BasicSetupWorkflow(
                new ValidationService(),
                new ConfigurationBuilder(),
                new RscConfigurationRenderer()),
            new DefaultExportService())
    {
    }

    public MikroTikSetupWizardService(BasicSetupWorkflow workflow, IExportService exportService)
    {
        _workflow = workflow;
        _exportService = exportService;
    }

    public GeneratedRscPreviewDto GeneratePreview(BasicSetupInputDto input)
    {
        var result = _workflow.Generate(ToRequest(input));

        return new GeneratedRscPreviewDto(
            result.IsSuccess,
            result.RscText,
            result.Validation.Issues.Select(ToDto).ToArray());
    }

    public Task SaveRscAsync(string path, string rscText, CancellationToken cancellationToken = default)
    {
        return _exportService.SaveTextAsync(path, rscText, cancellationToken);
    }

    private static BasicSetupRequest ToRequest(BasicSetupInputDto input)
    {
        return new BasicSetupRequest
        {
            RouterName = input.RouterName,
            RouterOsVersion = input.RouterOsVersion.Contains('6')
                ? RouterOsMajorVersion.V6
                : RouterOsMajorVersion.V7,
            WanInterface = input.WanInterface,
            LanBridgeName = input.LanBridgeName,
            LanAddress = input.LanAddress,
            LanPrefixLength = input.LanPrefixLength,
            DhcpPoolStart = input.DhcpPoolStart,
            DhcpPoolEnd = input.DhcpPoolEnd,
            DnsServers = input.DnsServers,
            AdminUserName = input.AdminUserName,
            AdminPassword = input.AdminPassword,
            EnableNat = input.EnableNat,
            EnableBasicFirewall = input.EnableBasicFirewall
        };
    }

    private static ValidationIssueDto ToDto(ValidationIssue issue)
    {
        return new ValidationIssueDto(
            ToSeverityLabel(issue.Severity),
            issue.Field,
            issue.Message);
    }

    private static string ToSeverityLabel(ValidationSeverity severity)
    {
        return severity switch
        {
            ValidationSeverity.Error => "Ошибка",
            ValidationSeverity.Warning => "Предупреждение",
            _ => "Информация"
        };
    }
}
