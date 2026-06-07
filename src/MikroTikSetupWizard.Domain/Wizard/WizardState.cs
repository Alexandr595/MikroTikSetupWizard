using MikroTikSetupWizard.Domain.SetupTasks;

namespace MikroTikSetupWizard.Domain.Wizard;

public sealed class WizardState
{
    public WizardState(
        SetupTaskId taskId,
        string currentStepId,
        IReadOnlyDictionary<string, string>? answers = null,
        WizardValidationResult? validation = null)
    {
        TaskId = taskId;
        CurrentStepId = currentStepId;
        Answers = answers ?? new Dictionary<string, string>();
        Validation = validation ?? WizardValidationResult.Success;
    }

    public SetupTaskId TaskId { get; }

    public string CurrentStepId { get; }

    public IReadOnlyDictionary<string, string> Answers { get; }

    public WizardValidationResult Validation { get; }
}
