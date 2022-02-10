namespace Template.FormsApp.Modules.Device;

public class DeviceSensorViewModel : AppViewModelBase
{
    public DeviceSensorViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
}
