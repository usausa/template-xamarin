namespace Template.FormsApp.Modules.Device;

public class DeviceMapViewModel : AppViewModelBase
{
    public DeviceMapViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
}
