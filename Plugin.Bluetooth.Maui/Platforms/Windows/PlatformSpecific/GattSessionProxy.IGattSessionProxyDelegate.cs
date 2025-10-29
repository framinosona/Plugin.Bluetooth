using Plugin.Bluetooth.Abstractions;

using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Plugin.Bluetooth.PlatformSpecific;

#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix

public partial class GattSessionProxy
{
    /// <summary>
    /// Delegate interface for handling GATT session operations and events.
    /// Extends the base Bluetooth device interface with Windows-specific GATT session callbacks.
    /// </summary>
    public interface IGattSessionProxyDelegate : IBluetoothDevice
    {
        /// <summary>
        /// Called when the GATT session status changes.
        /// </summary>
        /// <param name="argsStatus">The new GATT session status.</param>
        void OnGattSessionStatusChanged(GattSessionStatus argsStatus);

        /// <summary>
        /// Called when the maximum PDU (Protocol Data Unit) size changes.
        /// </summary>
        void OnMaxPduSizeChanged();
    }
}

#pragma warning restore CA1034 // Nested types should not be visible
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
