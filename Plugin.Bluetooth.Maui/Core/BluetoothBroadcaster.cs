namespace Plugin.Bluetooth.Maui;

/// <inheritdoc />
public partial class BluetoothBroadcaster : BaseBluetoothBroadcaster
{

    #region BaseBluetoothBroadcaster

    protected override void NativeRefreshIsBluetoothOn()
    {
        throw new NotImplementedException();
    }

    protected override void NativeRefreshIsRunning()
    {
        throw new NotImplementedException();
    }

    protected override void NativeStart(Dictionary<string, object>? nativeOptions)
    {
        throw new NotImplementedException();
    }

    protected override void NativeStop()
    {
        throw new NotImplementedException();
    }

    protected override void NativeRefreshIsBroadcasting()
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeInitializeAsync()
    {
        throw new NotImplementedException();
    }

    public async override Task NativeSetAdvertisingDataAsync(IEnumerable<Guid> serviceGuids)
    {
        throw new NotImplementedException();
    }


    #endregion
}
