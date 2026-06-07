using MikroTikSetupWizard.Domain.DeviceRoles;
using MikroTikSetupWizard.Domain.Modules;
using MikroTikSetupWizard.Domain.Wizard;

namespace MikroTikSetupWizard.Domain.SetupTasks;

public sealed class SetupTask
{
    public SetupTask(
        SetupTaskId taskId,
        string name,
        string description,
        DeviceRole deviceRole,
        IReadOnlyCollection<ModuleId> requiredModules,
        WizardDefinition wizardDefinition)
    {
        TaskId = taskId;
        Name = name;
        Description = description;
        DeviceRole = deviceRole;
        RequiredModules = requiredModules;
        WizardDefinition = wizardDefinition;
    }

    public SetupTaskId TaskId { get; }

    public string Name { get; }

    public string Description { get; }

    public DeviceRole DeviceRole { get; }

    public IReadOnlyCollection<ModuleId> RequiredModules { get; }

    public WizardDefinition WizardDefinition { get; }
}
