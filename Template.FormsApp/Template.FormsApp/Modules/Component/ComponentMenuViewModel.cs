namespace Template.FormsApp.Modules.Component;

public class ComponentMenuViewModel : AppViewModelBase
{
    public ComponentMenuViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);
}
