namespace Plugin.Bluetooth.Abstractions;

/// <summary>
///     Event arguments for when a characteristic value is updated.
/// </summary>
public class ValueUpdatedEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ValueUpdatedEventArgs"/> class.
    /// </summary>
    /// <param name="newValue">The new value of the characteristic.</param>
    /// <param name="oldValue">The old value of the characteristic.</param>
    public ValueUpdatedEventArgs(ReadOnlySpan<byte> newValue, ReadOnlySpan<byte> oldValue)
    {
        NewValue = new ReadOnlyMemory<byte>([.. newValue]);
        OldValue = new ReadOnlyMemory<byte>([.. oldValue]);
    }

    /// <summary>
    ///     Gets the new value as a read-only memory segment.
    /// </summary>
    public ReadOnlyMemory<byte> NewValue { get; }

    /// <summary>
    ///     Gets the old value as a read-only memory segment.
    /// </summary>
    public ReadOnlyMemory<byte> OldValue { get; }

}
