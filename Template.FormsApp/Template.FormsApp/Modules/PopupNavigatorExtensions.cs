namespace Template.FormsApp.Modules
{
    using System.Threading.Tasks;

    using Template.FormsApp.Input;
    using Template.FormsApp.Models.Input;

    using XamarinFormsComponents.Popup;

    public static class PopupNavigatorExtensions
    {
        public static ValueTask<string?> InputNumberAsync(this IPopupNavigator popupNavigator, string value, int maxLength)
        {
            return FocusHelper.WithRestoreFocus(() =>
                popupNavigator.PopupAsync<NumberInputParameter, string?>(
                    DialogId.InputNumber,
                    new NumberInputParameter(value, maxLength, 0, false)));
        }
    }
}
