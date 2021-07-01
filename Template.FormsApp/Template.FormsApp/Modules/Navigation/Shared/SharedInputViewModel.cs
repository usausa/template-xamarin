namespace Template.FormsApp.Modules.Navigation.Shared
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.ComponentModel;
    using Smart.Navigation;

    public class SharedInputViewModel : AppViewModelBase
    {
        private ViewId nextViewId;

        public NotificationValue<string> No { get; } = new();

        public ICommand NextCommand { get; }

        public SharedInputViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            NextCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(nextViewId, Parameters.MakeNextViewId(nextViewId).WithNo(No.Value)));
        }

        public override void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                nextViewId = context.Parameter.GetNextViewId();
            }
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.NavigationMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}