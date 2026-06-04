namespace MikroTikSetupWizard.Application.Setup;

public sealed record GeneratedRscPreviewDto(
    bool IsSuccess,
    string RscText,
    IReadOnlyList<ValidationIssueDto> Issues);
