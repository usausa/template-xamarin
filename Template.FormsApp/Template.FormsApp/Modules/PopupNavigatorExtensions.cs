namespace Template.FormsApp.Modules;

using Template.FormsApp.Input;

using XamarinFormsComponents.Popup;

public static class PopupNavigatorExtensions
{
    public static ValueTask<string?> InputNumberAsync(this IPopupNavigator popupNavigator, string title, string value, int maxLength)
    {
        return FocusHelper.WithRestoreFocus(() =>
            popupNavigator.PopupAsync<NumberInputParameter, string?>(
                DialogId.InputNumber,
                new NumberInputParameter(title, value, maxLength)));
    }
}
