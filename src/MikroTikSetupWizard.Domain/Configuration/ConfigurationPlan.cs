using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Domain.Configuration;

public sealed class ConfigurationPlan
{
    private readonly List<ConfigurationCommand> _commands = new();

    public ConfigurationPlan(string name, RouterOsMajorVersion routerOsVersion)
    {
        Name = name;
        RouterOsVersion = routerOsVersion;
    }

    public string Name { get; }

    public RouterOsMajorVersion RouterOsVersion { get; }

    public IReadOnlyList<ConfigurationCommand> Commands => _commands;

    public void Add(ConfigurationCommand command)
    {
        _commands.Add(command);
    }
}
