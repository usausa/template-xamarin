namespace Template.FormsApp.Modules.Navigation.Stack
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Navigation;

    public class Stack1ViewModel : AppViewModelBase
    {
        public ICommand PushCommand { get; }

        public Stack1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            PushCommand = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.NavigationMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
