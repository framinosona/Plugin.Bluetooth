using Android.Bluetooth;

namespace Plugin.Bluetooth.PlatformSpecific;

#pragma warning disable CA1034 // Nested types should not be visible

public partial class BluetoothGattProxy
{
    /// <summary>
    /// Interface for Bluetooth GATT connection options.
    /// </summary>
    public interface IConnectionOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use auto-connect for the GATT connection.
        /// </summary>
        bool UseAutoConnect { get; set; }

        /// <summary>
        /// Gets or sets the preferred Bluetooth PHY for the connection.
        /// </summary>
        Android.Bluetooth.BluetoothPhy? BluetoothPhy { get; set; }

        /// <summary>
        /// Gets or sets the preferred Bluetooth transports for the connection.
        /// </summary>
        BluetoothTransports? BluetoothTransports { get; set; }
    }
}

#pragma warning restore CA1034 // Nested types should not be visible
