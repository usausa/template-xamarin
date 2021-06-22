namespace Template.FormsApp.Modules.Navigation.Shared
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.ComponentModel;
    using Smart.Navigation;

    public class SharedMain2ViewModel : AppViewModelBase
    {
        public NotificationValue<string> No { get; } = new();

        public ICommand CompleteCommand { get; }

        public SharedMain2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            CompleteCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));
        }

        public override void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                No.Value = context.Parameter.GetNo();
            }
        }

        protected override Task OnNotifyBackAsync() =>
            Navigator.ForwardAsync(ViewId.NavigationSharedInput, Parameters.MakeNextViewId(ViewId.NavigationSharedMain2));

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
