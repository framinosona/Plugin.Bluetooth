using Plugin.Bluetooth.Abstractions.Enums;
using Plugin.Bluetooth.Abstractions.Exceptions;

using System.ComponentModel;

namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface representing a Bluetooth device, providing properties and methods for interacting with it.
/// </summary>
public partial interface IBluetoothDevice : INotifyPropertyChanged, IAsyncDisposable
{
    /// <summary>
    /// Gets the Bluetooth scanner associated with this device.
    /// </summary>
    IBluetoothScanner Scanner { get; }

    /// <summary>
    /// Returns a short string representation of the device.
    /// </summary>
    /// <returns>A short string representation of the device.</returns>
    string ToShortString();

    /// <summary>
    /// Refreshes the device asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous refresh operation.</returns>
    Task RefreshAsync();

    #region Device

    /// <summary>
    /// Gets the unique identifier of the device.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the manufacturer of the device.
    /// </summary>
    Manufacturer Manufacturer { get; }

    /// <summary>
    /// Gets the last time the device was seen, either by scanning or by connection.
    /// </summary>
    DateTimeOffset LastSeen { get; }

    #endregion

    #region Advertisement

    /// <summary>
    /// Gets the latest advertisement information received.
    /// </summary>
    IBluetoothAdvertisement? LastAdvertisement { get; }

    /// <summary>
    /// Gets the interval between advertisement information.
    /// </summary>
    TimeSpan IntervalBetweenAdvertisement { get; }

    /// <summary>
    /// Occurs when advertisement information is received.
    /// </summary>
    event EventHandler<AdvertisementReceivedEventArgs> AdvertisementReceived;

    /// <summary>
    /// Waits for advertisement information asynchronously.
    /// </summary>
    /// <param name="filter">The filter to apply to the advertisement information.</param>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the advertisement information.</returns>
    Task<IBluetoothAdvertisement> WaitForAdvertisementAsync(Func<IBluetoothAdvertisement, bool>? filter = null, int timeout = -1);

    /// <summary>
    /// Handles the advertisement information received event.
    /// </summary>
    /// <param name="advertisement">The advertisement information received.</param>
    void OnAdvertisementReceived(IBluetoothAdvertisement advertisement);

    #endregion

    #region Connection

    /// <summary>
    /// Gets a value indicating whether the device is connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Waits for the device to be connected or disconnected asynchronously.
    /// </summary>
    /// <param name="isConnected">True to wait for the device to be connected, false to wait for it to be disconnected.</param>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task WaitForIsConnectedAsync(bool isConnected, int timeout = -1);

    #region Connecting

    /// <summary>
    /// Gets a value indicating whether the device is connecting.
    /// </summary>
    bool IsConnecting { get; }

    /// <summary>
    /// Occurs when the device is connected.
    /// </summary>
    event EventHandler Connected;

    /// <summary>
    /// Occurs when the device is connecting.
    /// </summary>
    event EventHandler Connecting;

    /// <summary>
    /// Connects to the device if it is not already connected asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="TimeoutException">Thrown if the connection attempt times out.</exception>
    /// <exception cref="DeviceFailedToConnectException">Thrown if the connection attempt fails.</exception>
    /// <exception cref="NativeBluetoothException">Thrown if the native connection layer fails.</exception>
    Task ConnectIfNeededAsync(int timeout = -1);

    /// <summary>
    /// Connects to the device asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="TimeoutException">Thrown if the connection attempt times out.</exception>
    /// <exception cref="DeviceFailedToConnectException">Thrown if the connection attempt fails.</exception>
    /// <exception cref="DeviceIsAlreadyConnectedException">Thrown if the device is already connecting.</exception>
    /// <exception cref="NativeBluetoothException">Thrown if the native connection layer fails.</exception>
    Task ConnectAsync(int timeout = -1);

    #endregion

    #region Disconnecting

    /// <summary>
    /// Gets a value indicating whether the device is disconnecting.
    /// </summary>
    bool IsDisconnecting { get; }

    /// <summary>
    /// Occurs when the device is disconnected.
    /// </summary>
    event EventHandler Disconnected;

    /// <summary>
    /// Occurs when the device is disconnecting.
    /// </summary>
    event EventHandler Disconnecting;

    /// <summary>
    /// Disconnects from the device if it is not already disconnected asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="TimeoutException">Thrown if the disconnection attempt times out.</exception>
    /// <exception cref="DeviceFailedToConnectException">Thrown if the disconnection attempt fails.</exception>
    /// <exception cref="NativeBluetoothException">Thrown if the native disconnection layer fails.</exception>
    Task DisconnectIfNeededAsync(int timeout = -1);

    /// <summary>
    /// Disconnects from the device asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="TimeoutException">Thrown if the disconnection attempt times out.</exception>
    /// <exception cref="DeviceFailedToDisconnectException">Thrown if the disconnection attempt fails.</exception>
    /// <exception cref="DeviceIsAlreadyDisconnectedException">Thrown if the device is already disconnected.</exception>
    /// <exception cref="NativeBluetoothException">Thrown if the native connection layer fails.</exception>
    Task DisconnectAsync(int timeout = -1);

    #region UnexpectedDisconnection

    /// <summary>
    /// Occurs when an unexpected disconnection happens.
    /// </summary>
    event EventHandler<DeviceUnexpectedDisconnectionEventArgs> UnexpectedDisconnection;

    /// <summary>
    /// Gets or sets a value indicating whether to ignore the next unexpected disconnection.
    /// </summary>
    bool IgnoreNextUnexpectedDisconnection { get; set; }

    #endregion

    #endregion

    #endregion

    #region Identity

    /// <summary>
    /// Gets the advertised name of the device.
    /// </summary>
    string AdvertisedName { get; }

    /// <summary>
    /// Gets the name of the device, using the advertised name first, if empty it will use the cached name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the cached name of the device as seen by the native platform.
    /// </summary>
    string CachedName { get; }

    /// <summary>
    /// Gets the debug name of the device.
    /// </summary>
    string DebugName { get; }

    /// <summary>
    /// Waits for the name of the device to change asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task WaitForNameToChangeAsync(int timeout = -1);

    #endregion

    #region Services - Exploration

    /// <summary>
    /// Gets a value indicating whether the device is exploring services.
    /// </summary>
    bool IsExploringServices { get; }

    /// <summary>
    /// Occurs when the service list changes.
    /// </summary>
    event EventHandler<ServiceListChangedEventArgs> ServiceListChanged;


    /// <summary>
    /// Explores the services of the device asynchronously.
    /// </summary>
    /// <param name="clearBeforeExploring">True to clear the services before exploring.</param>
    /// <param name="exploreCharacteristicsToo">True to explore characteristics as well.</param>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ExploreServicesAsync(bool clearBeforeExploring = false, bool exploreCharacteristicsToo = false, int timeout = -1);

    #endregion

    #region Services

    /// <summary>
    /// Resets the list of services and characteristics, and stops all subscriptions and notifications.
    /// </summary>
    Task ClearServicesAsync();


    /// <summary>
    /// Checks if the device has a service with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the service to check for.</param>
    /// <returns>True if the device has the service, false otherwise.</returns>
    bool HasService(Guid id);

    /// <summary>
    /// Checks if the device has a service with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the service to check for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the device has the service.</returns>
    Task<bool> HasServiceAsync(Guid id);

    /// <summary>
    /// Gets the service that matches the specified filter asynchronously.
    /// </summary>
    /// <param name="filter">The filter to apply to the services.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service that matches the filter, or null if no such service exists.</returns>
    Task<IBluetoothService?> GetServiceOrDefaultAsync(Func<IBluetoothService, bool> filter);

    /// <summary>
    /// Gets the service that matches the specified filter.
    /// </summary>
    /// <param name="filter">The filter to apply to the services.</param>
    /// <returns>The service that matches the filter, or null if no such service exists.</returns>
    IBluetoothService? GetServiceOrDefault(Func<IBluetoothService, bool> filter);

    /// <summary>
    /// Gets the service with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the service to get.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the service with the specified ID, or null if no such service exists.</returns>
    Task<IBluetoothService?> GetServiceOrDefaultAsync(Guid id);

    /// <summary>
    /// Gets the service with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the service to get.</param>
    /// <returns>The service with the specified ID, or null if no such service exists.</returns>
    IBluetoothService? GetServiceOrDefault(Guid id);

    /// <summary>
    /// Gets the services that match the specified filter.
    /// </summary>
    /// <param name="filter">The filter to apply to the services.</param>
    /// <returns>The services that match the filter, or all services if the filter is null.</returns>
    IEnumerable<IBluetoothService> GetServices(Func<IBluetoothService, bool>? filter = null);

    /// <summary>
    /// Gets the services that match the specified filter asynchronously.
    /// </summary>
    /// <param name="filter">The filter to apply to the services.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the services that match the filter, or all services if the filter is null.</returns>
    Task<IEnumerable<IBluetoothService>> GetServicesAsync(Func<IBluetoothService, bool>? filter = null);

    /// <summary>
    /// Gets the services with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the services to get.</param>
    /// <returns>The services with the specified ID.</returns>
    IEnumerable<IBluetoothService> GetServices(Guid id);

    /// <summary>
    /// Gets the services with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the services to get.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the services with the specified ID.</returns>
    Task<IEnumerable<IBluetoothService>> GetServicesAsync(Guid id);

    #endregion

    #region SignalStrength

    /// <summary>
    /// Gets the signal strength in dBm.
    /// </summary>
    int SignalStrengthDbm { get; }

    /// <summary>
    /// Gets the signal strength as a percentage (between 0.00 and 1.00).
    /// </summary>
    double SignalStrengthPercent { get; }

    /// <summary>
    /// Gets a value indicating whether the device is reading signal strength.
    /// </summary>
    bool IsReadingSignalStrength { get; }

    /// <summary>
    /// Reads the signal strength asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ReadSignalStrengthAsync(int timeout = -1);

    /// <summary>
    /// Tries to read the signal strength asynchronously.
    /// </summary>
    /// <param name="timeout">The timeout for the operation, in milliseconds.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task TryReadSignalStrengthAsync(int timeout = -1);

    #endregion

    #region Version

    /// <summary>
    /// Gets the firmware version of the device.
    /// </summary>
    Version? FirmwareVersion { get; }

    /// <summary>
    /// Reads the firmware version asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the firmware version.</returns>
    Task<Version> ReadFirmwareVersionAsync();

    /// <summary>
    /// Gets the software version of the device.
    /// </summary>
    Version? SoftwareVersion { get; }

    /// <summary>
    /// Reads the software version asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the software version.</returns>
    Task<Version> ReadSoftwareVersionAsync();

    /// <summary>
    /// Gets the hardware version of the device.
    /// </summary>
    string? HardwareVersion { get; }

    /// <summary>
    /// Reads the hardware version asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hardware version.</returns>
    Task<string> ReadHardwareVersionAsync();

    /// <summary>
    /// Reads all versions (firmware, software, and hardware) asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ReadVersionsAsync();

    #endregion

    #region BatteryLevel

    /// <summary>
    /// Gets the battery level as a percentage.
    /// </summary>
    double? BatteryLevelPercent { get; }

    /// <summary>
    /// Reads the battery level asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the battery level as a percentage.</returns>
    Task<double?> ReadBatteryLevelAsync();

    #endregion

    #region MtuSize

    /// <summary>
    /// Gets the current MTU size of the device.
    /// </summary>
    int MtuSize { get; }

    #endregion
}
