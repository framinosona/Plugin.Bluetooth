namespace Plugin.Bluetooth.Abstractions;
/// <summary>
/// Interface for managing Bluetooth broadcasting operations, extending <see cref="IBluetoothManager" />.
/// </summary>
public interface IBluetoothBroadcaster : IBluetoothManager
{
    /// <summary>
    /// Sets the advertising data asynchronously.
    /// </summary>
    /// <param name="serviceGuids">The service GUIDs to include in the advertising data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// - Android: AdvertiseData.Builder.AddServiceUuid
    /// - iOS: CoreBluetooth.StartAdvertisingOptions.ServicesUUID
    /// - Windows: BluetoothLEAdvertisementData.ServiceUuids
    /// </remarks>
    Task SetAdvertisingDataAsync(IEnumerable<Guid> serviceGuids);
}
