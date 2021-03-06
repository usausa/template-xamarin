namespace Template.FormsApp;

using Template.FormsApp.Shell;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        (BindingContext as MainPageViewModel)?.Navigator.NotifyAsync(ShellEvent.Back);
        return true;
    }
}
