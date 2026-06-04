namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record ConfigurationCommand(
    string Section,
    string Operation,
    IReadOnlyList<ConfigurationParameter> Parameters,
    string? Selector = null,
    string? Comment = null);
