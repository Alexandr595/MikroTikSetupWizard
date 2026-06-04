# Архитектура

Проект разделён на слои:

- `MikroTikSetupWizard.Desktop` — WPF UI, русская тёмная тема, MVVM.
- `MikroTikSetupWizard.Application` — workflow, валидация, сборка плана, интерфейсы генерации и экспорта.
- `MikroTikSetupWizard.Domain` — модели предметной области, план конфигурации, validation result.
- `MikroTikSetupWizard.RouterOs` — capabilities RouterOS 6/7 и renderer `.rsc`.
- `MikroTikSetupWizard.Infrastructure` — сохранение файлов, будущие интерфейсы SSH/API/persistence.
- `MikroTikSetupWizard.Modules` — заготовки будущих функциональных модулей.
- `MikroTikSetupWizard.Shared` — общие небольшие типы.

Основной поток:

`WizardViewModel` -> `BasicSetupWorkflow` -> `ValidationService` -> `ConfigurationBuilder` -> `RouterOsRscRenderer` -> `FileExportService`.

UI не формирует RouterOS-команды напрямую. Он передаёт пользовательский ввод в application layer, где создаётся `ConfigurationPlan`.
