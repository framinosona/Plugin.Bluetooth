using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

/// <inheritdoc />
public partial class BluetoothBroadcaster : BaseBluetoothBroadcaster, BluetoothLeAdvertisementPublisherProxy.IBluetoothLeAdvertisementPublisherProxyDelegate
{

    public BluetoothLeAdvertisementPublisherProxy? BluetoothLeAdvertisementPublisherProxy { get; protected set; }

    #region BaseBluetoothBroadcaster


    protected override void NativeRefreshIsBluetoothOn()
    {
        throw new NotImplementedException();
    }

    protected override void NativeRefreshIsRunning()
    {
        throw new NotImplementedException();
    }

    protected override void NativeStart(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    protected override void NativeStop()
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeInitializeAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    public async override Task NativeSetAdvertisingDataAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region BluetoothLeAdvertisementPublisherProxy.IBluetoothLeAdvertisementPublisherProxyDelegate

    public void OnAdvertisementPublisherStatusChanged(BluetoothLEAdvertisementPublisherStatus status)
    {
        throw new NotImplementedException();
    }

    #endregion

}
