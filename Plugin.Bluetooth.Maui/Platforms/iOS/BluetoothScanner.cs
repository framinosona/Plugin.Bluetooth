using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothScanner : BaseBluetoothScanner, CbCentralManagerProxy.ICbCentralManagerProxyDelegate
{

    #region CbCentralManagerProxy

    public void DiscoveredPeripheral(CBPeripheral peripheral, NSDictionary advertisementData, NSNumber rssi)
    {
        throw new NotImplementedException();
    }

    public void UpdatedState()
    {
        throw new NotImplementedException();
    }

    public void WillRestoreState(NSDictionary dict)
    {
        throw new NotImplementedException();
    }

    public CbCentralManagerProxy.ICbPeripheralDelegate GetDevice(CBPeripheral peripheral)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region BaseBluetoothScanner

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

    protected async override ValueTask NativeInitializeAsync(Dictionary<string, object>? options = null)
    {
        throw new NotImplementedException();
    }

    protected override IBluetoothDevice NativeCreateDevice(IBluetoothAdvertisement advertisement)
    {
        throw new NotImplementedException();
    }

    #endregion

}
