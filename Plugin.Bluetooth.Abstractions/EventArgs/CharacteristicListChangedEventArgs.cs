namespace Plugin.Bluetooth.Abstractions;

/// <summary>
///     Event arguments for when the characteristic list changes during service exploration.
/// </summary>
public class CharacteristicListChangedEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CharacteristicListChangedEventArgs"/> class.
    /// </summary>
    /// <param name="addedCharacteristics">The characteristics that were added.</param>
    /// <param name="removedCharacteristics">The characteristics that were removed.</param>
    public CharacteristicListChangedEventArgs(
        IEnumerable<IBluetoothCharacteristic> addedCharacteristics,
        IEnumerable<IBluetoothCharacteristic> removedCharacteristics)
    {
        ArgumentNullException.ThrowIfNull(addedCharacteristics);
        ArgumentNullException.ThrowIfNull(removedCharacteristics);

        AddedCharacteristics = addedCharacteristics;
        RemovedCharacteristics = removedCharacteristics;
    }

    /// <summary>
    ///     Gets the characteristics that were added.
    /// </summary>
    public IEnumerable<IBluetoothCharacteristic> AddedCharacteristics { get; }

    /// <summary>
    ///     Gets the characteristics that were removed.
    /// </summary>
    public IEnumerable<IBluetoothCharacteristic> RemovedCharacteristics { get; }
}
