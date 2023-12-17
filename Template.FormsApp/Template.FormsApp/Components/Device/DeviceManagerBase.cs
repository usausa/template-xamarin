namespace Template.FormsApp.Components.Device;

using Xamarin.Essentials;

public abstract class DeviceManagerBase : IDeviceManager, IDisposable
{
    private readonly BehaviorSubject<NetworkState> networkState;

    public IObservable<NetworkState> NetworkState => networkState;

    protected DeviceManagerBase()
    {
        networkState = new BehaviorSubject<NetworkState>(GetNetworkState(Connectivity.NetworkAccess, Connectivity.ConnectionProfiles));
        Connectivity.ConnectivityChanged += (_, args) =>
        {
            networkState.OnNext(GetNetworkState(args.NetworkAccess, args.ConnectionProfiles));
        };
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            networkState.Dispose();
        }
    }

    private static NetworkState GetNetworkState(NetworkAccess access, IEnumerable<ConnectionProfile> profiles)
    {
        if (access != NetworkAccess.None && access != NetworkAccess.Unknown)
        {
            return profiles.Any(x => x == ConnectionProfile.Ethernet || x == ConnectionProfile.WiFi)
                ? Template.FormsApp.Components.Device.NetworkState.ConnectedHighSpeed
                : Template.FormsApp.Components.Device.NetworkState.Connected;
        }

        return Template.FormsApp.Components.Device.NetworkState.Disconnected;
    }

    public NetworkState GetNetworkState() => GetNetworkState(Connectivity.NetworkAccess, Connectivity.ConnectionProfiles);

    public abstract void SetOrientation(Orientation orientation);

    public abstract string? GetVersion();
}
