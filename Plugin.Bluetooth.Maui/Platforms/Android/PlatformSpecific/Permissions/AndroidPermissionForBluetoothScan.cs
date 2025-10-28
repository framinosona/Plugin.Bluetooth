using System.Runtime.Versioning;

namespace Plugin.Bluetooth.PlatformSpecific.Permissions;

[SupportedOSPlatform("android31.0")]
public class AndroidPermissionForBluetoothScan() : BaseAndroidPermissionHandler(Android.Manifest.Permission.BluetoothScan, true);
