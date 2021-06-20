namespace Template.FormsApp.Modules.Interface
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class InterfaceMenuViewModel : AppViewModelBase
    {
        public InterfaceMenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
    }
}
