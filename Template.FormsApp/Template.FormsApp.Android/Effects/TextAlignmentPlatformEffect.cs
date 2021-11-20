[assembly: Xamarin.Forms.ExportEffect(typeof(Template.FormsApp.Droid.Effects.TextAlignmentPlatformEffect), nameof(Template.FormsApp.Effects.TextAlignmentEffect))]

namespace Template.FormsApp.Droid.Effects;

using System.ComponentModel;

using Android.Widget;

using Template.FormsApp.Droid.Helpers;
using Template.FormsApp.Effects;

using Xamarin.Forms.Platform.Android;

public sealed class TextAlignmentPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        UpdateAlignment();
    }

    protected override void OnDetached()
    {
    }

    protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
    {
        base.OnElementPropertyChanged(args);

        if ((args.PropertyName == TextAlignment.VerticalProperty.PropertyName) ||
            (args.PropertyName == TextAlignment.HorizontalProperty.PropertyName))
        {
            UpdateAlignment();
        }
    }

    private void UpdateAlignment()
    {
        if (Control is TextView textView)
        {
            textView.Gravity = TextAlignment.GetVertical(Element).ToVerticalGravity() |
                               TextAlignment.GetHorizontal(Element).ToHorizontalGravity();
        }
    }
}
