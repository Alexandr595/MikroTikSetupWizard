using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Domain.Models;

public sealed record RouterProfile(
    string Name,
    RouterOsMajorVersion RouterOsVersion);
