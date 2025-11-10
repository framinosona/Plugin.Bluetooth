using Plugin.Bluetooth.Maui.PlatformSpecific;
using Plugin.Bluetooth.Maui.PlatformSpecific.BroadcastReceivers;

namespace Plugin.Bluetooth.Maui;

public class BluetoothDevice : BaseBluetoothDevice
{
    public Android.Bluetooth.BluetoothDevice NativeDevice { get; }

    public BluetoothGattProxy? BluetoothGattProxy { get; protected set; }

    public BluetoothDevice(IBluetoothScanner scanner, string id, Manufacturer manufacturer) : base(scanner, id, manufacturer)
    {
        // TODO : NativeDevice = advertisement.BluetoothDevice;
        BluetoothEventReceiverProxy.BluetoothDeviceEventReceiver.BondStateChanged += OnBondStateChangedEventReceived;

    }

    public BluetoothDevice(IBluetoothScanner scanner, BluetoothAdvertisement advertisement) : base(scanner, advertisement)
    {
        NativeDevice = advertisement.BluetoothDevice;
        BluetoothEventReceiverProxy.BluetoothDeviceEventReceiver.BondStateChanged += OnBondStateChangedEventReceived;
    }

    protected override ValueTask DisposeAsyncCore()
    {
        BluetoothEventReceiverProxy.BluetoothDeviceEventReceiver.BondStateChanged -= OnBondStateChangedEventReceived;
        return base.DisposeAsyncCore();
    }

    #region BluetoothEventReceiverProxy

    private void OnBondStateChangedEventReceived(object? sender, BluetoothDeviceEventReceiver.BondStateChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion

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
