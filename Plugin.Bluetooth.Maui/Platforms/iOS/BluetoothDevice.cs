using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothDevice : BaseBluetoothDevice, CbPeripheralProxy.ICbPeripheralProxyDelegate, CbCentralManagerProxy.ICbPeripheralDelegate
{
    public CbPeripheralProxy CbPeripheralDelegateProxy { get; }

    public BluetoothDevice(IBluetoothScanner scanner, string id, Manufacturer manufacturer, CbPeripheralProxy cbPeripheralDelegateProxy) : base(scanner, id, manufacturer)
    {
        CbPeripheralDelegateProxy = cbPeripheralDelegateProxy;
    }

    public BluetoothDevice(IBluetoothScanner scanner, BluetoothAdvertisement advertisement) : base(scanner, advertisement)
    {
        CbPeripheralDelegateProxy = new CbPeripheralProxy(this, advertisement.Peripheral);
    }

    #region BaseBluetoothDevice

    protected async override ValueTask NativeServicesExplorationAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override void NativeRefreshIsConnected()
    {
        throw new NotImplementedException();
    }

    protected override void NativeConnect(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override void NativeDisconnect(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override void NativeReadSignalStrength()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region CbPeripheralProxy.ICbPeripheralProxyDelegate

    public void DiscoveredService(NSError? error)
    {
        throw new NotImplementedException();
    }

    public void RssiRead(NSError? error, NSNumber rssi)
    {
        throw new NotImplementedException();
    }

    public void RssiUpdated(NSError? error)
    {
        throw new NotImplementedException();
    }

    public void UpdatedName()
    {
        throw new NotImplementedException();
    }

    public void DidOpenL2CapChannel(NSError? error, CBL2CapChannel? channel)
    {
        throw new NotImplementedException();
    }

    public void IsReadyToSendWriteWithoutResponse()
    {
        throw new NotImplementedException();
    }

    public void ModifiedServices(CBService[] services)
    {
        throw new NotImplementedException();
    }

    public CbPeripheralProxy.ICbServiceDelegate GetService(CBService? characteristicService)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region CbCentralManagerProxy.ICbPeripheralDelegate

    public void FailedToConnectPeripheral(NSError? error)
    {
        throw new NotImplementedException();
    }

    public void DisconnectedPeripheral(NSError? error)
    {
        throw new NotImplementedException();
    }

    public void ConnectedPeripheral()
    {
        throw new NotImplementedException();
    }

    public void ConnectionEventDidOccur(CBConnectionEvent connectionEvent)
    {
        throw new NotImplementedException();
    }

    public void DidUpdateAncsAuthorization()
    {
        throw new NotImplementedException();
    }

    public void DidDisconnectPeripheral(double timestamp, bool isReconnecting, NSError? error)
    {
        throw new NotImplementedException();
    }

    #endregion

}
