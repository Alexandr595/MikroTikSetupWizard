namespace MikroTikSetupWizard.Application.Setup;

public interface IMikroTikSetupWizardService
{
    GeneratedRscPreviewDto GeneratePreview(BasicSetupInputDto input);

    Task SaveRscAsync(string path, string rscText, CancellationToken cancellationToken = default);
}
