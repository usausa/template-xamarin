namespace Template.FormsApp.Modules.Navigation.Wizard
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.ComponentModel;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput1ViewModel : AppViewModelBase
    {
        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new();

        public ICommand NextCommand { get; }

        public WizardInput1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.NavigationWizardInput2));
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.NavigationMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
