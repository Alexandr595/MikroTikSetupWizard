# MikroTik Setup Wizard - полный экспорт исходников

Файлы перечислены по одному. Пути указаны относительно корня проекта.

## Список файлов

- MikroTikSetupWizard.sln
- Directory.Build.props
- README.md
- src\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj
- src\MikroTikSetupWizard.Desktop\MikroTikSetupWizard.Desktop.csproj
- src\MikroTikSetupWizard.Domain\MikroTikSetupWizard.Domain.csproj
- src\MikroTikSetupWizard.Infrastructure\MikroTikSetupWizard.Infrastructure.csproj
- src\MikroTikSetupWizard.Modules\MikroTikSetupWizard.Modules.csproj
- src\MikroTikSetupWizard.RouterOs\MikroTikSetupWizard.RouterOs.csproj
- src\MikroTikSetupWizard.Shared\MikroTikSetupWizard.Shared.csproj
- src\MikroTikSetupWizard.Desktop\App.xaml
- src\MikroTikSetupWizard.Desktop\Views\MainWindow.xaml
- src\MikroTikSetupWizard.Application\Setup\BasicSetupInputDto.cs
- src\MikroTikSetupWizard.Application\Setup\GeneratedRscPreviewDto.cs
- src\MikroTikSetupWizard.Application\Setup\ValidationIssueDto.cs
- src\MikroTikSetupWizard.Application\Export\DefaultExportService.cs
- src\MikroTikSetupWizard.Application\Export\IExportService.cs
- src\MikroTikSetupWizard.Application\Export\RscExportService.cs
- src\MikroTikSetupWizard.Application\Profiles\RouterProfileService.cs
- src\MikroTikSetupWizard.Application\Services\BasicSetupWorkflow.cs
- src\MikroTikSetupWizard.Application\Services\ConfigurationPlanService.cs
- src\MikroTikSetupWizard.Application\Services\SetupScenarioService.cs
- src\MikroTikSetupWizard.Application\Setup\IMikroTikSetupWizardService.cs
- src\MikroTikSetupWizard.Application\Setup\MikroTikSetupWizardService.cs
- src\MikroTikSetupWizard.Application\Validation\ValidationService.cs
- src\MikroTikSetupWizard.Desktop\Dialogs\DialogService.cs
- src\MikroTikSetupWizard.Desktop\Dialogs\IDialogService.cs
- src\MikroTikSetupWizard.Desktop\Dialogs\ISaveFileDialogService.cs
- src\MikroTikSetupWizard.Desktop\Dialogs\SaveFileDialogService.cs
- src\MikroTikSetupWizard.Desktop\Navigation\INavigationService.cs
- src\MikroTikSetupWizard.Desktop\Navigation\NavigationService.cs
- src\MikroTikSetupWizard.Infrastructure\Export\FileExportService.cs
- src\MikroTikSetupWizard.Infrastructure\FileSystem\IFileSystemService.cs
- src\MikroTikSetupWizard.Infrastructure\Logging\ILogService.cs
- src\MikroTikSetupWizard.Desktop\ViewModels\ObservableObject.cs
- src\MikroTikSetupWizard.Desktop\ViewModels\RelayCommand.cs
- src\MikroTikSetupWizard.Desktop\ViewModels\ShellViewModel.cs
- src\MikroTikSetupWizard.Desktop\ViewModels\WizardStepViewModel.cs
- src\MikroTikSetupWizard.Desktop\ViewModels\WizardViewModel.cs
- docs\architecture.md
- docs\module-design.md
- docs\routeros6-routeros7-notes.md
- src\MikroTikSetupWizard.Application\Generation\ConfigurationBuilder.cs
- src\MikroTikSetupWizard.Application\Generation\GeneratedConfiguration.cs
- src\MikroTikSetupWizard.Application\Generation\IConfigurationBuilder.cs
- src\MikroTikSetupWizard.Application\Generation\IConfigurationRenderer.cs
- src\MikroTikSetupWizard.Application\Generation\RscConfigurationRenderer.cs
- src\MikroTikSetupWizard.Application\Profiles\IRouterProfileRepository.cs
- src\MikroTikSetupWizard.Application\Validation\IConfigurationValidator.cs
- src\MikroTikSetupWizard.Application\Wizard\ISetupModule.cs
- src\MikroTikSetupWizard.Application\Wizard\WizardFlow.cs
- src\MikroTikSetupWizard.Application\Wizard\WizardSession.cs
- src\MikroTikSetupWizard.Application\Wizard\WizardStep.cs
- src\MikroTikSetupWizard.Desktop\App.xaml.cs
- src\MikroTikSetupWizard.Desktop\Controls\WizardStepHeader.cs
- src\MikroTikSetupWizard.Desktop\Themes\DarkTheme.xaml
- src\MikroTikSetupWizard.Desktop\Themes\ThemeManager.cs
- src\MikroTikSetupWizard.Desktop\Views\MainWindow.xaml.cs
- src\MikroTikSetupWizard.Domain\Configuration\BridgeConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\ConfigurationCommand.cs
- src\MikroTikSetupWizard.Domain\Configuration\ConfigurationParameter.cs
- src\MikroTikSetupWizard.Domain\Configuration\ConfigurationPlan.cs
- src\MikroTikSetupWizard.Domain\Configuration\DhcpConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\DnsConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\FirewallConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\IpAddressConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\NatConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\UserAccountConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\VlanConfig.cs
- src\MikroTikSetupWizard.Domain\Configuration\WirelessConfig.cs
- src\MikroTikSetupWizard.Domain\Models\Ipv4AddressMath.cs
- src\MikroTikSetupWizard.Domain\Models\NetworkInterface.cs
- src\MikroTikSetupWizard.Domain\Models\RouterProfile.cs
- src\MikroTikSetupWizard.Domain\RouterOs\RouterOsGeneration.cs
- src\MikroTikSetupWizard.Domain\RouterOs\RouterOsMajorVersion.cs
- src\MikroTikSetupWizard.Domain\RouterOs\RouterOsVersion.cs
- src\MikroTikSetupWizard.Domain\Scenarios\BasicSetupRequest.cs
- src\MikroTikSetupWizard.Domain\Validation\ValidationIssue.cs
- src\MikroTikSetupWizard.Domain\Validation\ValidationResult.cs
- src\MikroTikSetupWizard.Domain\Validation\ValidationSeverity.cs
- src\MikroTikSetupWizard.Infrastructure\Api\IRouterApiClient.cs
- src\MikroTikSetupWizard.Infrastructure\Api\IRouterApiSession.cs
- src\MikroTikSetupWizard.Infrastructure\Persistence\IRouterProfileStore.cs
- src\MikroTikSetupWizard.Infrastructure\Settings\AppSettings.cs
- src\MikroTikSetupWizard.Infrastructure\Settings\IAppSettingsRepository.cs
- src\MikroTikSetupWizard.Infrastructure\Ssh\IRouterSshClient.cs
- src\MikroTikSetupWizard.Infrastructure\Ssh\IRouterSshSession.cs
- src\MikroTikSetupWizard.Modules\Backup\BackupSetupModule.cs
- src\MikroTikSetupWizard.Modules\Dhcp\DhcpSetupModule.cs
- src\MikroTikSetupWizard.Modules\Dns\DnsSetupModule.cs
- src\MikroTikSetupWizard.Modules\Firewall\FirewallSetupModule.cs
- src\MikroTikSetupWizard.Modules\Internet\InternetSetupModule.cs
- src\MikroTikSetupWizard.Modules\Lan\LanSetupModule.cs
- src\MikroTikSetupWizard.Modules\ModuleStub.cs
- src\MikroTikSetupWizard.Modules\Nat\NatSetupModule.cs
- src\MikroTikSetupWizard.Modules\Security\SecurityHardeningModule.cs
- src\MikroTikSetupWizard.Modules\Users\UsersSetupModule.cs
- src\MikroTikSetupWizard.Modules\Vlans\VlanSetupModule.cs
- src\MikroTikSetupWizard.Modules\Wan\WanSetupModule.cs
- src\MikroTikSetupWizard.Modules\Wireless\WirelessSetupModule.cs
- src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOs6Capabilities.cs
- src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOs7Capabilities.cs
- src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOsCapabilities.cs
- src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOsFeatureSupport.cs
- src\MikroTikSetupWizard.RouterOs\Commands\RouterOsCommand.cs
- src\MikroTikSetupWizard.RouterOs\Commands\RouterOsCommandSet.cs
- src\MikroTikSetupWizard.RouterOs\Renderers\RouterOs6RscRenderer.cs
- src\MikroTikSetupWizard.RouterOs\Renderers\RouterOs7RscRenderer.cs
- src\MikroTikSetupWizard.RouterOs\Renderers\RouterOsRscRenderer.cs
- src\MikroTikSetupWizard.RouterOs\Versions\RouterOsSyntaxPolicy.cs
- src\MikroTikSetupWizard.Shared\Result.cs
- tests\MikroTikSetupWizard.Application.Tests\README.md
- tests\MikroTikSetupWizard.Domain.Tests\README.md
- tests\MikroTikSetupWizard.RouterOs.Tests\README.md

## Содержимое файлов

### MikroTikSetupWizard.sln

```text
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.Desktop", "src\MikroTikSetupWizard.Desktop\MikroTikSetupWizard.Desktop.csproj", "{E6945E4E-5D52-4E2C-9A6A-087B0E05D701}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.Application", "src\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj", "{B0F9C874-4F79-4D22-AF92-30BA21E8C901}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.Domain", "src\MikroTikSetupWizard.Domain\MikroTikSetupWizard.Domain.csproj", "{CE9E7EC5-92F0-45AA-A5C7-EBAD9017F6D1}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.Infrastructure", "src\MikroTikSetupWizard.Infrastructure\MikroTikSetupWizard.Infrastructure.csproj", "{2BCB65DE-8838-43B9-A03D-FE96440438B9}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.RouterOs", "src\MikroTikSetupWizard.RouterOs\MikroTikSetupWizard.RouterOs.csproj", "{60C553F3-674F-4C7F-B56C-BAB4A51C29B4}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.Modules", "src\MikroTikSetupWizard.Modules\MikroTikSetupWizard.Modules.csproj", "{1EAB4AB8-1676-4F13-8891-E81DFF19BB0C}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "MikroTikSetupWizard.Shared", "src\MikroTikSetupWizard.Shared\MikroTikSetupWizard.Shared.csproj", "{2F3846D9-73A7-401C-90B8-E599A16E38B5}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{E6945E4E-5D52-4E2C-9A6A-087B0E05D701}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{E6945E4E-5D52-4E2C-9A6A-087B0E05D701}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{E6945E4E-5D52-4E2C-9A6A-087B0E05D701}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{E6945E4E-5D52-4E2C-9A6A-087B0E05D701}.Release|Any CPU.Build.0 = Release|Any CPU
		{B0F9C874-4F79-4D22-AF92-30BA21E8C901}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B0F9C874-4F79-4D22-AF92-30BA21E8C901}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B0F9C874-4F79-4D22-AF92-30BA21E8C901}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B0F9C874-4F79-4D22-AF92-30BA21E8C901}.Release|Any CPU.Build.0 = Release|Any CPU
		{CE9E7EC5-92F0-45AA-A5C7-EBAD9017F6D1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{CE9E7EC5-92F0-45AA-A5C7-EBAD9017F6D1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{CE9E7EC5-92F0-45AA-A5C7-EBAD9017F6D1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{CE9E7EC5-92F0-45AA-A5C7-EBAD9017F6D1}.Release|Any CPU.Build.0 = Release|Any CPU
		{2BCB65DE-8838-43B9-A03D-FE96440438B9}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{2BCB65DE-8838-43B9-A03D-FE96440438B9}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{2BCB65DE-8838-43B9-A03D-FE96440438B9}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{2BCB65DE-8838-43B9-A03D-FE96440438B9}.Release|Any CPU.Build.0 = Release|Any CPU
		{60C553F3-674F-4C7F-B56C-BAB4A51C29B4}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{60C553F3-674F-4C7F-B56C-BAB4A51C29B4}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{60C553F3-674F-4C7F-B56C-BAB4A51C29B4}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{60C553F3-674F-4C7F-B56C-BAB4A51C29B4}.Release|Any CPU.Build.0 = Release|Any CPU
		{1EAB4AB8-1676-4F13-8891-E81DFF19BB0C}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{1EAB4AB8-1676-4F13-8891-E81DFF19BB0C}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{1EAB4AB8-1676-4F13-8891-E81DFF19BB0C}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{1EAB4AB8-1676-4F13-8891-E81DFF19BB0C}.Release|Any CPU.Build.0 = Release|Any CPU
		{2F3846D9-73A7-401C-90B8-E599A16E38B5}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{2F3846D9-73A7-401C-90B8-E599A16E38B5}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{2F3846D9-73A7-401C-90B8-E599A16E38B5}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{2F3846D9-73A7-401C-90B8-E599A16E38B5}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal
```

### Directory.Build.props

```xml
<Project>
  <PropertyGroup>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <LangVersion>12.0</LangVersion>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
  </PropertyGroup>
</Project>
```

### README.md

```markdown
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
```

### src\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AssemblyName>MikroTikSetupWizard.Application</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.Application</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\MikroTikSetupWizard.Domain\MikroTikSetupWizard.Domain.csproj" />
  </ItemGroup>
</Project>
```

### src\MikroTikSetupWizard.Desktop\MikroTikSetupWizard.Desktop.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
    <AssemblyName>MikroTikSetupWizard.Desktop</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.Desktop</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj" />
  </ItemGroup>
</Project>
```

### src\MikroTikSetupWizard.Domain\MikroTikSetupWizard.Domain.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AssemblyName>MikroTikSetupWizard.Domain</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.Domain</RootNamespace>
  </PropertyGroup>
</Project>
```

### src\MikroTikSetupWizard.Infrastructure\MikroTikSetupWizard.Infrastructure.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AssemblyName>MikroTikSetupWizard.Infrastructure</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.Infrastructure</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj" />
    <ProjectReference Include="..\MikroTikSetupWizard.Domain\MikroTikSetupWizard.Domain.csproj" />
  </ItemGroup>
</Project>
```

### src\MikroTikSetupWizard.Modules\MikroTikSetupWizard.Modules.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AssemblyName>MikroTikSetupWizard.Modules</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.Modules</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj" />
  </ItemGroup>
</Project>
```

### src\MikroTikSetupWizard.RouterOs\MikroTikSetupWizard.RouterOs.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AssemblyName>MikroTikSetupWizard.RouterOs</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.RouterOs</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\MikroTikSetupWizard.Application\MikroTikSetupWizard.Application.csproj" />
    <ProjectReference Include="..\MikroTikSetupWizard.Domain\MikroTikSetupWizard.Domain.csproj" />
  </ItemGroup>
</Project>
```

### src\MikroTikSetupWizard.Shared\MikroTikSetupWizard.Shared.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AssemblyName>MikroTikSetupWizard.Shared</AssemblyName>
    <RootNamespace>MikroTikSetupWizard.Shared</RootNamespace>
  </PropertyGroup>
</Project>
```

### src\MikroTikSetupWizard.Desktop\App.xaml

```xml
<Application x:Class="MikroTikSetupWizard.Desktop.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Themes/DarkTheme.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### src\MikroTikSetupWizard.Desktop\Views\MainWindow.xaml

```xml
<Window x:Class="MikroTikSetupWizard.Desktop.Views.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MikroTik Setup Wizard"
        Width="1180"
        Height="760"
        MinWidth="980"
        MinHeight="620"
        Background="{DynamicResource AppBackgroundBrush}">
    <Grid Margin="22">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="420" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>

        <Border Grid.Column="0"
                Background="{DynamicResource PanelBackgroundBrush}"
                BorderBrush="{DynamicResource BorderBrush}"
                BorderThickness="1"
                Padding="18">
            <ScrollViewer VerticalScrollBarVisibility="Auto">
                <StackPanel>
                    <TextBlock Text="MikroTik Setup Wizard" Style="{StaticResource TitleTextStyle}" />
                    <TextBlock Text="Первичная настройка RouterOS" Foreground="{DynamicResource MutedTextBrush}" />

                    <TextBlock Text="Устройство" Style="{StaticResource SectionTitleStyle}" />

                    <Button Content="Профиль: Малый офис"
                            Style="{StaticResource SecondaryButtonStyle}"
                            Command="{Binding ApplySmallOfficeProfileCommand}" />

                    <TextBlock Text="Имя роутера" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding RouterName, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="Версия RouterOS" Style="{StaticResource FieldLabelStyle}" />
                    <ComboBox ItemsSource="{Binding RouterOsVersions}"
                              SelectedItem="{Binding SelectedRouterOsVersion}" />

                    <TextBlock Text="Интерфейсы" Style="{StaticResource SectionTitleStyle}" />

                    <TextBlock Text="WAN-интерфейс" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding WanInterface, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="LAN bridge" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding LanBridgeName, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="LAN" Style="{StaticResource SectionTitleStyle}" />

                    <TextBlock Text="IP/CIDR LAN gateway" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding LanAddress, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="Префикс сети" Style="{StaticResource FieldLabelStyle}" />
                    <ComboBox ItemsSource="{Binding PrefixLengths}"
                              SelectedItem="{Binding SelectedPrefixLength}" />

                    <TextBlock Text="Начало DHCP-пула" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding DhcpPoolStart, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="Конец DHCP-пула" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding DhcpPoolEnd, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="DNS-серверы" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding DnsServers, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="Администратор" Style="{StaticResource SectionTitleStyle}" />

                    <TextBlock Text="Имя администратора" Style="{StaticResource FieldLabelStyle}" />
                    <TextBox Text="{Binding AdminUserName, UpdateSourceTrigger=PropertyChanged}" />

                    <TextBlock Text="Пароль администратора" Style="{StaticResource FieldLabelStyle}" />
                    <PasswordBox x:Name="AdminPasswordBox"
                                 PasswordChanged="AdminPasswordBox_OnPasswordChanged" />

                    <CheckBox Content="Включить NAT masquerade"
                              IsChecked="{Binding EnableNat}" />
                    <CheckBox Content="Добавить базовый firewall"
                              IsChecked="{Binding EnableBasicFirewall}" />

                    <StackPanel Orientation="Horizontal" Margin="0,8,0,0">
                        <Button Content="Сгенерировать"
                                Command="{Binding GeneratePreviewCommand}" />
                        <Button Content="Сохранить .rsc"
                                Style="{StaticResource SecondaryButtonStyle}"
                                Command="{Binding SaveFileCommand}" />
                    </StackPanel>

                    <TextBlock Text="{Binding StatusMessage}"
                               Foreground="{DynamicResource SuccessBrush}"
                               Margin="0,14,0,0" />

                    <TextBlock Text="{Binding ValidationMessage}"
                               Foreground="{DynamicResource DangerBrush}"
                               Margin="0,8,0,0" />
                </StackPanel>
            </ScrollViewer>
        </Border>

        <Grid Grid.Column="1" Margin="18,0,0,0">
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="*" />
            </Grid.RowDefinitions>

            <StackPanel Grid.Row="0" Margin="0,0,0,12">
                <TextBlock Text="Предпросмотр .rsc" Style="{StaticResource TitleTextStyle}" />
                <TextBlock Text="Скрипт генерируется из ConfigurationPlan и готов к сохранению."
                           Foreground="{DynamicResource MutedTextBrush}" />
            </StackPanel>

            <TextBox Grid.Row="1"
                     Text="{Binding GeneratedRsc, Mode=OneWay}"
                     FontFamily="Consolas"
                     FontSize="13"
                     AcceptsReturn="True"
                     AcceptsTab="True"
                     IsReadOnly="True"
                     HorizontalScrollBarVisibility="Auto"
                     VerticalScrollBarVisibility="Auto" />
        </Grid>
    </Grid>
</Window>
```

### src\MikroTikSetupWizard.Application\Setup\BasicSetupInputDto.cs

```csharp
namespace MikroTikSetupWizard.Application.Setup;

public sealed class BasicSetupInputDto
{
    public string RouterName { get; init; } = string.Empty;

    public string RouterOsVersion { get; init; } = string.Empty;

    public string WanInterface { get; init; } = string.Empty;

    public string LanBridgeName { get; init; } = string.Empty;

    public string LanAddress { get; init; } = string.Empty;

    public int LanPrefixLength { get; init; }

    public string DhcpPoolStart { get; init; } = string.Empty;

    public string DhcpPoolEnd { get; init; } = string.Empty;

    public string DnsServers { get; init; } = string.Empty;

    public string AdminUserName { get; init; } = string.Empty;

    public string AdminPassword { get; init; } = string.Empty;

    public bool EnableNat { get; init; }

    public bool EnableBasicFirewall { get; init; }
}
```

### src\MikroTikSetupWizard.Application\Setup\GeneratedRscPreviewDto.cs

```csharp
namespace MikroTikSetupWizard.Application.Setup;

public sealed record GeneratedRscPreviewDto(
    bool IsSuccess,
    string RscText,
    IReadOnlyList<ValidationIssueDto> Issues);
```

### src\MikroTikSetupWizard.Application\Setup\ValidationIssueDto.cs

```csharp
namespace MikroTikSetupWizard.Application.Setup;

public sealed record ValidationIssueDto(
    string Severity,
    string Field,
    string Message);
```

### src\MikroTikSetupWizard.Application\Export\DefaultExportService.cs

```csharp
using System.Text;

namespace MikroTikSetupWizard.Application.Export;

internal sealed class DefaultExportService : IExportService
{
    public async Task SaveTextAsync(string path, string content, CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(path, content, Encoding.UTF8, cancellationToken);
    }
}
```

### src\MikroTikSetupWizard.Application\Export\IExportService.cs

```csharp
namespace MikroTikSetupWizard.Application.Export;

public interface IExportService
{
    Task SaveTextAsync(string path, string content, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Application\Export\RscExportService.cs

```csharp
namespace MikroTikSetupWizard.Application.Export;

public sealed class RscExportService
{
    private readonly IExportService _exportService;

    public RscExportService(IExportService exportService)
    {
        _exportService = exportService;
    }

    public Task SaveAsync(string path, string rscText, CancellationToken cancellationToken = default)
    {
        return _exportService.SaveTextAsync(path, rscText, cancellationToken);
    }
}
```

### src\MikroTikSetupWizard.Application\Profiles\RouterProfileService.cs

```csharp
using MikroTikSetupWizard.Domain.Models;

namespace MikroTikSetupWizard.Application.Profiles;

public sealed class RouterProfileService
{
    private readonly IRouterProfileRepository _repository;

    public RouterProfileService(IRouterProfileRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<RouterProfile>> GetProfilesAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}
```

### src\MikroTikSetupWizard.Application\Services\BasicSetupWorkflow.cs

```csharp
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Application.Validation;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Services;

public sealed class BasicSetupWorkflow
{
    private readonly IConfigurationValidator<BasicSetupRequest> _validator;
    private readonly IConfigurationBuilder _builder;
    private readonly IConfigurationRenderer _renderer;

    public BasicSetupWorkflow(
        IConfigurationValidator<BasicSetupRequest> validator,
        IConfigurationBuilder builder,
        IConfigurationRenderer renderer)
    {
        _validator = validator;
        _builder = builder;
        _renderer = renderer;
    }

    public GeneratedConfiguration Generate(BasicSetupRequest request)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
        {
            return new GeneratedConfiguration(validation, null, string.Empty);
        }

        var plan = _builder.Build(request);
        var rscText = _renderer.Render(plan);

        return new GeneratedConfiguration(validation, plan, rscText);
    }
}
```

### src\MikroTikSetupWizard.Application\Services\ConfigurationPlanService.cs

```csharp
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Services;

public sealed class ConfigurationPlanService
{
    private readonly IConfigurationBuilder _builder;

    public ConfigurationPlanService(IConfigurationBuilder builder)
    {
        _builder = builder;
    }

    public ConfigurationPlan Build(BasicSetupRequest request)
    {
        return _builder.Build(request);
    }
}
```

### src\MikroTikSetupWizard.Application\Services\SetupScenarioService.cs

```csharp
namespace MikroTikSetupWizard.Application.Services;

public sealed class SetupScenarioService
{
    public IReadOnlyList<string> GetAvailableScenarios()
    {
        return
        [
            "basic"
        ];
    }
}
```

### src\MikroTikSetupWizard.Application\Setup\IMikroTikSetupWizardService.cs

```csharp
namespace MikroTikSetupWizard.Application.Setup;

public interface IMikroTikSetupWizardService
{
    GeneratedRscPreviewDto GeneratePreview(BasicSetupInputDto input);

    Task SaveRscAsync(string path, string rscText, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Application\Setup\MikroTikSetupWizardService.cs

```csharp
using MikroTikSetupWizard.Application.Export;
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Application.Services;
using MikroTikSetupWizard.Application.Validation;
using MikroTikSetupWizard.Domain.RouterOs;
using MikroTikSetupWizard.Domain.Scenarios;
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Setup;

public sealed class MikroTikSetupWizardService : IMikroTikSetupWizardService
{
    private readonly BasicSetupWorkflow _workflow;
    private readonly IExportService _exportService;

    public MikroTikSetupWizardService()
        : this(
            new BasicSetupWorkflow(
                new ValidationService(),
                new ConfigurationBuilder(),
                new RscConfigurationRenderer()),
            new DefaultExportService())
    {
    }

    public MikroTikSetupWizardService(BasicSetupWorkflow workflow, IExportService exportService)
    {
        _workflow = workflow;
        _exportService = exportService;
    }

    public GeneratedRscPreviewDto GeneratePreview(BasicSetupInputDto input)
    {
        var result = _workflow.Generate(ToRequest(input));

        return new GeneratedRscPreviewDto(
            result.IsSuccess,
            result.RscText,
            result.Validation.Issues.Select(ToDto).ToArray());
    }

    public Task SaveRscAsync(string path, string rscText, CancellationToken cancellationToken = default)
    {
        return _exportService.SaveTextAsync(path, rscText, cancellationToken);
    }

    private static BasicSetupRequest ToRequest(BasicSetupInputDto input)
    {
        return new BasicSetupRequest
        {
            RouterName = input.RouterName,
            RouterOsVersion = input.RouterOsVersion.Contains('6')
                ? RouterOsMajorVersion.V6
                : RouterOsMajorVersion.V7,
            WanInterface = input.WanInterface,
            LanBridgeName = input.LanBridgeName,
            LanAddress = input.LanAddress,
            LanPrefixLength = input.LanPrefixLength,
            DhcpPoolStart = input.DhcpPoolStart,
            DhcpPoolEnd = input.DhcpPoolEnd,
            DnsServers = input.DnsServers,
            AdminUserName = input.AdminUserName,
            AdminPassword = input.AdminPassword,
            EnableNat = input.EnableNat,
            EnableBasicFirewall = input.EnableBasicFirewall
        };
    }

    private static ValidationIssueDto ToDto(ValidationIssue issue)
    {
        return new ValidationIssueDto(
            ToSeverityLabel(issue.Severity),
            issue.Field,
            issue.Message);
    }

    private static string ToSeverityLabel(ValidationSeverity severity)
    {
        return severity switch
        {
            ValidationSeverity.Error => "Ошибка",
            ValidationSeverity.Warning => "Предупреждение",
            _ => "Информация"
        };
    }
}
```

### src\MikroTikSetupWizard.Application\Validation\ValidationService.cs

```csharp
using MikroTikSetupWizard.Domain.Models;
using MikroTikSetupWizard.Domain.Scenarios;
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Validation;

public sealed class ValidationService : IConfigurationValidator<BasicSetupRequest>
{
    public ValidationResult Validate(BasicSetupRequest request)
    {
        var issues = new List<ValidationIssue>();

        Required(issues, request.RouterName, "RouterName", "Укажите имя роутера.");
        Required(issues, request.WanInterface, "WanInterface", "Укажите WAN-интерфейс.");
        Required(issues, request.LanBridgeName, "LanBridgeName", "Укажите имя LAN bridge.");
        Required(issues, request.AdminUserName, "AdminUserName", "Укажите имя администратора.");

        if (!Ipv4AddressMath.TryParse(request.LanAddress, out _))
        {
            issues.Add(Error("LanAddress", "LAN IP должен быть корректным IPv4-адресом."));
        }

        if (!Ipv4AddressMath.IsValidPrefixLength(request.LanPrefixLength))
        {
            issues.Add(Error("LanPrefixLength", "Префикс LAN должен быть от 1 до 32."));
        }

        ValidateDhcpAddress(issues, request.DhcpPoolStart, request, "DhcpPoolStart", "Начало DHCP-пула должно быть IPv4-адресом в LAN-сети.");
        ValidateDhcpAddress(issues, request.DhcpPoolEnd, request, "DhcpPoolEnd", "Конец DHCP-пула должен быть IPv4-адресом в LAN-сети.");

        if (Ipv4AddressMath.TryParse(request.DhcpPoolStart, out _)
            && Ipv4AddressMath.TryParse(request.DhcpPoolEnd, out _)
            && !Ipv4AddressMath.IsLessThanOrEqual(request.DhcpPoolStart, request.DhcpPoolEnd))
        {
            issues.Add(Error("DhcpPoolEnd", "Конец DHCP-пула должен быть больше или равен началу."));
        }

        ValidateDnsServers(issues, request.DnsServers);

        if (string.Equals(request.WanInterface.Trim(), request.LanBridgeName.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            issues.Add(Error("WanInterface", "WAN-интерфейс и LAN bridge должны иметь разные имена."));
        }

        if (string.IsNullOrWhiteSpace(request.AdminPassword))
        {
            issues.Add(new ValidationIssue(
                ValidationSeverity.Warning,
                "AdminPassword",
                "Пароль администратора пустой. Скрипт не будет менять пароль."));
        }

        return ValidationResult.FromIssues(issues);
    }

    private static void Required(List<ValidationIssue> issues, string value, string field, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            issues.Add(Error(field, message));
        }
    }

    private static void ValidateDhcpAddress(
        List<ValidationIssue> issues,
        string value,
        BasicSetupRequest request,
        string field,
        string message)
    {
        if (!Ipv4AddressMath.TryParse(value, out _)
            || !Ipv4AddressMath.TryParse(request.LanAddress, out _)
            || !Ipv4AddressMath.IsValidPrefixLength(request.LanPrefixLength)
            || !Ipv4AddressMath.IsInSameNetwork(value, request.LanAddress, request.LanPrefixLength))
        {
            issues.Add(Error(field, message));
        }
    }

    private static void ValidateDnsServers(List<ValidationIssue> issues, string dnsServers)
    {
        var servers = dnsServers
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (servers.Length == 0)
        {
            issues.Add(Error("DnsServers", "Укажите хотя бы один DNS-сервер."));
            return;
        }

        foreach (var server in servers)
        {
            if (!Ipv4AddressMath.TryParse(server, out _))
            {
                issues.Add(Error("DnsServers", $"DNS-сервер \"{server}\" должен быть IPv4-адресом."));
            }
        }
    }

    private static ValidationIssue Error(string field, string message)
    {
        return new ValidationIssue(ValidationSeverity.Error, field, message);
    }
}
```

### src\MikroTikSetupWizard.Desktop\Dialogs\DialogService.cs

```csharp
using System.Windows;

namespace MikroTikSetupWizard.Desktop.Dialogs;

public sealed class DialogService : IDialogService
{
    public void ShowInfo(string title, string message)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
```

### src\MikroTikSetupWizard.Desktop\Dialogs\IDialogService.cs

```csharp
namespace MikroTikSetupWizard.Desktop.Dialogs;

public interface IDialogService
{
    void ShowInfo(string title, string message);
}
```

### src\MikroTikSetupWizard.Desktop\Dialogs\ISaveFileDialogService.cs

```csharp
namespace MikroTikSetupWizard.Desktop.Dialogs;

public interface ISaveFileDialogService
{
    string? GetSaveFilePath(string defaultFileName);
}
```

### src\MikroTikSetupWizard.Desktop\Dialogs\SaveFileDialogService.cs

```csharp
using Microsoft.Win32;

namespace MikroTikSetupWizard.Desktop.Dialogs;

public sealed class SaveFileDialogService : ISaveFileDialogService
{
    public string? GetSaveFilePath(string defaultFileName)
    {
        var dialog = new SaveFileDialog
        {
            AddExtension = true,
            DefaultExt = ".rsc",
            FileName = defaultFileName,
            Filter = "RouterOS script (*.rsc)|*.rsc|Все файлы (*.*)|*.*",
            OverwritePrompt = true,
            Title = "Сохранить .rsc файл"
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}
```

### src\MikroTikSetupWizard.Desktop\Navigation\INavigationService.cs

```csharp
namespace MikroTikSetupWizard.Desktop.Navigation;

public interface INavigationService
{
    void NavigateTo(string stepId);
}
```

### src\MikroTikSetupWizard.Desktop\Navigation\NavigationService.cs

```csharp
namespace MikroTikSetupWizard.Desktop.Navigation;

public sealed class NavigationService : INavigationService
{
    public string CurrentStepId { get; private set; } = "basic";

    public void NavigateTo(string stepId)
    {
        CurrentStepId = stepId;
    }
}
```

### src\MikroTikSetupWizard.Infrastructure\Export\FileExportService.cs

```csharp
using System.Text;
using MikroTikSetupWizard.Application.Export;

namespace MikroTikSetupWizard.Infrastructure.Export;

public sealed class FileExportService : IExportService
{
    public async Task SaveTextAsync(string path, string content, CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(path, content, Encoding.UTF8, cancellationToken);
    }
}
```

### src\MikroTikSetupWizard.Infrastructure\FileSystem\IFileSystemService.cs

```csharp
namespace MikroTikSetupWizard.Infrastructure.FileSystem;

public interface IFileSystemService
{
    Task WriteTextAsync(string path, string content, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Infrastructure\Logging\ILogService.cs

```csharp
namespace MikroTikSetupWizard.Infrastructure.Logging;

public interface ILogService
{
    void Info(string message);

    void Error(string message, Exception exception);
}
```

### src\MikroTikSetupWizard.Desktop\ViewModels\ObservableObject.cs

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

### src\MikroTikSetupWizard.Desktop\ViewModels\RelayCommand.cs

```csharp
using System.Windows.Input;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
```

### src\MikroTikSetupWizard.Desktop\ViewModels\ShellViewModel.cs

```csharp
namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class ShellViewModel : ObservableObject
{
    private string _title = "MikroTik Setup Wizard";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
```

### src\MikroTikSetupWizard.Desktop\ViewModels\WizardStepViewModel.cs

```csharp
namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class WizardStepViewModel : ObservableObject
{
    private bool _isActive;

    public WizardStepViewModel(string id, string title)
    {
        Id = id;
        Title = title;
    }

    public string Id { get; }

    public string Title { get; }

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }
}
```

### src\MikroTikSetupWizard.Desktop\ViewModels\WizardViewModel.cs

```csharp
using System.IO;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Desktop.Dialogs;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class WizardViewModel : ObservableObject
{
    private readonly IMikroTikSetupWizardService _setupWizardService;
    private readonly ISaveFileDialogService _saveFileDialogService;

    private string _routerName = "MikroTik-Office";
    private string _selectedRouterOsVersion = "RouterOS 7";
    private string _wanInterface = "ether1";
    private string _lanBridgeName = "bridge-LAN";
    private string _lanAddress = "192.168.88.1";
    private int _selectedPrefixLength = 24;
    private string _dhcpPoolStart = "192.168.88.10";
    private string _dhcpPoolEnd = "192.168.88.254";
    private string _dnsServers = "1.1.1.1,8.8.8.8";
    private string _adminUserName = "admin";
    private string _adminPassword = string.Empty;
    private bool _enableNat = true;
    private bool _enableBasicFirewall = true;
    private string _generatedRsc = string.Empty;
    private string _validationMessage = string.Empty;
    private string _statusMessage = "Готово к генерации.";

    public WizardViewModel(
        IMikroTikSetupWizardService setupWizardService,
        ISaveFileDialogService saveFileDialogService)
    {
        _setupWizardService = setupWizardService;
        _saveFileDialogService = saveFileDialogService;

        ApplySmallOfficeProfileCommand = new RelayCommand(_ => ApplySmallOfficeProfile());
        GeneratePreviewCommand = new RelayCommand(_ => GeneratePreview());
        SaveFileCommand = new RelayCommand(async _ => await SaveFileAsync());
    }

    public IReadOnlyList<string> RouterOsVersions { get; } =
    [
        "RouterOS 7",
        "RouterOS 6"
    ];

    public IReadOnlyList<int> PrefixLengths { get; } =
    [
        16,
        17,
        18,
        19,
        20,
        21,
        22,
        23,
        24,
        25,
        26,
        27,
        28,
        29,
        30
    ];

    public ICommand GeneratePreviewCommand { get; }

    public ICommand SaveFileCommand { get; }

    public ICommand ApplySmallOfficeProfileCommand { get; }

    public string RouterName
    {
        get => _routerName;
        set => SetProperty(ref _routerName, value);
    }

    public string SelectedRouterOsVersion
    {
        get => _selectedRouterOsVersion;
        set => SetProperty(ref _selectedRouterOsVersion, value);
    }

    public string WanInterface
    {
        get => _wanInterface;
        set => SetProperty(ref _wanInterface, value);
    }

    public string LanBridgeName
    {
        get => _lanBridgeName;
        set => SetProperty(ref _lanBridgeName, value);
    }

    public string LanAddress
    {
        get => _lanAddress;
        set => SetProperty(ref _lanAddress, value);
    }

    public int SelectedPrefixLength
    {
        get => _selectedPrefixLength;
        set => SetProperty(ref _selectedPrefixLength, value);
    }

    public string DhcpPoolStart
    {
        get => _dhcpPoolStart;
        set => SetProperty(ref _dhcpPoolStart, value);
    }

    public string DhcpPoolEnd
    {
        get => _dhcpPoolEnd;
        set => SetProperty(ref _dhcpPoolEnd, value);
    }

    public string DnsServers
    {
        get => _dnsServers;
        set => SetProperty(ref _dnsServers, value);
    }

    public string AdminUserName
    {
        get => _adminUserName;
        set => SetProperty(ref _adminUserName, value);
    }

    public string AdminPassword
    {
        get => _adminPassword;
        set => SetProperty(ref _adminPassword, value);
    }

    public bool EnableNat
    {
        get => _enableNat;
        set => SetProperty(ref _enableNat, value);
    }

    public bool EnableBasicFirewall
    {
        get => _enableBasicFirewall;
        set => SetProperty(ref _enableBasicFirewall, value);
    }

    public string GeneratedRsc
    {
        get => _generatedRsc;
        private set => SetProperty(ref _generatedRsc, value);
    }

    public string ValidationMessage
    {
        get => _validationMessage;
        private set => SetProperty(ref _validationMessage, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    private void GeneratePreview()
    {
        var result = _setupWizardService.GeneratePreview(BuildInput());
        ValidationMessage = FormatValidation(result.Issues);

        if (!result.IsSuccess)
        {
            GeneratedRsc = string.Empty;
            StatusMessage = "Исправьте ошибки в параметрах.";
            return;
        }

        GeneratedRsc = result.RscText;
        StatusMessage = "Предпросмотр обновлён.";
    }

    private void ApplySmallOfficeProfile()
    {
        RouterName = "MikroTik-SmallOffice";
        SelectedRouterOsVersion = "RouterOS 7";
        WanInterface = "ether1";
        LanBridgeName = "bridge-LAN";
        LanAddress = "192.168.88.1/24";
        SelectedPrefixLength = 24;
        DhcpPoolStart = "192.168.88.10";
        DhcpPoolEnd = "192.168.88.254";
        DnsServers = "1.1.1.1,8.8.8.8";
        AdminUserName = "admin";
        EnableNat = true;
        EnableBasicFirewall = true;
        GeneratedRsc = string.Empty;
        ValidationMessage = string.Empty;
        StatusMessage = "Профиль \"Малый офис\" применён.";
    }

    private async Task SaveFileAsync()
    {
        if (string.IsNullOrWhiteSpace(GeneratedRsc))
        {
            GeneratePreview();
        }

        if (string.IsNullOrWhiteSpace(GeneratedRsc))
        {
            return;
        }

        var path = _saveFileDialogService.GetSaveFilePath(BuildDefaultFileName());

        if (string.IsNullOrWhiteSpace(path))
        {
            StatusMessage = "Сохранение отменено.";
            return;
        }

        try
        {
            await _setupWizardService.SaveRscAsync(path, GeneratedRsc);
            StatusMessage = $"Файл сохранён: {path}";
        }
        catch (Exception exception)
        {
            StatusMessage = "Не удалось сохранить файл.";
            ValidationMessage = exception.Message;
        }
    }

    private BasicSetupInputDto BuildInput()
    {
        var lanAddress = ParseLanAddress();

        return new BasicSetupInputDto
        {
            RouterName = RouterName,
            RouterOsVersion = SelectedRouterOsVersion,
            WanInterface = WanInterface,
            LanBridgeName = LanBridgeName,
            LanAddress = lanAddress.Address,
            LanPrefixLength = lanAddress.PrefixLength,
            DhcpPoolStart = DhcpPoolStart,
            DhcpPoolEnd = DhcpPoolEnd,
            DnsServers = DnsServers,
            AdminUserName = AdminUserName,
            AdminPassword = AdminPassword,
            EnableNat = EnableNat,
            EnableBasicFirewall = EnableBasicFirewall
        };
    }

    private (string Address, int PrefixLength) ParseLanAddress()
    {
        var parts = LanAddress.Split('/', StringSplitOptions.TrimEntries);

        if (parts.Length == 1)
        {
            return (LanAddress, SelectedPrefixLength);
        }

        if (parts.Length == 2 && int.TryParse(parts[1], out var prefixLength))
        {
            return (parts[0], prefixLength);
        }

        return (parts[0], -1);
    }

    private string BuildDefaultFileName()
    {
        var safeName = string.Join(
            "_",
            RouterName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

        if (string.IsNullOrWhiteSpace(safeName))
        {
            safeName = "mikrotik-config";
        }

        return $"{safeName.Trim()}.rsc";
    }

    private static string FormatValidation(IReadOnlyList<ValidationIssueDto> issues)
    {
        if (issues.Count == 0)
        {
            return "Ошибок нет.";
        }

        return string.Join(
            Environment.NewLine,
            issues.Select(issue => $"{issue.Severity}: {issue.Message}"));
    }
}
```

### docs\architecture.md

```markdown
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
```

### docs\module-design.md

```markdown
# Модули

Каждый будущий модуль должен реализовать `ISetupModule`.

Предполагаемая ответственность модуля:

- объявить идентификатор и русское имя;
- предоставить шаги мастера;
- добавить команды в `ConfigurationPlan`;
- выполнить локальную валидацию;
- объявить необходимые возможности RouterOS.

Текущие модули являются пустыми заготовками:

- Internet
- WAN
- LAN
- DHCP
- DNS
- NAT
- Firewall
- Users
- Wireless
- VLAN
- Backup
- Security
```

### docs\routeros6-routeros7-notes.md

```markdown
# RouterOS 6 и 7

Различия версий должны быть локализованы в проекте `MikroTikSetupWizard.RouterOs`.

Текущий минимальный сценарий использует команды, рассчитанные на RouterOS 6/7:

- `/system identity`
- `/interface list`
- `/interface bridge`
- `/ip address`
- `/ip pool`
- `/ip dhcp-server`
- `/ip dns`
- `/ip firewall nat`
- `/ip firewall filter`
- `/user`

Для будущих модулей нужно расширять:

- `RouterOsCapabilities`
- `RouterOsSyntaxPolicy`
- специализированные renderer/policy классы, если синтаксис RouterOS 6 и 7 расходится.
```

### src\MikroTikSetupWizard.Application\Generation\ConfigurationBuilder.cs

```csharp
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Models;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Generation;

public sealed class ConfigurationBuilder : IConfigurationBuilder
{
    public ConfigurationPlan Build(BasicSetupRequest request)
    {
        var plan = new ConfigurationPlan(request.RouterName.Trim(), request.RouterOsVersion);

        AddSystemIdentity(plan, request);
        AddInterfaceLists(plan, request);
        AddLanAddressing(plan, request);
        AddDhcp(plan, request);
        AddDns(plan, request);
        AddUserHardening(plan, request);

        if (request.EnableNat)
        {
            AddNat(plan, request);
        }

        if (request.EnableBasicFirewall)
        {
            AddFirewallBaseline(plan);
        }

        return plan;
    }

    private static void AddSystemIdentity(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "system identity",
            "set",
            "Имя роутера",
            Param("name", request.RouterName.Trim())));
    }

    private static void AddInterfaceLists(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "interface list",
            "add",
            "Список WAN-интерфейсов",
            Param("name", "WAN")));

        plan.Add(Command(
            "interface list",
            "add",
            "Список LAN-интерфейсов",
            Param("name", "LAN")));

        plan.Add(Command(
            "interface list member",
            "add",
            "WAN-интерфейс",
            Param("list", "WAN"),
            Param("interface", request.WanInterface.Trim())));
    }

    private static void AddLanAddressing(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "interface bridge",
            "add",
            "LAN bridge",
            Param("name", request.LanBridgeName.Trim())));

        plan.Add(Command(
            "interface list member",
            "add",
            "LAN bridge в списке LAN",
            Param("list", "LAN"),
            Param("interface", request.LanBridgeName.Trim())));

        plan.Add(Command(
            "ip address",
            "add",
            "IP-адрес LAN gateway",
            Param("address", $"{request.LanAddress.Trim()}/{request.LanPrefixLength}"),
            Param("interface", request.LanBridgeName.Trim())));
    }

    private static void AddDhcp(ConfigurationPlan plan, BasicSetupRequest request)
    {
        var poolName = $"{request.LanBridgeName.Trim()}-pool";
        var serverName = $"{request.LanBridgeName.Trim()}-dhcp";
        var networkCidr = Ipv4AddressMath.GetNetworkCidr(request.LanAddress.Trim(), request.LanPrefixLength);

        plan.Add(Command(
            "ip pool",
            "add",
            "DHCP pool",
            Param("name", poolName),
            Param("ranges", $"{request.DhcpPoolStart.Trim()}-{request.DhcpPoolEnd.Trim()}")));

        plan.Add(Command(
            "ip dhcp-server",
            "add",
            "DHCP server",
            Param("name", serverName),
            Param("interface", request.LanBridgeName.Trim()),
            Param("address-pool", poolName),
            Param("disabled", "no")));

        plan.Add(Command(
            "ip dhcp-server network",
            "add",
            "DHCP network",
            Param("address", networkCidr),
            Param("gateway", request.LanAddress.Trim()),
            Param("dns-server", NormalizeDnsServers(request.DnsServers))));
    }

    private static void AddDns(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "ip dns",
            "set",
            "DNS forwarding",
            Param("allow-remote-requests", "yes"),
            Param("servers", NormalizeDnsServers(request.DnsServers))));
    }

    private static void AddUserHardening(ConfigurationPlan plan, BasicSetupRequest request)
    {
        var parameters = new List<ConfigurationParameter>();

        if (!string.Equals(request.AdminUserName.Trim(), "admin", StringComparison.OrdinalIgnoreCase))
        {
            parameters.Add(Param("name", request.AdminUserName.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(request.AdminPassword))
        {
            parameters.Add(Param("password", request.AdminPassword));
        }

        if (parameters.Count == 0)
        {
            return;
        }

        plan.Add(new ConfigurationCommand(
            "user",
            "set",
            parameters,
            "[find name=\"admin\"]",
            "Администратор"));
    }

    private static void AddNat(ConfigurationPlan plan, BasicSetupRequest request)
    {
        plan.Add(Command(
            "ip firewall nat",
            "add",
            "NAT masquerade для выхода в интернет",
            Param("chain", "srcnat"),
            Param("out-interface-list", "WAN"),
            Param("action", "masquerade")));
    }

    private static void AddFirewallBaseline(ConfigurationPlan plan)
    {
        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Разрешить установленные входящие соединения",
            Param("chain", "input"),
            Param("action", "accept"),
            Param("connection-state", "established,related,untracked")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Отклонить invalid",
            Param("chain", "input"),
            Param("action", "drop"),
            Param("connection-state", "invalid")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Разрешить ICMP",
            Param("chain", "input"),
            Param("action", "accept"),
            Param("protocol", "icmp")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Разрешить управление из LAN",
            Param("chain", "input"),
            Param("action", "accept"),
            Param("in-interface-list", "LAN")));

        plan.Add(Command(
            "ip firewall filter",
            "add",
            "Закрыть вход с WAN",
            Param("chain", "input"),
            Param("action", "drop"),
            Param("in-interface-list", "WAN")));
    }

    private static ConfigurationCommand Command(
        string section,
        string operation,
        string comment,
        params ConfigurationParameter[] parameters)
    {
        return new ConfigurationCommand(section, operation, parameters, Comment: comment);
    }

    private static ConfigurationParameter Param(string name, string? value)
    {
        return new ConfigurationParameter(name, value);
    }

    private static string NormalizeDnsServers(string dnsServers)
    {
        return string.Join(
            ",",
            dnsServers.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
    }
}
```

### src\MikroTikSetupWizard.Application\Generation\GeneratedConfiguration.cs

```csharp
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Generation;

public sealed record GeneratedConfiguration(
    ValidationResult Validation,
    ConfigurationPlan? Plan,
    string RscText)
{
    public bool IsSuccess => Validation.IsValid && Plan is not null;
}
```

### src\MikroTikSetupWizard.Application\Generation\IConfigurationBuilder.cs

```csharp
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.Scenarios;

namespace MikroTikSetupWizard.Application.Generation;

public interface IConfigurationBuilder
{
    ConfigurationPlan Build(BasicSetupRequest request);
}
```

### src\MikroTikSetupWizard.Application\Generation\IConfigurationRenderer.cs

```csharp
using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Application.Generation;

public interface IConfigurationRenderer
{
    string Render(ConfigurationPlan plan);
}
```

### src\MikroTikSetupWizard.Application\Generation\RscConfigurationRenderer.cs

```csharp
using System.Text;
using MikroTikSetupWizard.Domain.Configuration;
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Application.Generation;

internal sealed class RscConfigurationRenderer : IConfigurationRenderer
{
    public string Render(ConfigurationPlan plan)
    {
        var builder = new StringBuilder();

        builder.AppendLine("# MikroTik Setup Wizard");
        builder.AppendLine($"# Target: {GetVersionLabel(plan.RouterOsVersion)}");
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

    private static string GetVersionLabel(RouterOsMajorVersion version)
    {
        return version switch
        {
            RouterOsMajorVersion.V6 => "RouterOS 6",
            RouterOsMajorVersion.V7 => "RouterOS 7",
            _ => "RouterOS"
        };
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
```

### src\MikroTikSetupWizard.Application\Profiles\IRouterProfileRepository.cs

```csharp
using MikroTikSetupWizard.Domain.Models;

namespace MikroTikSetupWizard.Application.Profiles;

public interface IRouterProfileRepository
{
    Task<IReadOnlyList<RouterProfile>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(RouterProfile profile, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Application\Validation\IConfigurationValidator.cs

```csharp
using MikroTikSetupWizard.Domain.Validation;

namespace MikroTikSetupWizard.Application.Validation;

public interface IConfigurationValidator<in TRequest>
{
    ValidationResult Validate(TRequest request);
}
```

### src\MikroTikSetupWizard.Application\Wizard\ISetupModule.cs

```csharp
namespace MikroTikSetupWizard.Application.Wizard;

public interface ISetupModule
{
    string Id { get; }

    string DisplayName { get; }

    IReadOnlyCollection<string> RequiredFeatures { get; }
}
```

### src\MikroTikSetupWizard.Application\Wizard\WizardFlow.cs

```csharp
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
```

### src\MikroTikSetupWizard.Application\Wizard\WizardSession.cs

```csharp
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
```

### src\MikroTikSetupWizard.Application\Wizard\WizardStep.cs

```csharp
namespace MikroTikSetupWizard.Application.Wizard;

public sealed record WizardStep(string Id, string Title);
```

### src\MikroTikSetupWizard.Desktop\App.xaml.cs

```csharp
using System.Windows;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Desktop.Dialogs;
using MikroTikSetupWizard.Desktop.ViewModels;
using MikroTikSetupWizard.Desktop.Views;

namespace MikroTikSetupWizard.Desktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var window = new MainWindow
        {
            DataContext = new WizardViewModel(
                new MikroTikSetupWizardService(),
                new SaveFileDialogService())
        };

        window.Show();
    }
}
```

### src\MikroTikSetupWizard.Desktop\Controls\WizardStepHeader.cs

```csharp
using System.Windows.Controls;

namespace MikroTikSetupWizard.Desktop.Controls;

public sealed class WizardStepHeader : ContentControl
{
}
```

### src\MikroTikSetupWizard.Desktop\Themes\DarkTheme.xaml

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <SolidColorBrush x:Key="AppBackgroundBrush" Color="#101216" />
    <SolidColorBrush x:Key="PanelBackgroundBrush" Color="#181B22" />
    <SolidColorBrush x:Key="InputBackgroundBrush" Color="#11141A" />
    <SolidColorBrush x:Key="BorderBrush" Color="#2B303B" />
    <SolidColorBrush x:Key="TextBrush" Color="#F0F3F7" />
    <SolidColorBrush x:Key="MutedTextBrush" Color="#A8B0BD" />
    <SolidColorBrush x:Key="AccentBrush" Color="#2EA6FF" />
    <SolidColorBrush x:Key="AccentHoverBrush" Color="#4DB5FF" />
    <SolidColorBrush x:Key="DangerBrush" Color="#FF6B6B" />
    <SolidColorBrush x:Key="SuccessBrush" Color="#69D28D" />

    <Style TargetType="{x:Type Window}">
        <Setter Property="FontFamily" Value="Segoe UI" />
        <Setter Property="FontSize" Value="14" />
    </Style>

    <Style TargetType="{x:Type TextBlock}">
        <Setter Property="Foreground" Value="{DynamicResource TextBrush}" />
        <Setter Property="TextWrapping" Value="Wrap" />
    </Style>

    <Style x:Key="TitleTextStyle" TargetType="{x:Type TextBlock}">
        <Setter Property="FontSize" Value="26" />
        <Setter Property="FontWeight" Value="SemiBold" />
        <Setter Property="Margin" Value="0,0,0,4" />
    </Style>

    <Style x:Key="SectionTitleStyle" TargetType="{x:Type TextBlock}">
        <Setter Property="FontSize" Value="17" />
        <Setter Property="FontWeight" Value="SemiBold" />
        <Setter Property="Margin" Value="0,18,0,8" />
    </Style>

    <Style x:Key="FieldLabelStyle" TargetType="{x:Type TextBlock}">
        <Setter Property="Foreground" Value="{DynamicResource MutedTextBrush}" />
        <Setter Property="Margin" Value="0,8,0,4" />
    </Style>

    <Style TargetType="{x:Type TextBox}">
        <Setter Property="Background" Value="{DynamicResource InputBackgroundBrush}" />
        <Setter Property="Foreground" Value="{DynamicResource TextBrush}" />
        <Setter Property="BorderBrush" Value="{DynamicResource BorderBrush}" />
        <Setter Property="BorderThickness" Value="1" />
        <Setter Property="Padding" Value="9,7" />
        <Setter Property="CaretBrush" Value="{DynamicResource TextBrush}" />
        <Setter Property="SelectionBrush" Value="{DynamicResource AccentBrush}" />
        <Setter Property="Margin" Value="0,0,0,2" />
    </Style>

    <Style TargetType="{x:Type PasswordBox}">
        <Setter Property="Background" Value="{DynamicResource InputBackgroundBrush}" />
        <Setter Property="Foreground" Value="{DynamicResource TextBrush}" />
        <Setter Property="BorderBrush" Value="{DynamicResource BorderBrush}" />
        <Setter Property="BorderThickness" Value="1" />
        <Setter Property="Padding" Value="9,7" />
        <Setter Property="CaretBrush" Value="{DynamicResource TextBrush}" />
        <Setter Property="SelectionBrush" Value="{DynamicResource AccentBrush}" />
        <Setter Property="Margin" Value="0,0,0,2" />
    </Style>

    <Style TargetType="{x:Type ComboBox}">
        <Setter Property="Background" Value="{DynamicResource InputBackgroundBrush}" />
        <Setter Property="Foreground" Value="{DynamicResource TextBrush}" />
        <Setter Property="BorderBrush" Value="{DynamicResource BorderBrush}" />
        <Setter Property="Padding" Value="8,6" />
        <Setter Property="Margin" Value="0,0,0,2" />
    </Style>

    <Style TargetType="{x:Type CheckBox}">
        <Setter Property="Foreground" Value="{DynamicResource TextBrush}" />
        <Setter Property="Margin" Value="0,10,0,0" />
    </Style>

    <Style TargetType="{x:Type Button}">
        <Setter Property="Background" Value="{DynamicResource AccentBrush}" />
        <Setter Property="Foreground" Value="#FFFFFF" />
        <Setter Property="BorderBrush" Value="{DynamicResource AccentBrush}" />
        <Setter Property="BorderThickness" Value="1" />
        <Setter Property="Padding" Value="14,8" />
        <Setter Property="FontWeight" Value="SemiBold" />
        <Setter Property="Margin" Value="0,12,10,0" />
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Background" Value="{DynamicResource AccentHoverBrush}" />
                <Setter Property="BorderBrush" Value="{DynamicResource AccentHoverBrush}" />
            </Trigger>
        </Style.Triggers>
    </Style>

    <Style x:Key="SecondaryButtonStyle" TargetType="{x:Type Button}" BasedOn="{StaticResource {x:Type Button}}">
        <Setter Property="Background" Value="{DynamicResource PanelBackgroundBrush}" />
        <Setter Property="BorderBrush" Value="{DynamicResource BorderBrush}" />
    </Style>
</ResourceDictionary>
```

### src\MikroTikSetupWizard.Desktop\Themes\ThemeManager.cs

```csharp
namespace MikroTikSetupWizard.Desktop.Themes;

public sealed class ThemeManager
{
    public string CurrentTheme => "Dark";
}
```

### src\MikroTikSetupWizard.Desktop\Views\MainWindow.xaml.cs

```csharp
using System.Windows;
using MikroTikSetupWizard.Desktop.ViewModels;

namespace MikroTikSetupWizard.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AdminPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is WizardViewModel viewModel)
        {
            viewModel.AdminPassword = AdminPasswordBox.Password;
        }
    }
}
```

### src\MikroTikSetupWizard.Domain\Configuration\BridgeConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record BridgeConfig(string Name);
```

### src\MikroTikSetupWizard.Domain\Configuration\ConfigurationCommand.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record ConfigurationCommand(
    string Section,
    string Operation,
    IReadOnlyList<ConfigurationParameter> Parameters,
    string? Selector = null,
    string? Comment = null);
```

### src\MikroTikSetupWizard.Domain\Configuration\ConfigurationParameter.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record ConfigurationParameter(string Name, string? Value);
```

### src\MikroTikSetupWizard.Domain\Configuration\ConfigurationPlan.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Domain.Configuration;

public sealed class ConfigurationPlan
{
    private readonly List<ConfigurationCommand> _commands = new();

    public ConfigurationPlan(string name, RouterOsMajorVersion routerOsVersion)
    {
        Name = name;
        RouterOsVersion = routerOsVersion;
    }

    public string Name { get; }

    public RouterOsMajorVersion RouterOsVersion { get; }

    public IReadOnlyList<ConfigurationCommand> Commands => _commands;

    public void Add(ConfigurationCommand command)
    {
        _commands.Add(command);
    }
}
```

### src\MikroTikSetupWizard.Domain\Configuration\DhcpConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record DhcpConfig(
    string ServerName,
    string PoolName,
    string PoolStart,
    string PoolEnd,
    string InterfaceName);
```

### src\MikroTikSetupWizard.Domain\Configuration\DnsConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record DnsConfig(IReadOnlyList<string> Servers, bool AllowRemoteRequests);
```

### src\MikroTikSetupWizard.Domain\Configuration\FirewallConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record FirewallConfig(bool EnableBaselineRules);
```

### src\MikroTikSetupWizard.Domain\Configuration\IpAddressConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record IpAddressConfig(string Address, int PrefixLength, string InterfaceName);
```

### src\MikroTikSetupWizard.Domain\Configuration\NatConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record NatConfig(bool EnableMasquerade, string WanInterfaceList);
```

### src\MikroTikSetupWizard.Domain\Configuration\UserAccountConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record UserAccountConfig(string UserName, bool ChangeDefaultAdmin);
```

### src\MikroTikSetupWizard.Domain\Configuration\VlanConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record VlanConfig(int VlanId, string Name, string ParentInterface);
```

### src\MikroTikSetupWizard.Domain\Configuration\WirelessConfig.cs

```csharp
namespace MikroTikSetupWizard.Domain.Configuration;

public sealed record WirelessConfig(string Ssid, bool Enabled);
```

### src\MikroTikSetupWizard.Domain\Models\Ipv4AddressMath.cs

```csharp
using System.Net;
using System.Net.Sockets;

namespace MikroTikSetupWizard.Domain.Models;

public static class Ipv4AddressMath
{
    public static bool TryParse(string value, out uint address)
    {
        address = 0;

        if (!IPAddress.TryParse(value?.Trim(), out var ipAddress))
        {
            return false;
        }

        if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
        {
            return false;
        }

        var bytes = ipAddress.GetAddressBytes();
        address = ((uint)bytes[0] << 24)
            | ((uint)bytes[1] << 16)
            | ((uint)bytes[2] << 8)
            | bytes[3];

        return true;
    }

    public static bool IsValidPrefixLength(int prefixLength)
    {
        return prefixLength is >= 1 and <= 32;
    }

    public static bool IsInSameNetwork(string candidate, string gateway, int prefixLength)
    {
        if (!TryParse(candidate, out var candidateAddress)
            || !TryParse(gateway, out var gatewayAddress)
            || !IsValidPrefixLength(prefixLength))
        {
            return false;
        }

        var mask = GetMask(prefixLength);
        return (candidateAddress & mask) == (gatewayAddress & mask);
    }

    public static bool IsLessThanOrEqual(string left, string right)
    {
        return TryParse(left, out var leftAddress)
            && TryParse(right, out var rightAddress)
            && leftAddress <= rightAddress;
    }

    public static string GetNetworkCidr(string gateway, int prefixLength)
    {
        if (!TryParse(gateway, out var gatewayAddress) || !IsValidPrefixLength(prefixLength))
        {
            throw new ArgumentException("Invalid IPv4 address or prefix length.", nameof(gateway));
        }

        var networkAddress = gatewayAddress & GetMask(prefixLength);
        return $"{ToDottedDecimal(networkAddress)}/{prefixLength}";
    }

    private static uint GetMask(int prefixLength)
    {
        return prefixLength == 0 ? 0 : uint.MaxValue << (32 - prefixLength);
    }

    private static string ToDottedDecimal(uint address)
    {
        return string.Join(
            ".",
            (address >> 24) & 0xFF,
            (address >> 16) & 0xFF,
            (address >> 8) & 0xFF,
            address & 0xFF);
    }
}
```

### src\MikroTikSetupWizard.Domain\Models\NetworkInterface.cs

```csharp
namespace MikroTikSetupWizard.Domain.Models;

public sealed record NetworkInterface(string Name, string Role);
```

### src\MikroTikSetupWizard.Domain\Models\RouterProfile.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Domain.Models;

public sealed record RouterProfile(
    string Name,
    RouterOsMajorVersion RouterOsVersion);
```

### src\MikroTikSetupWizard.Domain\RouterOs\RouterOsGeneration.cs

```csharp
namespace MikroTikSetupWizard.Domain.RouterOs;

public enum RouterOsGeneration
{
    RouterOs6,
    RouterOs7
}
```

### src\MikroTikSetupWizard.Domain\RouterOs\RouterOsMajorVersion.cs

```csharp
namespace MikroTikSetupWizard.Domain.RouterOs;

public enum RouterOsMajorVersion
{
    V6 = 6,
    V7 = 7
}
```

### src\MikroTikSetupWizard.Domain\RouterOs\RouterOsVersion.cs

```csharp
namespace MikroTikSetupWizard.Domain.RouterOs;

public sealed record RouterOsVersion(RouterOsMajorVersion MajorVersion, string DisplayName);
```

### src\MikroTikSetupWizard.Domain\Scenarios\BasicSetupRequest.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.Domain.Scenarios;

public sealed class BasicSetupRequest
{
    public string RouterName { get; init; } = string.Empty;

    public RouterOsMajorVersion RouterOsVersion { get; init; }

    public string WanInterface { get; init; } = string.Empty;

    public string LanBridgeName { get; init; } = string.Empty;

    public string LanAddress { get; init; } = string.Empty;

    public int LanPrefixLength { get; init; }

    public string DhcpPoolStart { get; init; } = string.Empty;

    public string DhcpPoolEnd { get; init; } = string.Empty;

    public string DnsServers { get; init; } = string.Empty;

    public string AdminUserName { get; init; } = string.Empty;

    public string AdminPassword { get; init; } = string.Empty;

    public bool EnableNat { get; init; }

    public bool EnableBasicFirewall { get; init; }
}
```

### src\MikroTikSetupWizard.Domain\Validation\ValidationIssue.cs

```csharp
namespace MikroTikSetupWizard.Domain.Validation;

public sealed record ValidationIssue(
    ValidationSeverity Severity,
    string Field,
    string Message);
```

### src\MikroTikSetupWizard.Domain\Validation\ValidationResult.cs

```csharp
namespace MikroTikSetupWizard.Domain.Validation;

public sealed class ValidationResult
{
    private static readonly ValidationResult Empty = new(Array.Empty<ValidationIssue>());

    private ValidationResult(IReadOnlyList<ValidationIssue> issues)
    {
        Issues = issues;
    }

    public IReadOnlyList<ValidationIssue> Issues { get; }

    public bool IsValid => Issues.All(issue => issue.Severity != ValidationSeverity.Error);

    public static ValidationResult Success()
    {
        return Empty;
    }

    public static ValidationResult FromIssues(IEnumerable<ValidationIssue> issues)
    {
        var materializedIssues = issues.ToArray();
        return materializedIssues.Length == 0 ? Empty : new ValidationResult(materializedIssues);
    }
}
```

### src\MikroTikSetupWizard.Domain\Validation\ValidationSeverity.cs

```csharp
namespace MikroTikSetupWizard.Domain.Validation;

public enum ValidationSeverity
{
    Info,
    Warning,
    Error
}
```

### src\MikroTikSetupWizard.Infrastructure\Api\IRouterApiClient.cs

```csharp
using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Infrastructure.Api;

public interface IRouterApiClient
{
    Task ApplyAsync(ConfigurationPlan plan, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Infrastructure\Api\IRouterApiSession.cs

```csharp
namespace MikroTikSetupWizard.Infrastructure.Api;

public interface IRouterApiSession : IAsyncDisposable
{
    Task SendCommandAsync(string path, IReadOnlyDictionary<string, string> parameters, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Infrastructure\Persistence\IRouterProfileStore.cs

```csharp
using MikroTikSetupWizard.Domain.Models;

namespace MikroTikSetupWizard.Infrastructure.Persistence;

public interface IRouterProfileStore
{
    Task<IReadOnlyList<RouterProfile>> LoadAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(RouterProfile profile, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Infrastructure\Settings\AppSettings.cs

```csharp
namespace MikroTikSetupWizard.Infrastructure.Settings;

public sealed class AppSettings
{
    public string LastExportDirectory { get; init; } = string.Empty;

    public string Theme { get; init; } = "Dark";
}
```

### src\MikroTikSetupWizard.Infrastructure\Settings\IAppSettingsRepository.cs

```csharp
namespace MikroTikSetupWizard.Infrastructure.Settings;

public interface IAppSettingsRepository
{
    Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Infrastructure\Ssh\IRouterSshClient.cs

```csharp
using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.Infrastructure.Ssh;

public interface IRouterSshClient
{
    Task ApplyAsync(ConfigurationPlan plan, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Infrastructure\Ssh\IRouterSshSession.cs

```csharp
namespace MikroTikSetupWizard.Infrastructure.Ssh;

public interface IRouterSshSession : IAsyncDisposable
{
    Task ExecuteAsync(string command, CancellationToken cancellationToken = default);
}
```

### src\MikroTikSetupWizard.Modules\Backup\BackupSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Backup;

public sealed class BackupSetupModule : ModuleStub
{
    public BackupSetupModule()
        : base("backup", "Backup")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Dhcp\DhcpSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Dhcp;

public sealed class DhcpSetupModule : ModuleStub
{
    public DhcpSetupModule()
        : base("dhcp", "DHCP")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Dns\DnsSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Dns;

public sealed class DnsSetupModule : ModuleStub
{
    public DnsSetupModule()
        : base("dns", "DNS")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Firewall\FirewallSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Firewall;

public sealed class FirewallSetupModule : ModuleStub
{
    public FirewallSetupModule()
        : base("firewall", "Firewall")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Internet\InternetSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Internet;

public sealed class InternetSetupModule : ModuleStub
{
    public InternetSetupModule()
        : base("internet", "Интернет")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Lan\LanSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Lan;

public sealed class LanSetupModule : ModuleStub
{
    public LanSetupModule()
        : base("lan", "LAN")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\ModuleStub.cs

```csharp
using MikroTikSetupWizard.Application.Wizard;

namespace MikroTikSetupWizard.Modules;

public abstract class ModuleStub : ISetupModule
{
    protected ModuleStub(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public string Id { get; }

    public string DisplayName { get; }

    public IReadOnlyCollection<string> RequiredFeatures { get; } = Array.Empty<string>();
}
```

### src\MikroTikSetupWizard.Modules\Nat\NatSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Nat;

public sealed class NatSetupModule : ModuleStub
{
    public NatSetupModule()
        : base("nat", "NAT")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Security\SecurityHardeningModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Security;

public sealed class SecurityHardeningModule : ModuleStub
{
    public SecurityHardeningModule()
        : base("security", "Безопасность")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Users\UsersSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Users;

public sealed class UsersSetupModule : ModuleStub
{
    public UsersSetupModule()
        : base("users", "Пользователи")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Vlans\VlanSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Vlans;

public sealed class VlanSetupModule : ModuleStub
{
    public VlanSetupModule()
        : base("vlans", "VLAN")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Wan\WanSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Wan;

public sealed class WanSetupModule : ModuleStub
{
    public WanSetupModule()
        : base("wan", "WAN")
    {
    }
}
```

### src\MikroTikSetupWizard.Modules\Wireless\WirelessSetupModule.cs

```csharp
using MikroTikSetupWizard.Modules;

namespace MikroTikSetupWizard.Modules.Wireless;

public sealed class WirelessSetupModule : ModuleStub
{
    public WirelessSetupModule()
        : base("wireless", "Wi-Fi")
    {
    }
}
```

### src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOs6Capabilities.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Capabilities;

public sealed record RouterOs6Capabilities()
    : RouterOsCapabilities(
        RouterOsMajorVersion.V6,
        SupportsInterfaceLists: true,
        SupportsOutInterfaceListNat: true);
```

### src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOs7Capabilities.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Capabilities;

public sealed record RouterOs7Capabilities()
    : RouterOsCapabilities(
        RouterOsMajorVersion.V7,
        SupportsInterfaceLists: true,
        SupportsOutInterfaceListNat: true);
```

### src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOsCapabilities.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Capabilities;

public abstract record RouterOsCapabilities(
    RouterOsMajorVersion Version,
    bool SupportsInterfaceLists,
    bool SupportsOutInterfaceListNat)
{
    public static RouterOsCapabilities For(RouterOsMajorVersion version)
    {
        return version switch
        {
            RouterOsMajorVersion.V6 => new RouterOs6Capabilities(),
            RouterOsMajorVersion.V7 => new RouterOs7Capabilities(),
            _ => throw new ArgumentOutOfRangeException(nameof(version), version, "Unsupported RouterOS version.")
        };
    }
}
```

### src\MikroTikSetupWizard.RouterOs\Capabilities\RouterOsFeatureSupport.cs

```csharp
namespace MikroTikSetupWizard.RouterOs.Capabilities;

public sealed record RouterOsFeatureSupport(string Feature, bool IsSupported);
```

### src\MikroTikSetupWizard.RouterOs\Commands\RouterOsCommand.cs

```csharp
namespace MikroTikSetupWizard.RouterOs.Commands;

public sealed record RouterOsCommand(string Text);
```

### src\MikroTikSetupWizard.RouterOs\Commands\RouterOsCommandSet.cs

```csharp
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
```

### src\MikroTikSetupWizard.RouterOs\Renderers\RouterOs6RscRenderer.cs

```csharp
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.RouterOs.Renderers;

public sealed class RouterOs6RscRenderer : IConfigurationRenderer
{
    private readonly RouterOsRscRenderer _renderer = new();

    public string Render(ConfigurationPlan plan)
    {
        return _renderer.Render(plan);
    }
}
```

### src\MikroTikSetupWizard.RouterOs\Renderers\RouterOs7RscRenderer.cs

```csharp
using MikroTikSetupWizard.Application.Generation;
using MikroTikSetupWizard.Domain.Configuration;

namespace MikroTikSetupWizard.RouterOs.Renderers;

public sealed class RouterOs7RscRenderer : IConfigurationRenderer
{
    private readonly RouterOsRscRenderer _renderer = new();

    public string Render(ConfigurationPlan plan)
    {
        return _renderer.Render(plan);
    }
}
```

### src\MikroTikSetupWizard.RouterOs\Renderers\RouterOsRscRenderer.cs

```csharp
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
```

### src\MikroTikSetupWizard.RouterOs\Versions\RouterOsSyntaxPolicy.cs

```csharp
using MikroTikSetupWizard.Domain.RouterOs;

namespace MikroTikSetupWizard.RouterOs.Versions;

public sealed class RouterOsSyntaxPolicy
{
    public RouterOsSyntaxPolicy(RouterOsMajorVersion version)
    {
        Version = version;
    }

    public RouterOsMajorVersion Version { get; }

    public string HeaderVersionLabel => Version switch
    {
        RouterOsMajorVersion.V6 => "RouterOS 6",
        RouterOsMajorVersion.V7 => "RouterOS 7",
        _ => "RouterOS"
    };
}
```

### src\MikroTikSetupWizard.Shared\Result.cs

```csharp
namespace MikroTikSetupWizard.Shared;

public sealed record Result<T>(bool IsSuccess, T? Value, string Error)
{
    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, default, error);
    }
}
```

### tests\MikroTikSetupWizard.Application.Tests\README.md

```markdown
# Application tests

Папка оставлена под тесты workflow, validation и configuration builder.
```

### tests\MikroTikSetupWizard.Domain.Tests\README.md

```markdown
# Domain tests

Папка оставлена под тесты доменной модели и IPv4-валидации.
```

### tests\MikroTikSetupWizard.RouterOs.Tests\README.md

```markdown
# RouterOS tests

Папка оставлена под snapshot/approval тесты `.rsc` renderer.
```
