namespace MikroTikSetupWizard.Application.ConfigurationApply;

public interface IConfigurationApplyService
{
    Task<ConfigurationApplyResult> ApplyAsync(
        DeviceConnectionProfile connectionProfile,
        ConfigurationApplyPlan plan,
        CancellationToken cancellationToken = default);
}
