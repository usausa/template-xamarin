[assembly: Xamarin.Forms.ExportEffect(typeof(Template.FormsApp.Droid.Effects.InputFilterPlatformEffect), nameof(Template.FormsApp.Effects.InputFilterEffect))]

namespace Template.FormsApp.Droid.Effects;

using Android.Text;
using Android.Widget;

using Java.Lang;

using Template.FormsApp.Effects;

using Xamarin.Forms.Platform.Android;

public sealed class InputFilterPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        UpdateFilter();
    }

    protected override void OnDetached()
    {
    }

    protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
    {
        base.OnElementPropertyChanged(args);

        if (args.PropertyName == InputFilterEffect.RuleProperty.PropertyName)
        {
            UpdateFilter();
        }
    }

    private void UpdateFilter()
    {
        if (Control is EditText editText)
        {
            var rule = InputFilterEffect.GetRule(Element);
            editText.SetFilters(rule is null ? Array.Empty<IInputFilter>() : new IInputFilter[] { new RuleInputFilter(rule) });
        }
    }

    private class RuleInputFilter : Java.Lang.Object, IInputFilter
    {
        private readonly Func<string, bool> rule;

        public RuleInputFilter(Func<string, bool> rule)
        {
            this.rule = rule;
        }

        public ICharSequence? FilterFormatted(ICharSequence? source, int start, int end, ISpanned? dest, int dstart, int dend)
        {
            var value = dest!.SubSequence(0, dstart) + source!.SubSequence(start, end) + dest!.SubSequence(dend, dest!.Length());
            return rule(value) ? source : new Java.Lang.String(dest.SubSequence(dstart, dend));
        }
    }
}
