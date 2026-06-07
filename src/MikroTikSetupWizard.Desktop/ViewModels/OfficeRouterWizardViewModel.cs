using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Input;
using MikroTikSetupWizard.Application.Setup;
using MikroTikSetupWizard.Desktop.Dialogs;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class OfficeRouterWizardViewModel : ObservableObject
{
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

    public OfficeRouterWizardViewModel(
        IMikroTikSetupWizardService setupWizardService,
        ISaveFileDialogService saveFileDialogService)
    {
        _setupWizardService = setupWizardService;
        _saveFileDialogService = saveFileDialogService;
        _backCommand = new RelayCommand(_ => MoveBack(), _ => CanGoBack);
        _nextCommand = new RelayCommand(_ => MoveNext(), _ => CanGoNext);
        _generatePreviewCommand = new RelayCommand(_ => GeneratePreview(), _ => !HasValidationErrors);
        _saveFileCommand = new RelayCommand(async _ => await SaveFileAsync(), _ => !string.IsNullOrWhiteSpace(GeneratedRsc));

        Steps =
        [
            new OfficeRouterWizardStepViewModel(
                "internet",
                "Интернет",
                "WAN-интерфейс, тип подключения и DNS."),
            new OfficeRouterWizardStepViewModel(
                "local-network",
                "Локальная сеть",
                "LAN bridge, IP/CIDR и DHCP-диапазон."),
            new OfficeRouterWizardStepViewModel(
                "security",
                "Безопасность",
                "Администратор, NAT и базовый firewall."),
            new OfficeRouterWizardStepViewModel(
                "review",
                "Проверка",
                "Ошибки, предупреждения и список создаваемых объектов."),
            new OfficeRouterWizardStepViewModel(
                "result",
                "Результат",
                "Предпросмотр .rsc и сохранение файла.")
        ];

        CurrentStepIndex = 0;
    }

    public OfficeRouterWizardInputViewModel Input { get; } = new();

    public IReadOnlyList<OfficeRouterWizardStepViewModel> Steps { get; }

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

    public OfficeRouterWizardStepViewModel CurrentStep => Steps[CurrentStepIndex];

    public bool CanGoBack => CurrentStepIndex > 0;

    public bool CanGoNext => CurrentStepIndex < Steps.Count - 1
        && !IsReviewStepVisible;

    public bool IsFirstStep => CurrentStepIndex == 0;

    public bool IsLastStep => CurrentStepIndex == Steps.Count - 1;

    public bool IsInternetStepVisible => CurrentStep.Id == "internet";

    public bool IsLocalNetworkStepVisible => CurrentStep.Id == "local-network";

    public bool IsSecurityStepVisible => CurrentStep.Id == "security";

    public bool IsReviewStepVisible => CurrentStep.Id == "review";

    public bool IsResultStepVisible => CurrentStep.Id == "result";

    public bool HasGeneratedPreview => !string.IsNullOrWhiteSpace(GeneratedRsc);

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
                RefreshNavigationState();
                OnPropertyChanged(nameof(HasGeneratedPreview));
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

    public string SummaryRouterName => string.IsNullOrWhiteSpace(Input.RouterName)
        ? "Не указано"
        : Input.RouterName.Trim();

    public string SummaryWan => string.IsNullOrWhiteSpace(Input.WanInterface)
        ? "Не указано"
        : Input.WanInterface.Trim();

    public string SummaryLan
    {
        get
        {
            var (address, prefixLength, _) = GetLanAddressParts();
            return string.IsNullOrWhiteSpace(address)
                ? "Не указано"
                : $"{address}/{prefixLength}";
        }
    }

    public string SummaryDhcp => Input.DhcpEnabled
        ? $"{Input.DhcpPoolStart.Trim()} - {Input.DhcpPoolEnd.Trim()}"
        : "выключен";

    public string SummaryDns
    {
        get
        {
            var dnsServers = GetDnsServers();
            return dnsServers.Count == 0
                ? "Не указано"
                : string.Join(", ", dnsServers);
        }
    }

    public string SummaryNat => Input.EnableNat ? "включён" : "выключен";

    public string SummaryFirewall => Input.EnableBasicFirewall ? "включён" : "выключен";

    public string SummaryAdmin => string.IsNullOrWhiteSpace(Input.AdminUserName)
        ? "Не указано"
        : Input.AdminUserName.Trim();

    public BasicSetupInputDto BuildBasicSetupInput()
    {
        return Input.ToBasicSetupInputDto();
    }

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

    private void RefreshStepState()
    {
        for (var index = 0; index < Steps.Count; index++)
        {
            Steps[index].UpdateState(index, CurrentStepIndex);
        }
    }

    private void RefreshNavigationState()
    {
        _backCommand.RaiseCanExecuteChanged();
        _nextCommand.RaiseCanExecuteChanged();
        _generatePreviewCommand.RaiseCanExecuteChanged();
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

        var result = _setupWizardService.GeneratePreview(BuildBasicSetupInput());

        if (!result.IsSuccess)
        {
            GeneratedRsc = string.Empty;
            ValidationErrors = result.Issues
                .Select(issue => issue.Message)
                .ToArray();
            ValidationMessage = FormatValidation(result.Issues);
            SetReviewHasIssues(true);
            StatusMessage = "Исправьте ошибки перед генерацией .rsc.";
            return;
        }

        ValidationErrors = [];
        ValidationMessage = "Ошибок нет.";
        SetReviewHasIssues(false);
        GeneratedRsc = result.RscText;

        if (IsReviewStepVisible)
        {
            MoveToStep("result");
        }

        StatusMessage = "Конфигурация успешно сгенерирована.";
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
        var (lanAddress, prefixLength, hasInvalidCidrPrefix) = GetLanAddressParts();

        if (string.IsNullOrWhiteSpace(Input.RouterName))
        {
            errors.Add("Имя роутера не должно быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(Input.WanInterface))
        {
            errors.Add("WAN interface не должен быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(Input.LanBridgeName))
        {
            errors.Add("LAN bridge не должен быть пустым.");
        }

        if (!IsValidIpv4(lanAddress))
        {
            errors.Add("LAN IP должен быть корректным IPv4-адресом.");
        }

        if (hasInvalidCidrPrefix || prefixLength is < 1 or > 32)
        {
            errors.Add("Prefix length должен быть в диапазоне 1-32.");
        }

        if (!IsValidIpv4(Input.DhcpPoolStart))
        {
            errors.Add("DHCP range start должен быть корректным IPv4-адресом.");
        }

        if (!IsValidIpv4(Input.DhcpPoolEnd))
        {
            errors.Add("DHCP range end должен быть корректным IPv4-адресом.");
        }

        if (GetDnsServers().Count == 0)
        {
            errors.Add("DNS список не должен быть пустым.");
        }

        return errors;
    }

    private IReadOnlyList<string> BuildValidationWarnings()
    {
        var warnings = new List<string>();
        var publicDnsServers = GetDnsServers()
            .Where(IsKnownPublicDns)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (string.IsNullOrWhiteSpace(Input.AdminPassword))
        {
            warnings.Add("Пароль администратора пустой.");
        }

        if (publicDnsServers.Length > 0)
        {
            warnings.Add("Используются внешние DNS серверы (Cloudflare/Google).");
        }

        return warnings;
    }

    private (string Address, int PrefixLength, bool HasInvalidPrefix) GetLanAddressParts()
    {
        var parts = Input.LanAddress.Split('/', StringSplitOptions.TrimEntries);

        if (parts.Length == 1)
        {
            return (parts[0], Input.LanPrefixLength, false);
        }

        if (parts.Length == 2 && int.TryParse(parts[1], out var prefixLength))
        {
            return (parts[0], prefixLength, false);
        }

        return (parts[0], Input.LanPrefixLength, true);
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

    private static bool IsKnownPublicDns(string value)
    {
        return value is
            "1.1.1.1" or
            "1.0.0.1" or
            "8.8.8.8" or
            "8.8.4.4" or
            "9.9.9.9" or
            "149.112.112.112" or
            "208.67.222.222" or
            "208.67.220.220";
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
            Input.RouterName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

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

    private void ClearStatusMessage()
    {
        StatusMessage = string.Empty;
    }

    private void NotifySummaryChanged()
    {
        OnPropertyChanged(nameof(SummaryRouterName));
        OnPropertyChanged(nameof(SummaryWan));
        OnPropertyChanged(nameof(SummaryLan));
        OnPropertyChanged(nameof(SummaryDhcp));
        OnPropertyChanged(nameof(SummaryDns));
        OnPropertyChanged(nameof(SummaryNat));
        OnPropertyChanged(nameof(SummaryFirewall));
        OnPropertyChanged(nameof(SummaryAdmin));
    }

    private void NotifyCurrentStepChanged()
    {
        OnPropertyChanged(nameof(CurrentStep));
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(CanGoNext));
        OnPropertyChanged(nameof(IsFirstStep));
        OnPropertyChanged(nameof(IsLastStep));
        OnPropertyChanged(nameof(IsInternetStepVisible));
        OnPropertyChanged(nameof(IsLocalNetworkStepVisible));
        OnPropertyChanged(nameof(IsSecurityStepVisible));
        OnPropertyChanged(nameof(IsReviewStepVisible));
        OnPropertyChanged(nameof(IsResultStepVisible));
        OnPropertyChanged(nameof(IsGenerateActionVisible));
        OnPropertyChanged(nameof(IsNextActionVisible));
        OnPropertyChanged(nameof(IsSaveActionVisible));
        OnPropertyChanged(nameof(IsReturnToTaskSelectionActionVisible));
        NotifySummaryChanged();
    }
}
