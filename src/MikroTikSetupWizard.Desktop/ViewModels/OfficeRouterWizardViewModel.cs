using System.IO;
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
    private readonly RelayCommand _saveFileCommand;
    private int _currentStepIndex = -1;
    private string _generatedRsc = string.Empty;
    private string _validationMessage = "Проверка ещё не выполнялась.";
    private string _statusMessage = "Заполните шаги мастера и перейдите к проверке.";

    public OfficeRouterWizardViewModel(
        IMikroTikSetupWizardService setupWizardService,
        ISaveFileDialogService saveFileDialogService)
    {
        _setupWizardService = setupWizardService;
        _saveFileDialogService = saveFileDialogService;
        _backCommand = new RelayCommand(_ => MoveBack(), _ => CanGoBack);
        _nextCommand = new RelayCommand(_ => MoveNext(), _ => CanGoNext);
        GeneratePreviewCommand = new RelayCommand(_ => GeneratePreview());
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

    public ICommand GeneratePreviewCommand { get; }

    public ICommand SaveFileCommand => _saveFileCommand;

    public int CurrentStepIndex
    {
        get => _currentStepIndex;
        private set
        {
            if (SetProperty(ref _currentStepIndex, value))
            {
                RefreshStepState();
                RefreshNavigationState();
                NotifyCurrentStepChanged();
            }
        }
    }

    public OfficeRouterWizardStepViewModel CurrentStep => Steps[CurrentStepIndex];

    public bool CanGoBack => CurrentStepIndex > 0;

    public bool CanGoNext => CurrentStepIndex < Steps.Count - 1
        && (!IsReviewStepVisible || HasGeneratedPreview);

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
        && (!IsReviewStepVisible || HasGeneratedPreview);

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
                OnPropertyChanged(nameof(IsNextActionVisible));
            }
        }
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
        GeneratePreviewForReviewOrResult();
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
    }

    private void GeneratePreview()
    {
        var result = _setupWizardService.GeneratePreview(BuildBasicSetupInput());
        ValidationMessage = FormatValidation(result.Issues);
        SetReviewHasIssues(result.Issues.Count > 0);

        if (!result.IsSuccess)
        {
            GeneratedRsc = string.Empty;
            StatusMessage = "Исправьте ошибки перед сохранением .rsc.";
            return;
        }

        GeneratedRsc = result.RscText;
        StatusMessage = "Предпросмотр .rsc обновлён.";
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

    private void GeneratePreviewForReviewOrResult()
    {
        if (IsReviewStepVisible || IsResultStepVisible)
        {
            GeneratePreview();
        }
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
    }
}
