namespace Plugin.Bluetooth.Abstractions;

/// <summary>
///     Event arguments for when the service list changes during device exploration.
/// </summary>
public class ServiceListChangedEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceListChangedEventArgs"/> class.
    /// </summary>
    /// <param name="addedServices">The services that were added.</param>
    /// <param name="removedServices">The services that were removed.</param>
    public ServiceListChangedEventArgs(
        IEnumerable<IBluetoothService> addedServices,
        IEnumerable<IBluetoothService> removedServices)
    {
        ArgumentNullException.ThrowIfNull(addedServices);
        ArgumentNullException.ThrowIfNull(removedServices);

        AddedServices = addedServices;
        RemovedServices = removedServices;
    }

    /// <summary>
    ///     Gets the services that were added.
    /// </summary>
    public IEnumerable<IBluetoothService> AddedServices { get; }

    /// <summary>
    ///     Gets the services that were removed.
    /// </summary>
    public IEnumerable<IBluetoothService> RemovedServices { get; }
}
