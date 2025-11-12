using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic : BaseBluetoothCharacteristic, BluetoothGattProxy.ICharacteristic
{
    /// <summary>
    ///     See org.bluetooth.descriptor.gatt.client_characteristic_configuration in https://
    ///     www.bluetooth.com/specifications/gatt/descriptors/
    /// </summary>
    public const string NotificationDescriptorId = "00002902-0000-1000-8000-00805f9b34fb";

    public BluetoothGattCharacteristic NativeCharacteristic { get; }

    public BluetoothGattProxy BluetoothGattProxy { get; }

    public BluetoothCharacteristic(IBluetoothService service, Guid id, BluetoothGattCharacteristic bluetoothGattCharacteristic) : base(service, id)
    {
        if (Service.Device is not BluetoothDevice androidDevice)
        {
            throw new ArgumentException("The provided service's device is not a valid Android BluetoothDevice.", nameof(service));
        }
        BluetoothGattProxy = androidDevice.BluetoothGattProxy ?? throw new InvalidOperationException("The BluetoothGattProxy is not initialized.");
        NativeCharacteristic = bluetoothGattCharacteristic;
    }
}
