namespace Plugin.Bluetooth.Maui.PlatformSpecific.Permissions;

public class AndroidPermissionForBluetooth() : BaseAndroidPermissionHandler(Android.Manifest.Permission.Bluetooth, false);
