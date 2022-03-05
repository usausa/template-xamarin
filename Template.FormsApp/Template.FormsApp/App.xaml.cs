namespace Template.FormsApp;

using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

using Rester;

using Smart.Data.Mapper;
using Smart.Forms.Resolver;
using Smart.Resolver;

using Template.FormsApp.Components.Dialog;
using Template.FormsApp.Extender;
using Template.FormsApp.Helpers;
using Template.FormsApp.Helpers.Data;
using Template.FormsApp.Helpers.Json;
using Template.FormsApp.Modules;
using Template.FormsApp.Services;
using Template.FormsApp.State;
using Template.FormsApp.Usecase;

using Xamarin.Essentials;

using XamarinFormsComponents;
using XamarinFormsComponents.Popup;

public partial class App
{
    private readonly SmartResolver resolver;

    private readonly Navigator navigator;

    public App(IComponentProvider provider)
    {
        InitializeComponent();

        // Config DataMapper
        SqlMapperConfig.Default.ConfigureTypeHandlers(config =>
        {
            config[typeof(DateTime)] = new DateTimeTypeHandler();
            config[typeof(Guid)] = new GuidTypeHandler();
        });

        // Config Rest
        RestConfig.Default.UseJsonSerializer(config =>
        {
            config.Converters.Add(new DateTimeConverter());
            config.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            config.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        // Config Resolver
        resolver = CreateResolver(provider);
        ResolveProvider.Default.UseSmartResolver(resolver);

        // Config Navigator
        navigator = new NavigatorConfig()
            .UseFormsNavigationProvider()
            .AddPlugin<NavigationFocusPlugin>()
            .UseResolver(resolver)
            .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
            .ToNavigator();
        navigator.Navigated += (_, args) =>
        {
            // for debug
            System.Diagnostics.Debug.WriteLine(
                $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
        };

        // Popup Navigator
        var popupNavigator = resolver.Get<IPopupNavigator>();
        popupNavigator.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes);

        // Show MainWindow
        MainPage = resolver.Get<MainPage>();
    }

    private SmartResolver CreateResolver(IComponentProvider provider)
    {
        var config = new ResolverConfig()
            .UseAutoBinding()
            .UseArrayBinding()
            .UseAssignableBinding()
            .UsePropertyInjector()
            .UsePageContextScope();

        config.UseXamarinFormsComponents(adapter =>
        {
            adapter.AddDialogs();
            adapter.AddPopupNavigator();
            adapter.AddJsonSerializer();
            adapter.AddSettings();

            // Custom
            adapter.UsePopupPageFactory<PopupPageFactory>();
        });

        config.BindSingleton<INavigator>(_ => navigator);

        config.BindSingleton<ApplicationState>();

        config.BindSingleton<Configuration>();
        config.BindSingleton<Session>();

        config.BindSingleton(new DataServiceOptions
        {
            Path = Path.Combine(FileSystem.AppDataDirectory, "Mobile.db")
        });
        config.BindSingleton<DataService>();
        config.BindSingleton<NetworkService>();

        config.BindSingleton<NetworkOperator>();

        config.BindSingleton<SampleUsecase>();

        provider.RegisterComponents(config);

        return config.ToResolver();
    }

    protected override async void OnStart()
    {
        var dialogs = resolver.Get<IApplicationDialog>();
        var configuration = resolver.Get<Configuration>();
        var dataService = resolver.Get<DataService>();
        var networkService = resolver.Get<NetworkService>();

        // Crash report
        await CrashReportHelper.ShowReport();

        // Permission
        while (await Permissions.IsPermissionRequired())
        {
            await Permissions.RequestPermissions();

            if (await Permissions.IsPermissionRequired())
            {
                await dialogs.Information("Permission required.");
            }
        }

        // Initialize
        networkService.SetAddress(configuration.ApiEndPoint);
        networkService.SetToken(Definition.ApiToken);

        // Database
        await dataService.PrepareAsync();

        // Navigate
        await navigator.ForwardAsync(ViewId.Menu);
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}
