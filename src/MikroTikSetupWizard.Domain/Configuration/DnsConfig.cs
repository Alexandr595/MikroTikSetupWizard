namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record DnsConfig(IReadOnlyList<string> Servers, bool AllowRemoteRequests);
