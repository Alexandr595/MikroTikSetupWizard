using System.Windows.Input;
using MikroTikSetupWizard.Application.Setup;

namespace MikroTikSetupWizard.Desktop.ViewModels;

public sealed class OfficeRouterWizardViewModel : ObservableObject
{
    private readonly RelayCommand _backCommand;
    private readonly RelayCommand _nextCommand;
    private int _currentStepIndex = -1;

    public OfficeRouterWizardViewModel()
    {
        _backCommand = new RelayCommand(_ => MoveBack(), _ => CanGoBack);
        _nextCommand = new RelayCommand(_ => MoveNext(), _ => CanGoNext);

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

    public bool CanGoNext => CurrentStepIndex < Steps.Count - 1;

    public bool IsFirstStep => CurrentStepIndex == 0;

    public bool IsLastStep => CurrentStepIndex == Steps.Count - 1;

    public bool IsInternetStepVisible => CurrentStep.Id == "internet";

    public bool IsLocalNetworkStepVisible => CurrentStep.Id == "local-network";

    public bool IsSecurityStepVisible => CurrentStep.Id == "security";

    public bool IsReviewStepVisible => CurrentStep.Id == "review";

    public bool IsResultStepVisible => CurrentStep.Id == "result";

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
    }
}
