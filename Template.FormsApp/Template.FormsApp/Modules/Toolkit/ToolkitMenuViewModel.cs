namespace Template.FormsApp.Modules.Toolkit;

public class ToolkitMenuViewModel : AppViewModelBase
{
    public ToolkitMenuViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
}
