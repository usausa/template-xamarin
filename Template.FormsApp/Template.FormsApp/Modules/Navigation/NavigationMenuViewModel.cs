namespace Template.FormsApp.Modules.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class NavigationMenuViewModel : AppViewModelBase
    {
        public NavigationMenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
    }
}
