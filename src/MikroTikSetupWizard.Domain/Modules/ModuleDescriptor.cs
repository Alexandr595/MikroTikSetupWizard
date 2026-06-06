namespace MikroTikSetupWizard.Domain.Modules;

public sealed class ModuleDescriptor
{
    public ModuleDescriptor(
        ModuleId moduleId,
        string name,
        string description,
        IReadOnlyCollection<ModuleDependency>? dependencies = null)
    {
        ModuleId = moduleId;
        Name = name;
        Description = description;
        Dependencies = dependencies ?? Array.Empty<ModuleDependency>();
    }

    public ModuleId ModuleId { get; }

    public string Name { get; }

    public string Description { get; }

    public IReadOnlyCollection<ModuleDependency> Dependencies { get; }
}
