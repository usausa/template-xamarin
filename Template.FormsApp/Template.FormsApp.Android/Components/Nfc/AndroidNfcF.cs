namespace Template.FormsApp.Droid.Components.Nfc;

using Android.Nfc;
using Android.Nfc.Tech;
using Android.Util;

using Template.FormsApp.Components.Nfc;

public sealed class AndroidNfcF : INfc
{
    private readonly NfcF nfc;

    public byte[] Id { get; }

    public AndroidNfcF(byte[] id, NfcF nfc)
    {
        Id = id;
        this.nfc = nfc;
    }

    public byte[] Access(byte[] command)
    {
        Log.Debug("NFC", $"Command: {BitConverter.ToString(command)}");
        try
        {
            var response = nfc.Transceive(command);
            Log.Debug("NFC", $"Response: {BitConverter.ToString(response)}");
            return response ?? Array.Empty<byte>();
        }
        catch (TagLostException ex)
        {
            Log.Error("NFC", ex, "Tag lost.");
            return Array.Empty<byte>();
        }
    }
}
