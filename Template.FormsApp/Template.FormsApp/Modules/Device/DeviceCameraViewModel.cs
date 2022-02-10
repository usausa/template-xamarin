namespace Template.FormsApp.Modules.Device;

public class DeviceCameraViewModel : AppViewModelBase
{
    public DeviceCameraViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
}
