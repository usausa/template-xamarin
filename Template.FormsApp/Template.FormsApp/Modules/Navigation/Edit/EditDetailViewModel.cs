namespace Template.FormsApp.Modules.Navigation.Edit
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.ComponentModel;
    using Smart.Navigation;

    using Template.FormsApp.Models.Entity;
    using Template.FormsApp.Services;

    public class EditDetailViewModel : AppViewModelBase
    {
        private readonly DataService dataService;

        [AllowNull]
        private WorkEntity entity;

        public NotificationValue<bool> IsUpdate { get; } = new();

        public NotificationValue<string> Name { get; } = new();

        public ICommand UpdateCommand { get; }

        public EditDetailViewModel(
            ApplicationState applicationState,
            DataService dataService)
            : base(applicationState)
        {
            this.dataService = dataService;

            UpdateCommand = MakeAsyncCommand(UpdateAsync, () => !String.IsNullOrEmpty(Name.Value))
                .Observe(Name);
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            if (!context.Attribute.IsRestore())
            {
                IsUpdate.Value = Equals(context.ToId, ViewId.NavigationEditDetailUpdate);
                if (IsUpdate.Value)
                {
                    entity = context.Parameter.GetValue<WorkEntity>();
                    Name.Value = entity.Name;
                }
            }
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.NavigationEditList);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();

        private async Task UpdateAsync()
        {
            if (IsUpdate.Value)
            {
                entity.Name = Name.Value;
                await dataService.UpdateWorkAsync(entity);
            }
            else
            {
                await dataService.InsertWorkAsync(Name.Value);
            }

            await Navigator.ForwardAsync(ViewId.NavigationEditList);
        }
    }
}
