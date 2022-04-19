namespace Template.FormsApp.Droid;

using System;

using Acr.UserDialogs;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;

using Smart.Forms.Resolver;
using Smart.Resolver;

using Template.FormsApp.Components.Device;
using Template.FormsApp.Components.Dialog;
using Template.FormsApp.Components.Nfc;
using Template.FormsApp.Droid.Components.Device;
using Template.FormsApp.Droid.Components.Dialog;
using Template.FormsApp.Droid.Components.Nfc;
using Template.FormsApp.Helpers;

using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

[Activity(
    Name = "template.app.MainActivity",
    Icon = "@mipmap/icon",
    Theme = "@style/MainTheme.Splash",
    MainLauncher = true,
    AlwaysRetainTaskState = true,
    LaunchMode = LaunchMode.SingleInstance,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
    ScreenOrientation = ScreenOrientation.Portrait,
    WindowSoftInputMode = SoftInput.AdjustResize)]
public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
{
    private DeviceManager deviceManager = default!;

    private KeyInputDriver keyInputDriver = default!;

    private AndroidNfcReader nfcReader = default!;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        SetTheme(Resource.Style.MainTheme);
        base.OnCreate(savedInstanceState);

        // Setup crash report
        //AppDomain.CurrentDomain.UnhandledException += (sender, args) => CrashReport(args.ExceptionObject as Exception);
        TaskScheduler.UnobservedTaskException += (_, args) => CrashReport(args.Exception);
        AndroidEnvironment.UnhandledExceptionRaiser += (_, args) => CrashReport(args.Exception);

        // Database
        SQLitePCL.Batteries_V2.Init();

        // Barcode
        ZXing.Net.Mobile.Forms.Android.Platform.Init();

        // Service
        deviceManager = new DeviceManager(this);
        nfcReader = new AndroidNfcReader(this);

        // Components
        UserDialogs.Init(this);
        Rg.Plugins.Popup.Popup.Init(this);

        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        Xamarin.Forms.Forms.Init(this, savedInstanceState);

        // Key input
        keyInputDriver = new KeyInputDriver(this);

        // Boot
        LoadApplication(new App(new ComponentProvider(this)));

        // Adjust soft input
        Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
            .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    public override void OnBackPressed()
    {
        Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
    }

    public override bool DispatchKeyEvent(KeyEvent? e)
    {
        if (keyInputDriver.Process(e!))
        {
            return true;
        }

        return base.DispatchKeyEvent(e);
    }

    protected override void OnPause()
    {
        nfcReader.Pause();

        base.OnPause();
    }

    protected override void OnResume()
    {
        base.OnResume();

        nfcReader.Resume();
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        nfcReader.OnNewIntent(intent);
    }

    private static void CrashReport(Exception ex)
    {
        Log.Error("CrashReport", ex.ToString());
        CrashReportHelper.LogException(ex);
    }

    private sealed class ComponentProvider : IComponentProvider
    {
        private readonly MainActivity activity;

        public ComponentProvider(MainActivity activity)
        {
            this.activity = activity;
        }

        public void RegisterComponents(ResolverConfig config)
        {
            config.Bind<Activity>().ToConstant(activity).InSingletonScope();

            config.Bind<IApplicationDialog>().To<ApplicationDialog>().InSingletonScope();
            config.Bind<IDeviceManager>().ToConstant(activity.deviceManager).InSingletonScope();
            config.Bind<INfcReader>().ToConstant(activity.nfcReader).InSingletonScope();
        }
    }
}
