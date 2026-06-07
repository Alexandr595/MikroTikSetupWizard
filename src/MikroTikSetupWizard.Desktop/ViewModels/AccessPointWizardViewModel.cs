using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Desktop.Dialogs;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class AccessPointWizardViewModel : ObservableObject
{
    private readonly AccessPointConfigurationBuilder _configurationBuilder;
    private readonly IMikroTikSetupWizardService _setupWizardService;
    private readonly ISaveFileDialogService _saveFileDialogService;
    private readonly RelayCommand _backCommand;
    private readonly RelayCommand _nextCommand;
    private readonly RelayCommand _generatePreviewCommand;
    private readonly RelayCommand _saveFileCommand;
    private int _currentStepIndex = -1;
    private string _generatedRsc = string.Empty;
    private string _validationMessage = "Проверка ещё не выполнялась.";
    private IReadOnlyList<string> _validationErrors = [];
    private IReadOnlyList<string> _validationWarnings = [];
    private string _statusMessage = "Заполните шаги мастера и перейдите к проверке.";

    public AccessPointWizardViewModel(
        AccessPointConfigurationBuilder configurationBuilder,
        IMikroTikSetupWizardService setupWizardService,
        ISaveFileDialogService saveFileDialogService)
    {
        _configurationBuilder = configurationBuilder;
        _setupWizardService = setupWizardService;
        _saveFileDialogService = saveFileDialogService;
        _backCommand = new RelayCommand(_ => MoveBack(), _ => CanGoBack);
        _nextCommand = new RelayCommand(_ => MoveNext(), _ => CanGoNext);
        _generatePreviewCommand = new RelayCommand(_ => GeneratePreview(), _ => !HasValidationErrors);
        _saveFileCommand = new RelayCommand(async _ => await SaveFileAsync(), _ => !string.IsNullOrWhiteSpace(GeneratedRsc));

        Steps =
        [
            new AccessPointWizardStepViewModel(
                "basic",
                "Основное",
                "Имя устройства и bridge для локальной сети."),
            new AccessPointWizardStepViewModel(
                "network",
                "Сеть",
                "Получение IP-адреса из существующей сети."),
            new AccessPointWizardStepViewModel(
                "wifi",
                "Wi-Fi",
                "SSID и пароль беспроводной сети."),
            new AccessPointWizardStepViewModel(
                "review",
                "Проверка",
                "Сводка, ошибки и предупреждения."),
            new AccessPointWizardStepViewModel(
                "result",
                "Результат",
                "Предпросмотр .rsc и сохранение файла.")
        ];

        CurrentStepIndex = 0;
    }

    public AccessPointWizardInputViewModel Input { get; } = new();

    public IReadOnlyList<AccessPointWizardStepViewModel> Steps { get; }

    public ICommand BackCommand => _backCommand;

    public ICommand NextCommand => _nextCommand;

    public ICommand GeneratePreviewCommand => _generatePreviewCommand;

    public ICommand SaveFileCommand => _saveFileCommand;

    public int CurrentStepIndex
    {
        get => _currentStepIndex;
        private set
        {
            if (SetProperty(ref _currentStepIndex, value))
            {
                RefreshStepState();
                RefreshReviewState();
                ClearStatusMessage();
                RefreshNavigationState();
                NotifyCurrentStepChanged();
            }
        }
    }

    public AccessPointWizardStepViewModel CurrentStep => Steps[CurrentStepIndex];

    public bool CanGoBack => CurrentStepIndex > 0;

    public bool CanGoNext => CurrentStepIndex < Steps.Count - 1
        && !IsReviewStepVisible;

    public bool IsFirstStep => CurrentStepIndex == 0;

    public bool IsLastStep => CurrentStepIndex == Steps.Count - 1;

    public bool IsBasicStepVisible => CurrentStep.Id == "basic";

    public bool IsNetworkStepVisible => CurrentStep.Id == "network";

    public bool IsWifiStepVisible => CurrentStep.Id == "wifi";

    public bool IsReviewStepVisible => CurrentStep.Id == "review";

    public bool IsResultStepVisible => CurrentStep.Id == "result";

    public bool IsGenerateActionVisible => IsReviewStepVisible;

    public bool IsNextActionVisible => !IsLastStep
        && !IsReviewStepVisible;

    public bool IsSaveActionVisible => IsResultStepVisible;

    public bool IsReturnToTaskSelectionActionVisible => IsResultStepVisible;

    public string GeneratedRsc
    {
        get => _generatedRsc;
        private set
        {
            if (SetProperty(ref _generatedRsc, value))
            {
                _saveFileCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string ValidationMessage
    {
        get => _validationMessage;
        private set => SetProperty(ref _validationMessage, value);
    }

    public IReadOnlyList<string> ValidationErrors
    {
        get => _validationErrors;
        private set
        {
            if (SetProperty(ref _validationErrors, value))
            {
                OnPropertyChanged(nameof(HasValidationErrors));
                _generatePreviewCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public IReadOnlyList<string> ValidationWarnings
    {
        get => _validationWarnings;
        private set
        {
            if (SetProperty(ref _validationWarnings, value))
            {
                OnPropertyChanged(nameof(HasValidationWarnings));
            }
        }
    }

    public bool HasValidationErrors => ValidationErrors.Count > 0;

    public bool HasValidationWarnings => ValidationWarnings.Count > 0;

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public string SummaryDeviceName => string.IsNullOrWhiteSpace(Input.DeviceName)
        ? "Не указано"
        : Input.DeviceName.Trim();

    public string SummaryBridgeName => string.IsNullOrWhiteSpace(Input.BridgeName)
        ? "Не указано"
        : Input.BridgeName.Trim();

    public bool IsStaticManagementIpVisible => !Input.UseDhcpClient;

    public string SummaryAddressMode => Input.UseDhcpClient
        ? "DHCP Client"
        : "Static IP";

    public string SummaryManagementIp => Input.UseDhcpClient
        ? "получается автоматически"
        : $"{Input.ManagementIpAddress.Trim()}/{Input.ManagementPrefixLength}";

    public string SummaryGateway => Input.UseDhcpClient
        ? "получается автоматически"
        : Input.DefaultGateway.Trim();

    public string SummaryDns => Input.UseDhcpClient
        ? "получается автоматически"
        : Input.DnsServers.Trim();

    public string SummarySsid => string.IsNullOrWhiteSpace(Input.Ssid)
        ? "не указан"
        : Input.Ssid.Trim();

    public string SummaryWifiPassword => string.IsNullOrWhiteSpace(Input.WifiPassword)
        ? "не задан"
        : "задан";

    public void MoveBack()
    {
        if (!CanGoBack)
        {
            return;
        }

        CurrentStepIndex--;
    }

    public void MoveNext()
    {
        if (!CanGoNext)
        {
            return;
        }

        CurrentStepIndex++;
    }

    public void MoveToStep(string stepId)
    {
        var stepIndex = Steps
            .Select((step, index) => new { step.Id, Index = index })
            .FirstOrDefault(step => step.Id == stepId)
            ?.Index;

        if (stepIndex.HasValue)
        {
            CurrentStepIndex = stepIndex.Value;
        }
    }

    private void GeneratePreview()
    {
        RefreshValidationState();

        if (HasValidationErrors)
        {
            GeneratedRsc = string.Empty;
            StatusMessage = "Исправьте ошибки перед генерацией .rsc.";
            return;
        }

        GeneratedRsc = _configurationBuilder.Build(Input);
        ValidationMessage = "Ошибок нет.";
        SetReviewHasIssues(false);

        if (IsReviewStepVisible)
        {
            MoveToStep("result");
        }

        StatusMessage = "Конфигурация точки доступа успешно сгенерирована.";
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

    private void RefreshStepState()
    {
        for (var index = 0; index < Steps.Count; index++)
        {
            Steps[index].UpdateState(index, CurrentStepIndex);
        }
    }

    private void RefreshReviewState()
    {
        if (!IsReviewStepVisible)
        {
            return;
        }

        RefreshValidationState();
        NotifySummaryChanged();
    }

    private void RefreshValidationState()
    {
        ValidationErrors = BuildValidationErrors();
        ValidationWarnings = BuildValidationWarnings();
        ValidationMessage = HasValidationErrors
            ? "Исправьте ошибки перед генерацией .rsc."
            : "Ошибок нет. Можно генерировать .rsc.";
        SetReviewHasIssues(HasValidationErrors);
    }

    private IReadOnlyList<string> BuildValidationErrors()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Input.DeviceName))
        {
            errors.Add("Имя устройства не должно быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(Input.BridgeName))
        {
            errors.Add("Bridge name не должен быть пустым.");
        }

        if (!string.IsNullOrWhiteSpace(Input.WifiPassword) && Input.WifiPassword.Length < 8)
        {
            errors.Add("Пароль Wi-Fi должен быть не короче 8 символов.");
        }

        if (!Input.UseDhcpClient)
        {
            if (!IsValidIpv4(Input.ManagementIpAddress))
            {
                errors.Add("IP адрес управления должен быть корректным IPv4-адресом.");
            }

            if (Input.ManagementPrefixLength is < 1 or > 32)
            {
                errors.Add("CIDR должен быть в диапазоне 1-32.");
            }

            if (!IsValidIpv4(Input.DefaultGateway))
            {
                errors.Add("Шлюз должен быть корректным IPv4-адресом.");
            }

            if (GetDnsServers().Count == 0)
            {
                errors.Add("DNS серверы должны быть указаны.");
            }
        }

        return errors;
    }

    private IReadOnlyList<string> BuildValidationWarnings()
    {
        return [];
    }

    private IReadOnlyList<string> GetDnsServers()
    {
        return Input.DnsServers
            .Split(
                new[] { ',', ';', ' ', '\r', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToArray();
    }

    private static bool IsValidIpv4(string value)
    {
        return IPAddress.TryParse(value.Trim(), out var address)
            && address.AddressFamily == AddressFamily.InterNetwork;
    }

    private void RefreshNavigationState()
    {
        _backCommand.RaiseCanExecuteChanged();
        _nextCommand.RaiseCanExecuteChanged();
        _generatePreviewCommand.RaiseCanExecuteChanged();
    }

    private void SetReviewHasIssues(bool hasIssues)
    {
        var reviewStep = Steps.FirstOrDefault(step => step.Id == "review");

        if (reviewStep is not null)
        {
            reviewStep.HasIssues = hasIssues;
        }
    }

    private string BuildDefaultFileName()
    {
        var safeName = string.Join(
            "_",
            Input.DeviceName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

        if (string.IsNullOrWhiteSpace(safeName))
        {
            safeName = "mikrotik-access-point";
        }

        return $"{safeName.Trim()}.rsc";
    }

    private void ClearStatusMessage()
    {
        StatusMessage = string.Empty;
    }

    private void NotifySummaryChanged()
    {
        OnPropertyChanged(nameof(SummaryDeviceName));
        OnPropertyChanged(nameof(SummaryBridgeName));
        OnPropertyChanged(nameof(IsStaticManagementIpVisible));
        OnPropertyChanged(nameof(SummaryAddressMode));
        OnPropertyChanged(nameof(SummaryManagementIp));
        OnPropertyChanged(nameof(SummaryGateway));
        OnPropertyChanged(nameof(SummaryDns));
        OnPropertyChanged(nameof(SummarySsid));
        OnPropertyChanged(nameof(SummaryWifiPassword));
    }

    private void NotifyCurrentStepChanged()
    {
        OnPropertyChanged(nameof(CurrentStep));
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(CanGoNext));
        OnPropertyChanged(nameof(IsFirstStep));
        OnPropertyChanged(nameof(IsLastStep));
        OnPropertyChanged(nameof(IsBasicStepVisible));
        OnPropertyChanged(nameof(IsNetworkStepVisible));
        OnPropertyChanged(nameof(IsWifiStepVisible));
        OnPropertyChanged(nameof(IsReviewStepVisible));
        OnPropertyChanged(nameof(IsResultStepVisible));
        OnPropertyChanged(nameof(IsGenerateActionVisible));
        OnPropertyChanged(nameof(IsNextActionVisible));
        OnPropertyChanged(nameof(IsSaveActionVisible));
        OnPropertyChanged(nameof(IsReturnToTaskSelectionActionVisible));
        NotifySummaryChanged();
    }
}
