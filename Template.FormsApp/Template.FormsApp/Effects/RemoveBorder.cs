namespace Template.FormsApp.Effects;

using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

public static class RemoveBorder
{
    public static readonly BindableProperty OnProperty = BindableProperty.CreateAttached(
        "On",
        typeof(bool),
        typeof(RemoveBorder),
        false,
        propertyChanged: OnOnChanged);

    public static bool GetOn(BindableObject bindable) => (bool)bindable.GetValue(OnProperty);

    public static void SetOn(BindableObject bindable, bool value) => bindable.SetValue(OnProperty, value);

    private static void OnOnChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Entry entry)
        {
            return;
        }

        if ((bool)newValue)
        {
            entry.Effects.Add(new RemoveBorderEffect());
        }
        else
        {
            var effect = entry.Effects.FirstOrDefault(x => x is RemoveBorderEffect);
            if (effect != null)
            {
                entry.Effects.Remove(effect);
            }
        }
    }
}
