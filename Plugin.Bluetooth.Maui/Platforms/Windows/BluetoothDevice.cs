namespace Plugin.Bluetooth.Maui;

public partial class BluetoothDevice : BaseBluetoothDevice
{

    public BluetoothDevice(IBluetoothScanner scanner, string id, Manufacturer manufacturer) : base(scanner, id, manufacturer)
    {
    }

    public BluetoothDevice(IBluetoothScanner scanner, IBluetoothAdvertisement advertisement) : base(scanner, advertisement)
    {
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
