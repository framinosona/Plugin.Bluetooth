namespace Plugin.Bluetooth.Maui.PlatformSpecific;

#pragma warning disable CA1034 // Nested types should not be visible

public partial class BluetoothGattProxy
{
    /// <summary>
    /// Default implementation of <see cref="IConnectionOptions"/> with standard settings.
    /// </summary>
    public record DefaultConnectionOptions : IConnectionOptions
    {
        /// <inheritdoc/>
        public bool UseAutoConnect { get; set; }

        /// <inheritdoc/>
        public Android.Bluetooth.BluetoothPhy? BluetoothPhy { get; set; }

        /// <inheritdoc/>
        public BluetoothTransports? BluetoothTransports { get; set; }
    }
}

#pragma warning restore CA1034 // Nested types should not be visible
