namespace Template.FormsApp.Modules.Navigation.Stack
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Navigation;

    public class Stack3ViewModel : AppViewModelBase
    {
        public ICommand PopCommand { get; }

        public Stack3ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            PopCommand = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        }

        protected override Task OnNotifyBackAsync() => Navigator.PopAsync(1);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
