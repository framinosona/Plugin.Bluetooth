using System.Diagnostics.CodeAnalysis;

namespace Plugin.Bluetooth.BaseClasses;

/// <inheritdoc cref="IBluetoothDevice" />
public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    protected BaseBluetoothDevice(IBluetoothScanner scanner, string id, Manufacturer manufacturer)
    {
        Scanner = scanner;
        Id = id;
        Manufacturer = manufacturer;
    }

    protected BaseBluetoothDevice(IBluetoothScanner scanner, [NotNull] IBluetoothAdvertisement advertisement) : this(scanner, advertisement.BluetoothAddress, advertisement.Manufacturer)
    {
    }

    public IBluetoothScanner Scanner { get; set; }

    public string Id { get; set; }

    public Manufacturer Manufacturer { get; set; }

    public DateTimeOffset LastSeen { get; set; }

    public int MtuSize { get; set; }

    protected virtual async ValueTask DisposeAsyncCore()
    {

        try
        {
            // Ensure device is disconnected
            await DisconnectIfNeededAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, ex);
        }

        // Clear RSSI history
        _rssiHistory.Clear();

        // Complete any pending tasks
        ConnectionTcs?.TrySetCanceled();
        DisconnectionTcs?.TrySetCanceled();
        ServicesExplorationTcs?.TrySetCanceled();

        // Unsubscribe from events
        Services.CollectionChanged -= ServicesOnCollectionChanged;
        AdvertisementReceived = null;

        Connected = null;
        Disconnected = null;
        Connecting = null;
        Disconnecting = null;
        UnexpectedDisconnection = null;

        ServiceListChanged = null;
        ServicesAdded = null;
        ServicesRemoved = null;

        await ClearServicesAsync().ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}
