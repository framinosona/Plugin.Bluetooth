using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

/// <inheritdoc/>
public abstract class BluetoothActivity : BaseBluetoothActivity
{
    protected override void NativeRefreshIsBluetoothOn()
    {
        IsBluetoothOn = BluetoothAdapterProxy.BluetoothAdapter.IsEnabled;
    }
}
