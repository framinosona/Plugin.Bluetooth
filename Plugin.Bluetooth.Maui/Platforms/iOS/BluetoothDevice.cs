using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothDevice : BaseBluetoothDevice
{
    public CbPeripheralProxy CbPeripheralDelegateProxy { get; }
    public BluetoothDevice(IBluetoothScanner scanner, string id, Manufacturer manufacturer, CbPeripheralProxy cbPeripheralDelegateProxy) : base(scanner, id, manufacturer)
    {
        CbPeripheralDelegateProxy = cbPeripheralDelegateProxy;
    }

    public BluetoothDevice(IBluetoothScanner scanner, IBluetoothAdvertisement advertisement, CbPeripheralProxy cbPeripheralDelegateProxy) : base(scanner, advertisement)
    {
        CbPeripheralDelegateProxy = cbPeripheralDelegateProxy;
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
}
