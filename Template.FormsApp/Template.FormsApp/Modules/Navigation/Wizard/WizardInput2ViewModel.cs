namespace Template.FormsApp.Modules.Navigation.Wizard
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.ComponentModel;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput2ViewModel : AppViewModelBase
    {
        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new();

        public ICommand NextCommand { get; }

        public WizardInput2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.NavigationWizardResult));
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.NavigationWizardInput1);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
