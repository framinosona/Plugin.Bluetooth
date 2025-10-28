using System.ComponentModel;

namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface representing a Bluetooth service, providing properties and methods for interacting with it.
/// </summary>
public partial interface IBluetoothService : INotifyPropertyChanged, IAsyncDisposable
{
    /// <summary>
    /// Gets the Bluetooth device associated with this service.
    /// </summary>
    IBluetoothDevice Device { get; }

    #region Characteristics - Exploration

    /// <summary>
    /// Gets a value indicating whether the device is exploring services.
    /// </summary>
    bool IsExploringCharacteristics { get; }

    /// <summary>
    /// Occurs when the service list changes.
    /// </summary>
    event EventHandler<CharacteristicListChangedEventArgs>? CharacteristicListChanged;

    /// <summary>
    /// Explores the characteristics of the service asynchronously.
    /// </summary>
    /// <param name="clearBeforeExploring">True to clear the characteristics before exploring.</param>
    /// <param name="timeout">The timeout for the operation.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ExploreCharacteristicsAsync(bool clearBeforeExploring = false, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    #endregion

    #region Characteristics - Clear

    /// <summary>
    /// Clears the characteristics of the service.
    /// </summary>
    ValueTask ClearCharacteristicsAsync();

    #endregion

    /// <summary>
    /// Returns the name of the service without the ID/GUID, useful for shortened logging.
    /// </summary>
    /// <returns>The short string representation of the service.</returns>
    string ToShortString();

    #region Service

    /// <summary>
    /// Gets the unique identifier of the service.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name of the service.
    /// </summary>
    string Name { get; }

    #endregion

    #region Characteristics - HasCharacteristic

    /// <summary>
    /// Checks if the service has a characteristic with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the characteristic to check for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the service has the characteristic.</returns>
    ValueTask<bool> HasCharacteristicAsync(Guid id);

    /// <summary>
    /// Checks if the service has a characteristic with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the characteristic to check for.</param>
    /// <returns>True if the service has the characteristic, false otherwise.</returns>
    bool HasCharacteristic(Guid id);

    #endregion

    #region Characteristics - GetCharacteristic

    /// <summary>
    /// Gets the characteristic with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the characteristic to get.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the characteristic with the specified ID, or null if no such characteristic exists.</returns>
    ValueTask<IBluetoothCharacteristic?> GetCharacteristicOrDefaultAsync(Guid id);

    /// <summary>
    /// Gets the characteristic with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the characteristic to get.</param>
    /// <returns>The characteristic with the specified ID, or null if no such characteristic exists.</returns>
    IBluetoothCharacteristic? GetCharacteristicOrDefault(Guid id);

    /// <summary>
    /// Gets the characteristic that matches the specified filter asynchronously.
    /// </summary>
    /// <param name="filter">The filter to apply to the characteristics.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the characteristic that matches the filter, or null if no such characteristic exists.</returns>
    ValueTask<IBluetoothCharacteristic?> GetCharacteristicOrDefaultAsync(Func<IBluetoothCharacteristic, bool> filter);

    /// <summary>
    /// Gets the characteristic that matches the specified filter.
    /// </summary>
    /// <param name="filter">The filter to apply to the characteristics.</param>
    /// <returns>The characteristic that matches the filter, or null if no such characteristic exists.</returns>
    IBluetoothCharacteristic? GetCharacteristicOrDefault(Func<IBluetoothCharacteristic, bool> filter);

    /// <summary>
    /// Gets the characteristics that match the specified filter asynchronously.
    /// </summary>
    /// <param name="filter">The filter to apply to the characteristics. If null, returns all characteristics.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the characteristics that match the filter, or all characteristics if the filter is null.</returns>
    ValueTask<IEnumerable<IBluetoothCharacteristic>> GetCharacteristicsAsync(Func<IBluetoothCharacteristic, bool>? filter = null);

    /// <summary>
    /// Gets the characteristics that match the specified filter.
    /// </summary>
    /// <param name="filter">The filter to apply to the characteristics. If null, returns all characteristics.</param>
    /// <returns>The characteristics that match the filter, or all characteristics if the filter is null.</returns>
    IEnumerable<IBluetoothCharacteristic> GetCharacteristics(Func<IBluetoothCharacteristic, bool>? filter = null);

    #endregion
}
