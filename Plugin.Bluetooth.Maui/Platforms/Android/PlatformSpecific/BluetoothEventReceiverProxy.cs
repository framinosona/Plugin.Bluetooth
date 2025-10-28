using Plugin.Bluetooth.PlatformSpecific.BroadcastReceivers;

namespace Plugin.Bluetooth.PlatformSpecific;

/// <summary>
/// Provides lazy-initialized access to Bluetooth event receivers.
/// </summary>
public static class BluetoothEventReceiverProxy
{
    private static readonly Lazy<BluetoothAdapterEventsReceiver> _lazyBluetoothAdapterEventsReceiver =
        new(() => new BluetoothAdapterEventsReceiver());

    /// <summary>
    /// Gets the Bluetooth adapter events receiver instance.
    /// </summary>
    public static BluetoothAdapterEventsReceiver BluetoothAdapterEventsReceiver =>
        _lazyBluetoothAdapterEventsReceiver.Value;

    /// <summary>
    /// Gets a value indicating whether the Bluetooth adapter events receiver has been initialized.
    /// </summary>
    public static bool IsBluetoothAdapterEventsReceiverInitialized =>
        _lazyBluetoothAdapterEventsReceiver.IsValueCreated;

    private static readonly Lazy<BluetoothDeviceEventReceiver> _lazyBluetoothDeviceEventReceiver =
        new(() => new BluetoothDeviceEventReceiver());

    /// <summary>
    /// Gets the Bluetooth device event receiver instance.
    /// </summary>
    public static BluetoothDeviceEventReceiver BluetoothDeviceEventReceiver =>
        _lazyBluetoothDeviceEventReceiver.Value;

    /// <summary>
    /// Gets a value indicating whether the Bluetooth device event receiver has been initialized.
    /// </summary>
    public static bool IsBluetoothDeviceEventReceiverInitialized =>
        _lazyBluetoothDeviceEventReceiver.IsValueCreated;
}
