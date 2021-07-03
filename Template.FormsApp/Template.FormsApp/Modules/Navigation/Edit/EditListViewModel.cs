namespace Template.FormsApp.Modules.Navigation.Edit
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.Collections.Generic;
    using Smart.Navigation;

    using Template.FormsApp.Models.Entity;
    using Template.FormsApp.Services;

    public class EditListViewModel : AppViewModelBase
    {
        private readonly DataService dataService;

        public ObservableCollection<WorkEntity> Items { get; } = new();

        public ICommand SelectCommand { get; }

        public EditListViewModel(
            ApplicationState applicationState,
            DataService dataService)
            : base(applicationState)
        {
            this.dataService = dataService;

            SelectCommand = MakeAsyncCommand<WorkEntity>(x =>
                Navigator.ForwardAsync(ViewId.NavigationEditDetailUpdate, new NavigationParameter().SetValue(x)));
        }

        public override async void OnNavigatedTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                Items.AddRange(await dataService.QueryWorkListAsync());
            }
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.NavigationMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();

        protected override Task OnNotifyFunction4() => Navigator.ForwardAsync(ViewId.NavigationEditDetailNew);
    }
}
