using System.ComponentModel;

namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface for managing Bluetooth operations and state. Base class for both the BluetoothScanner and BluetoothBroadcaster.
/// </summary>
public interface IBluetoothManager : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether Bluetooth is currently enabled on the device.
    /// </summary>
    bool IsBluetoothOn { get; }

    event EventHandler<BluetoothStateChangedEventArgs> BluetoothStateChanged;

    /// <summary>
    /// Gets the Bluetooth scanner instance associated with this manager.
    /// </summary>
    ValueTask<IBluetoothScanner> GetScannerAsync();

    /// <summary>
    /// Gets the Bluetooth broadcaster instance associated with this manager.
    /// </summary>
    ValueTask<IBluetoothBroadcaster> GetBroadcasterAsync();

}
