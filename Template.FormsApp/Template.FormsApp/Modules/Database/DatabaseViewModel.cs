namespace Template.FormsApp.Modules.Database
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class DatabaseViewModel : AppViewModelBase
    {
        public DatabaseViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
