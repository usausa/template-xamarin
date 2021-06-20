namespace Template.FormsApp.Modules.Device
{
    using System.Threading.Tasks;

    using Smart.Navigation;

    public class DeviceSensorViewModel : AppViewModelBase
    {
        public DeviceSensorViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
    }
}
