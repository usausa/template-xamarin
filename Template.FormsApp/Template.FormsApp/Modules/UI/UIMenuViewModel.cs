namespace Template.FormsApp.Modules.UI;

public class UIMenuViewModel : AppViewModelBase
{
    public UIMenuViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
}
