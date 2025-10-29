using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth.PlatformSpecific;

#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix

public partial class BluetoothAdapterProxy
{
    /// <summary>
    /// Delegate interface for handling Bluetooth adapter operations and events.
    /// Extends the base Bluetooth manager interface to provide Windows-specific adapter functionality.
    /// </summary>
    public interface IBluetoothAdapterProxyDelegate : IBluetoothManager
    {
    }
}

#pragma warning restore CA1034 // Nested types should not be visible
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
