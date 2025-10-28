namespace Plugin.Bluetooth.PlatformSpecific.Permissions;

public class AndroidPermissionForBluetoothAdmin() : BaseAndroidPermissionHandler(Android.Manifest.Permission.BluetoothAdmin, false);
