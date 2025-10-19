namespace Plugin.Bluetooth.Abstractions;

/// <summary>
///     Event arguments for when the Bluetooth state changes.
/// </summary>
public class BluetoothStateChangedEventArgs : EventArgs
{
    public BluetoothStateChangedEventArgs(bool isBluetoothOn)
    {
        IsBluetoothOn = isBluetoothOn;
    }

    /// <summary>
    ///     Gets a value indicating whether Bluetooth is currently enabled.
    /// </summary>
    public bool IsBluetoothOn { get; }
}
