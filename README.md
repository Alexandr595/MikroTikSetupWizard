# MikroTik Setup Wizard

Windows desktop MVP-приложение для генерации базового RouterOS `.rsc` файла.

Текущий функционал зафиксирован:

- ввод базовых параметров;
- профиль автозаполнения "Малый офис";
- валидация;
- предпросмотр `.rsc`;
- сохранение `.rsc` файла.

## Требования

Нужно установить:

- Windows 10/11;
- .NET SDK 8.0.x;
- Visual Studio 2022 версии 17.8 или новее;
- workload Visual Studio: `.NET desktop development`.

Проект использует:

- WPF;
- `net8.0-windows` для desktop-приложения;
- `net8.0` для остальных библиотек;
- C# 12.0.

Внешних NuGet-зависимостей сейчас нет.

## Как открыть в Visual Studio

1. Откройте Visual Studio 2022.
2. Выберите `Open a project or solution`.
3. Откройте файл `MikroTikSetupWizard.sln`.
4. В качестве стартового проекта выберите `MikroTikSetupWizard.Desktop`.

## Как собрать

Из корня репозитория:

```powershell
dotnet build .\MikroTikSetupWizard.sln -c Debug
```

Для release-сборки:

```powershell
dotnet build .\MikroTikSetupWizard.sln -c Release
```

## Как запустить

Из корня репозитория:

```powershell
dotnet run --project .\src\MikroTikSetupWizard.Desktop\MikroTikSetupWizard.Desktop.csproj -c Debug
```

Также можно запустить из Visual Studio кнопкой `Start`, если стартовый проект установлен на `MikroTikSetupWizard.Desktop`.

## Структура решения

```text
src/
  MikroTikSetupWizard.Desktop/        WPF UI
  MikroTikSetupWizard.Application/    workflow, validation, generation interfaces
  MikroTikSetupWizard.Domain/         domain models and configuration plan
  MikroTikSetupWizard.Infrastructure/ file export and infrastructure interfaces
  MikroTikSetupWizard.RouterOs/       RouterOS .rsc renderer
  MikroTikSetupWizard.Modules/        module placeholders
  MikroTikSetupWizard.Shared/         shared small types

tests/
  MikroTikSetupWizard.Domain.Tests/
  MikroTikSetupWizard.Application.Tests/
  MikroTikSetupWizard.RouterOs.Tests/
```

## Проверка окружения

Проверьте установленный SDK:

```powershell
dotnet --list-sdks
```

В списке должна быть версия `8.0.x`.
