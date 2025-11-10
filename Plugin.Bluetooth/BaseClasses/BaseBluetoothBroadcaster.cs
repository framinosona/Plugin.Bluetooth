
namespace Plugin.Bluetooth.BaseClasses;

/// <inheritdoc cref="IBluetoothBroadcaster" />
public abstract partial class BaseBluetoothBroadcaster : BaseBluetoothActivity, IBluetoothBroadcaster
{
    /// <summary>
    /// Initializes the Bluetooth scanner.
    /// </summary>
    protected async virtual ValueTask InitializeAsync()
    {
        await NativeInitializeAsync().ConfigureAwait(false);
    }

    protected override void NativeRefreshAllValues()
    {
        NativeRefreshIsBluetoothOn();
        NativeRefreshIsBroadcasting();
    }

    protected abstract void NativeRefreshIsBroadcasting();

    protected abstract ValueTask NativeInitializeAsync();
    public abstract Task NativeSetAdvertisingDataAsync(IEnumerable<Guid> serviceGuids);
}
