using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Wizard;

public sealed class WizardSession
{
    public BasicSetupRequest? CurrentRequest { get; private set; }

    public void Update(BasicSetupRequest request)
    {
        CurrentRequest = request;
    }
}
