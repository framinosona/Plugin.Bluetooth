namespace Plugin.Bluetooth.Maui;

/// <inheritdoc />
public class BluetoothBroadcaster : BaseBluetoothBroadcaster
{

    /// <inheritdoc/>
    protected override void NativeRefreshIsBluetoothOn()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    protected override void NativeRefreshIsRunning()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    protected override void NativeStart(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    protected override void NativeStop()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    protected override ValueTask NativeInitializeAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override Task NativeSetAdvertisingDataAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }
}
