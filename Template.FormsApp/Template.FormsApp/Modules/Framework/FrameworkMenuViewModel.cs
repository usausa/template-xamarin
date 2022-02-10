namespace Template.FormsApp.Modules.Framework;

public class FrameworkMenuViewModel : AppViewModelBase
{
    public FrameworkMenuViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

    protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
}
