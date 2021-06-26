namespace Template.FormsApp.Modules.Example
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class ExampleMenuViewModel : AppViewModelBase
    {
        public ExampleMenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
    }
}
