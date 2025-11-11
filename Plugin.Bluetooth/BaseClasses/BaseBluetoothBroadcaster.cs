
namespace Plugin.Bluetooth.BaseClasses;

/// <inheritdoc cref="IBluetoothBroadcaster" />
public abstract partial class BaseBluetoothBroadcaster : BaseBluetoothActivity, IBluetoothBroadcaster
{
    /// <summary>
    /// Platform-specific implementation to set the advertising data for the broadcaster.
    /// </summary>
    /// <param name="nativeOptions">Platform-specific options for setting advertising data.</param>
    /// <returns>A task that completes when the advertising data has been set.</returns>
    public abstract Task NativeSetAdvertisingDataAsync(Dictionary<string, object>? nativeOptions = null);
}
