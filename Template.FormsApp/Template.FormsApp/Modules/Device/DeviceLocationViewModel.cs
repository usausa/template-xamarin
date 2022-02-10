namespace Template.FormsApp.Modules.Device;

public class DeviceLocationViewModel : AppViewModelBase
{
    public DeviceLocationViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
}
