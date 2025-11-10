using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public class BluetoothBroadcaster : BaseBluetoothBroadcaster, AdvertiseCallbackProxy.IBroadcaster
{
    public BluetoothGattServerCallbackProxy? BluetoothGattServerCallbackProxy { get; protected set; }

    public AdvertiseCallbackProxy AdvertiseCallbackProxy { get; }

    public BluetoothBroadcaster()
    {
        AdvertiseCallbackProxy = new AdvertiseCallbackProxy(this);
    }

    #region AdvertiseCallbackProxy.IBroadcaster

    public void OnStartSuccess(AdvertiseSettings? settingsInEffect)
    {
        throw new NotImplementedException();
    }

    public void OnStartFailure(AdvertiseFailure errorCode)
    {
        throw new NotImplementedException();
    }

    #endregion

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
