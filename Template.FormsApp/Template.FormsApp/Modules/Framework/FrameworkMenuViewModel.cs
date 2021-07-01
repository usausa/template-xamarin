namespace Template.FormsApp.Modules.Framework
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class FrameworkMenuViewModel : AppViewModelBase
    {
        public FrameworkMenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
    }
}