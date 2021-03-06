namespace Template.FormsApp.Modules.Key;

using Template.FormsApp.Components.Dialog;

public class KeyMenuViewModel : AppViewModelBase
{
    private int selected = -1;

    public ICommand ForwardCommand { get; }
    public ICommand InformationCommand { get; }
    public ICommand ConfirmCommand { get; }
    public ICommand SelectCommand { get; }

    public KeyMenuViewModel(
        ApplicationState applicationState,
        IApplicationDialog dialog)
        : base(applicationState)
    {
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        InformationCommand = MakeAsyncCommand(async () =>
        {
            await dialog.Information("message");
        });
        ConfirmCommand = MakeAsyncCommand(async () =>
        {
            var ret = await dialog.Confirm("message");
            await dialog.Information($"result=[{ret}]");
        });
        SelectCommand = MakeAsyncCommand(async () =>
        {
            selected = await dialog.Select(Enumerable.Range(1, 15).Select(x => $"Item-{x}").ToArray(), selected);
            await dialog.Information($"result=[{selected}]");
        });
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

    protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
}
