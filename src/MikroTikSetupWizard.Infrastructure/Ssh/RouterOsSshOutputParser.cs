using System.Text.RegularExpressions;
using System.Text;
using MikroTikSetupWizard.Application.Connections;

namespace MikroTikSetupWizard.Infrastructure.Ssh;

internal static partial class RouterOsSshOutputParser
{
    public static string? ParseIdentity(string output)
    {
        return ParseKeyValues(output).GetValueOrDefault("name");
    }

    public static RouterOsResourceInfo ParseResource(string output)
    {
        var values = ParseKeyValues(output);

        return new RouterOsResourceInfo(
            Version: values.GetValueOrDefault("version"),
            Uptime: values.GetValueOrDefault("uptime"),
            BoardName: values.GetValueOrDefault("board-name"));
    }

    public static string? ParseBoardName(string output)
    {
        var values = ParseKeyValues(output);

        return FirstKnownValue(
            values.GetValueOrDefault("model"),
            values.GetValueOrDefault("board-name"));
    }

    public static IReadOnlyList<DeviceInterfaceDto> ParseInterfaces(string output)
    {
        var normalizedOutput = NormalizeOutput(output);
        var interfaces = new List<DeviceInterfaceDto>();

        foreach (var record in ReadInterfaceRecords(normalizedOutput))
        {
            var name = ReadQuotedOrUnquotedValue(record, "name");

            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            var type = ReadQuotedOrUnquotedValue(record, "type") ?? "unknown";
            var flags = ReadLeadingFlags(record);
            var disabledValue = ReadQuotedOrUnquotedValue(record, "disabled");
            var runningValue = ReadQuotedOrUnquotedValue(record, "running");

            interfaces.Add(new DeviceInterfaceDto(
                Name: name,
                Type: type,
                IsRunning: IsTrue(runningValue) || flags.Contains('R'),
                IsDisabled: IsTrue(disabledValue) || flags.Contains('X')));
        }

        return interfaces;
    }

    private static IReadOnlyList<string> ReadInterfaceRecords(string output)
    {
        var records = new List<string>();
        StringBuilder? currentRecord = null;

        foreach (var line in output.Split('\n'))
        {
            if (InterfaceRecordStartRegex().IsMatch(line))
            {
                if (currentRecord is not null)
                {
                    records.Add(currentRecord.ToString());
                }

                currentRecord = new StringBuilder(line.Trim());
                continue;
            }

            if (currentRecord is not null && !string.IsNullOrWhiteSpace(line))
            {
                currentRecord.Append(' ').Append(line.Trim());
            }
        }

        if (currentRecord is not null)
        {
            records.Add(currentRecord.ToString());
        }

        return records;
    }

    private static IReadOnlyDictionary<string, string> ParseKeyValues(string output)
    {
        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in NormalizeOutput(output)
                     .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var separatorIndex = line.IndexOf(':');

            if (separatorIndex <= 0 || separatorIndex == line.Length - 1)
            {
                continue;
            }

            var key = line[..separatorIndex].Trim();
            var value = line[(separatorIndex + 1)..].Trim().Trim('"');

            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
            {
                values[key] = value;
            }
        }

        return values;
    }

    private static string NormalizeOutput(string output)
    {
        if (string.IsNullOrEmpty(output))
        {
            return string.Empty;
        }

        return AnsiEscapeRegex()
            .Replace(output.Replace("\r\n", "\n", StringComparison.Ordinal), string.Empty)
            .Replace('\r', '\n');
    }

    private static string? ReadQuotedOrUnquotedValue(string record, string key)
    {
        var match = Regex.Match(
            record,
            $@"(?:^|\s){Regex.Escape(key)}=(?:""(?<quoted>[^""]*)""|(?<plain>\S+))",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        if (!match.Success)
        {
            return null;
        }

        var value = match.Groups["quoted"].Success
            ? match.Groups["quoted"].Value
            : match.Groups["plain"].Value;

        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    private static string ReadLeadingFlags(string record)
    {
        var firstLine = record
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .FirstOrDefault();

        if (string.IsNullOrWhiteSpace(firstLine))
        {
            return string.Empty;
        }

        var match = LeadingFlagsRegex().Match(firstLine);
        return match.Success ? match.Groups["flags"].Value : string.Empty;
    }

    private static bool IsTrue(string? value)
    {
        return string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase)
            || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
    }

    private static string? FirstKnownValue(params string?[] values)
    {
        return values.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
    }

    [GeneratedRegex(@"\x1B(?:[@-Z\\-_]|\[[0-?]*[ -/]*[@-~])")]
    private static partial Regex AnsiEscapeRegex();

    [GeneratedRegex(@"^\s*\d+\s+", RegexOptions.CultureInvariant)]
    private static partial Regex InterfaceRecordStartRegex();

    [GeneratedRegex(@"^(?:\d+\s+)?(?<flags>[A-Z*]+)(?:\s|$)")]
    private static partial Regex LeadingFlagsRegex();
}

internal sealed record RouterOsResourceInfo(
    string? Version,
    string? Uptime,
    string? BoardName);
