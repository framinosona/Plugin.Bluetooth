
namespace Plugin.Bluetooth.PlatformSpecific.Permissions;

/// <summary>
/// iOS permission for Bluetooth Peripheral usage.
/// </summary>
public class IosPermissionForBluetoothPeripheral() : BaseIosPermissionHandler("NSBluetoothPeripheralUsageDescription");
