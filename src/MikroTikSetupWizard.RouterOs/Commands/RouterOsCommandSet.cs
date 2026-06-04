namespace MikroTikSetupWizard.RouterOs.Commands;

public sealed class RouterOsCommandSet
{
    private readonly List<RouterOsCommand> _commands = new();

    public IReadOnlyList<RouterOsCommand> Commands => _commands;

    public void Add(RouterOsCommand command)
    {
        _commands.Add(command);
    }
}
