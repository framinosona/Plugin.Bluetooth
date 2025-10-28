using System;

using CoreBluetooth;
using Foundation;

namespace Plugin.Bluetooth.PlatformSpecific;

// Mapping native APIs leads to unclean interfaces, ignoring warnings here
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
#pragma warning disable CA1716 // Identifiers should not match keywords

public partial class CbCentralManagerProxy
{
    /// <summary>
    /// Delegate interface for CoreBluetooth central manager callbacks, extending the base Bluetooth scanner interface.
    /// </summary>
    public interface ICbCentralManagerProxyDelegate : Abstractions.IBluetoothScanner
    {
        /// <summary>
        /// Called when a peripheral is discovered during scanning.
        /// </summary>
        /// <param name="peripheral">The discovered peripheral.</param>
        /// <param name="advertisementData">The advertisement data from the peripheral.</param>
        /// <param name="rssi">The received signal strength indicator (RSSI) value.</param>
        void DiscoveredPeripheral(CBPeripheral peripheral, NSDictionary advertisementData, NSNumber rssi);

        /// <summary>
        /// Called when the central manager's state is updated.
        /// </summary>
        void UpdatedState();

        /// <summary>
        /// Called when the central manager will restore state from a previous session.
        /// </summary>
        /// <param name="dict">The dictionary containing the state information to restore.</param>
        void WillRestoreState(NSDictionary dict);

        /// <summary>
        /// Gets the device delegate for the specified CoreBluetooth peripheral.
        /// </summary>
        /// <param name="peripheral">The CoreBluetooth peripheral to get the device delegate for.</param>
        /// <returns>The device delegate for the specified peripheral.</returns>
        CbCentralManagerProxy.ICbPeripheralDelegate GetDevice(CBPeripheral peripheral);
    }
}

#pragma warning restore CA1034 // Nested types should not be visible
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
#pragma warning restore CA1716 // Identifiers should not match keywords
