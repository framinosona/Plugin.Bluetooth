namespace Plugin.Bluetooth.PlatformSpecific.Permissions;

public class AndroidPermissionForBluetooth() : BaseAndroidPermissionHandler(Android.Manifest.Permission.Bluetooth, false);
