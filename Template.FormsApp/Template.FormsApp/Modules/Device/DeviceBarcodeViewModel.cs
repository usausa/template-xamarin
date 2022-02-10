namespace Template.FormsApp.Modules.Device;

public class DeviceBarcodeViewModel : AppViewModelBase
{
    public DeviceBarcodeViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.DeviceMenu);
}
