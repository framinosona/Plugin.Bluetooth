using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

/// <inheritdoc  />
public partial class BluetoothScanner : BaseBluetoothScanner, BluetoothLeAdvertisementWatcherProxy.IBluetoothLeAdvertisementWatcherProxyDelegate
{
    public BluetoothLeAdvertisementWatcherProxy? BluetoothLeAdvertisementWatcherProxy { get; protected set; }

    #region BaseBluetoothScanner
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

    protected override IBluetoothDevice NativeCreateDevice(IBluetoothAdvertisement advertisement)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region BluetoothLeAdvertisementWatcherProxy.IBluetoothLeAdvertisementWatcherProxyDelegate

    public void OnAdvertisementWatcherStopped(BluetoothError argsError)
    {
        throw new NotImplementedException();
    }

    public void OnAdvertisementReceived(BluetoothLEAdvertisementReceivedEventArgs argsAdvertisement)
    {
        throw new NotImplementedException();
    }

    #endregion
}
