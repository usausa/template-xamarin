namespace Template.FormsApp.Components.Device;

public interface IDeviceManager
{
    IObservable<NetworkState> NetworkState { get; }

    NetworkState GetNetworkState();

    void SetOrientation(Orientation orientation);

    string? GetVersion();
}
