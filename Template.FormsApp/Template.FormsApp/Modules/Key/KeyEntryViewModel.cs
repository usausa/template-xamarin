namespace Template.FormsApp.Modules.Key
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Navigation;

    using Template.FormsApp.Models.Entry;

    public class KeyEntryViewModel : AppViewModelBase
    {
        public EntryModel Input1 { get; }
        public EntryModel Input2 { get; }
        public EntryModel Input3 { get; }

        public ICommand SwitchCommand { get; }
        public ICommand SetCommand { get; }

        public KeyEntryViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Input1 = new EntryModel(MakeDelegateCommand<EntryCompleteEvent>(Input1Complete));
            Input2 = new EntryModel(MakeDelegateCommand<EntryCompleteEvent>(Input2Complete));
            Input3 = new EntryModel(MakeDelegateCommand<EntryCompleteEvent>(Input3Complete));

            SwitchCommand = MakeDelegateCommand(() => Input1.Enable = !Input1.Enable);
            SetCommand = MakeDelegateCommand(() => Input3.Text = "123");
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.KeyMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();

        private void Input1Complete(EntryCompleteEvent ice)
        {
            ice.HasError = String.IsNullOrEmpty(Input1.Text);
            Debug.WriteLine($"**** Input1 completed {Input1.Text}");
        }

        private void Input2Complete(EntryCompleteEvent ice)
        {
            ice.HasError = String.IsNullOrEmpty(Input2.Text);
            Debug.WriteLine($"**** Input2 completed {Input2.Text}");
        }

        private void Input3Complete(EntryCompleteEvent ice)
        {
            ice.HasError = String.IsNullOrEmpty(Input3.Text);
            Debug.WriteLine($"**** Input3 completed {Input3.Text}");
        }
    }
}
