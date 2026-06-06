using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Domain.Modules;

public sealed class ModulePreview
{
    public ModulePreview(
        ModuleId moduleId,
        string title,
        IReadOnlyCollection<ConfigurationCommand>? commands = null,
        IReadOnlyCollection<string>? notes = null)
    {
        ModuleId = moduleId;
        Title = title;
        Commands = commands ?? Array.Empty<ConfigurationCommand>();
        Notes = notes ?? Array.Empty<string>();
    }

    public ModuleId ModuleId { get; }

    public string Title { get; }

    public IReadOnlyCollection<ConfigurationCommand> Commands { get; }

    public IReadOnlyCollection<string> Notes { get; }
}
