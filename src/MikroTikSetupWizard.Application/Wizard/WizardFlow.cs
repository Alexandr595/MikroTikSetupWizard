namespace MikroTikSetupWizard.Application.Wizard;

public sealed class WizardFlow
{
    public IReadOnlyList<WizardStep> Steps { get; } =
    [
        new WizardStep("basic", "Базовые параметры"),
        new WizardStep("preview", "Предпросмотр .rsc"),
        new WizardStep("export", "Сохранение файла")
    ];
}
