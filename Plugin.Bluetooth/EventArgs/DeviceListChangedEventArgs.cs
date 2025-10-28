namespace Plugin.Bluetooth.Abstractions;

/// <summary>
///     Event arguments for when the device list changes during Bluetooth scanning.
/// </summary>
public class DeviceListChangedEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeviceListChangedEventArgs"/> class.
    /// </summary>
    /// <param name="addedDevices">The devices that were added.</param>
    /// <param name="removedDevices">The devices that were removed.</param>
    public DeviceListChangedEventArgs(
        IEnumerable<IBluetoothDevice> addedDevices,
        IEnumerable<IBluetoothDevice> removedDevices)
    {
        ArgumentNullException.ThrowIfNull(addedDevices);
        ArgumentNullException.ThrowIfNull(removedDevices);

        AddedDevices = addedDevices;
        RemovedDevices = removedDevices;
    }

    /// <summary>
    ///     Gets the devices that were added.
    /// </summary>
    public IEnumerable<IBluetoothDevice> AddedDevices { get; }

    /// <summary>
    ///     Gets the devices that were removed.
    /// </summary>
    public IEnumerable<IBluetoothDevice> RemovedDevices { get; }
}
