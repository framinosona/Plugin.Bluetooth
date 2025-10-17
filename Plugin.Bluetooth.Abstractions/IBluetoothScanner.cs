namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface for managing and scanning Bluetooth devices, extending <see cref="IBluetoothManager" />.
/// </summary>
public partial interface IBluetoothScanner : IBluetoothManager
{
    /// <summary>
    /// Gets the repository for known Bluetooth services and characteristics.
    /// </summary>
    IBluetoothCharacteristicAccessServicesRepository KnownServicesAndCharacteristicsRepository { get; }

    #region Advertisement

    /// <summary>
    /// Event triggered when a Bluetooth advertisement is received.
    /// </summary>
    event EventHandler<AdvertisementReceivedEventArgs> AdvertisementReceived;

    /// <summary>
    /// Advertisement filter. If set, only advertisements that pass the filter will be processed.
    /// </summary>
    Func<IBluetoothAdvertisement, bool>? AdvertisementFilter { get; set; }

    /// <summary>
    /// Resets the advertisement filter.
    /// </summary>
    void ResetAdvertisementFilter();

    #endregion

    #region Clean

    /// <summary>
    /// Cleans resources associated with a list of Bluetooth devices.
    /// </summary>
    /// <param name="devices">The devices to clean.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CleanAsync(IEnumerable<IBluetoothDevice> devices);

    /// <summary>
    /// Cleans resources associated with a specific Bluetooth device.
    /// </summary>
    /// <param name="device">The device to clean.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CleanAsync(IBluetoothDevice? device);

    /// <summary>
    /// Cleans resources associated with a Bluetooth device by its ID.
    /// </summary>
    /// <param name="deviceId">The ID of the device to clean.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CleanAsync(string deviceId);

    /// <summary>
    /// Cleans all resources associated with Bluetooth devices. This includes all devices, services, and characteristics.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CleanAsync();

    #endregion

    #region DeviceList

    /// <summary>
    /// Event triggered when the list of available devices changes.
    /// </summary>
    event EventHandler<DeviceListChangedEventArgs> DeviceListChanged;

    /// <summary>
    /// Waits for a Bluetooth device with the specified ID to appear or returns it if already available.
    /// </summary>
    /// <param name="id">The ID of the device to wait for.</param>
    /// <param name="timeout">Optional timeout in milliseconds. Defaults to -1 for no timeout.</param>
    /// <returns>The <see cref="IBluetoothDevice" /> when it appears.</returns>
    Task<IBluetoothDevice> WaitForDeviceToAppearAsync(string id, int timeout = -1);

    /// <summary>
    /// Waits for the first Bluetooth device that matches the specified filter to appear.
    /// </summary>
    /// <param name="filter">A function to filter devices. Should return true for matching devices.</param>
    /// <param name="timeout">Optional timeout in milliseconds. Defaults to -1 for no timeout.</param>
    /// <returns>The <see cref="IBluetoothDevice" /> that matches the filter when it appears.</returns>
    Task<IBluetoothDevice> WaitForDeviceToAppearAsync(Func<IBluetoothDevice, bool> filter, int timeout = -1);

    #endregion

    #region GetDevice

    /// <summary>
    /// Returns the closest Bluetooth device currently available.
    /// </summary>
    /// <returns>The closest <see cref="IBluetoothDevice" />, or null if none are found.</returns>
    IBluetoothDevice? GetClosestDeviceOrDefault();

    /// <summary>
    /// Returns the first Bluetooth device that matches the specified filter.
    /// </summary>
    /// <param name="filter">A function to filter devices. Should return true for matching devices.</param>
    /// <returns>The matching <see cref="IBluetoothDevice" />, or null if none are found.</returns>
    IBluetoothDevice? GetDeviceOrDefault(Func<IBluetoothDevice, bool> filter);

    /// <summary>
    /// Returns a Bluetooth device with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the device to retrieve.</param>
    /// <returns>The matching <see cref="IBluetoothDevice" />, or null if none are found.</returns>
    IBluetoothDevice? GetDeviceOrDefault(string id);

    /// <summary>
    /// Returns all Bluetooth devices that match the specified filter.
    /// </summary>
    /// <param name="filter">An optional function to filter devices. Defaults to null for all devices.</param>
    /// <returns>A collection of <see cref="IBluetoothDevice" /> that match the filter.</returns>
    IEnumerable<IBluetoothDevice> GetDevices(Func<IBluetoothDevice, bool>? filter = null);

    #endregion
}
