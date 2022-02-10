namespace Template.FormsApp.Modules.Device;

public class DeviceMenuViewModel : AppViewModelBase
{
    public DeviceMenuViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
}
