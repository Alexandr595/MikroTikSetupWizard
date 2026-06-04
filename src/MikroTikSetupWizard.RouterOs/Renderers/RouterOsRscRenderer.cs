using System.Text;
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.RouterOs.Capabilities;
using MikroTikSetupWizard.RouterOs.Versions;

namespace MikroTikSetupWizard.RouterOs.Renderers;

public sealed class RouterOsRscRenderer : IConfigurationRenderer
{
    public string Render(ConfigurationPlan plan)
    {
        _ = RouterOsCapabilities.For(plan.RouterOsVersion);
        var syntaxPolicy = new RouterOsSyntaxPolicy(plan.RouterOsVersion);
        var builder = new StringBuilder();

        builder.AppendLine("# MikroTik Setup Wizard");
        builder.AppendLine($"# Target: {syntaxPolicy.HeaderVersionLabel}");
        builder.AppendLine($"# Profile: {EscapeComment(plan.Name)}");
        builder.AppendLine("# Generated configuration preview");
        builder.AppendLine();

        foreach (var command in plan.Commands)
        {
            if (!string.IsNullOrWhiteSpace(command.Comment))
            {
                builder.AppendLine($"# {EscapeComment(command.Comment)}");
            }

            builder.Append('/');
            builder.Append(command.Section);
            builder.Append(' ');
            builder.Append(command.Operation);

            if (!string.IsNullOrWhiteSpace(command.Selector))
            {
                builder.Append(' ');
                builder.Append(command.Selector);
            }

            foreach (var parameter in command.Parameters)
            {
                builder.Append(' ');
                builder.Append(parameter.Name);

                if (parameter.Value is not null)
                {
                    builder.Append('=');
                    builder.Append(FormatValue(parameter.Value));
                }
            }

            builder.AppendLine();
            builder.AppendLine();
        }

        return builder.ToString().TrimEnd() + Environment.NewLine;
    }

    private static string FormatValue(string value)
    {
        if (CanBeUnquoted(value))
        {
            return value;
        }

        return $"\"{EscapeQuoted(value)}\"";
    }

    private static bool CanBeUnquoted(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return value.All(character =>
            char.IsLetterOrDigit(character)
            || character is '.' or '-' or '_' or '/' or ':' or ',' or '+');
    }

    private static string EscapeQuoted(string value)
    {
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }

    private static string EscapeComment(string value)
    {
        return value.ReplaceLineEndings(" ").Trim();
    }
}
