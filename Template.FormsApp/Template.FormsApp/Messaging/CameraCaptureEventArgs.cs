namespace Template.FormsApp.Messaging;

public sealed class CameraCaptureEventArgs : EventArgs
{
    public TaskCompletionSource<byte[]?> CompletionSource { get; } = new();
}
