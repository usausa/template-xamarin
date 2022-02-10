namespace Template.FormsApp.Modules;

using XamarinFormsComponents.Popup;

public class AppDialogViewModelBase : ViewModelBase, IPopupNavigatorAware
{
    [AllowNull]
    public IPopupNavigator PopupNavigator { get; set; }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
    }
}
