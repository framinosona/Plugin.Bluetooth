using System.Runtime.Versioning;

namespace Plugin.Bluetooth.PlatformSpecific.Permissions;

[SupportedOSPlatform("android31.0")]
public class AndroidPermissionForBluetoothAdvertise() : BaseAndroidPermissionHandler(Android.Manifest.Permission.BluetoothAdvertise, true);
