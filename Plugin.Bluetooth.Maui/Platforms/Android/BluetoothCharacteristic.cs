using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public class BluetoothCharacteristic : BaseBluetoothCharacteristic
{
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

    #region BaseBluetoothCharacteristic

    protected override bool NativeCanListen()
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeReadIsListeningAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeWriteIsListeningAsync(bool shouldBeListening, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeReadValueAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override bool NativeCanRead()
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeWriteValueAsync(ReadOnlyMemory<byte> value, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override bool NativeCanWrite()
    {
        throw new NotImplementedException();
    }

    #endregion

}
