[assembly: Xamarin.Forms.ExportEffect(typeof(Template.FormsApp.Droid.Effects.BorderPlatformEffect), nameof(Template.FormsApp.Effects.BorderEffect))]

namespace Template.FormsApp.Droid.Effects
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    using Android.Graphics.Drawables;

    using Template.FormsApp.Effects;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public sealed class BorderPlatformEffect : PlatformEffect
    {
        private Drawable? originalBackground;

        [AllowNull]
        private GradientDrawable drawable;

        protected override void OnAttached()
        {
            originalBackground = Control.Background;
            drawable = new GradientDrawable();
            Control.Background = drawable;

            UpdateBorder();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Ignore")]
        protected override void OnDetached()
        {
            drawable.Dispose();
            Control.Background = originalBackground;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if ((args.PropertyName == Border.WidthProperty.PropertyName) ||
                (args.PropertyName == Border.ColorProperty.PropertyName) ||
                (args.PropertyName == VisualElement.BackgroundColorProperty.PropertyName))
            {
                UpdateBorder();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Ignore")]
        private void UpdateBorder()
        {
            var width = (int)Control.Context.ToPixels(Border.GetWidth(Element));
            var color = Border.GetColor(Element).ToAndroid();

            drawable.SetStroke(width, color);
            drawable.SetColor(((View)Element).BackgroundColor.ToAndroid());

            Control.Background = drawable;
        }
    }
}
