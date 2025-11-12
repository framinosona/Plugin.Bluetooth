using Plugin.Bluetooth.Maui.PlatformSpecific.Exceptions;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic
{
    protected override bool NativeCanRead()
    {
        return NativeCharacteristic.Properties.HasFlag(GattProperty.Read);
    }

    protected override ValueTask NativeReadValueAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure BluetoothGatt exists and is available
        ArgumentNullException.ThrowIfNull(BluetoothGattProxy, nameof(BluetoothGattProxy));

        // Ensure READ is supported
        CharacteristicCantReadException.ThrowIfCantRead(this);

        // Call ReadCharacteristic and Handle return value
        if (!BluetoothGattProxy.BluetoothGatt.ReadCharacteristic(NativeCharacteristic))
        {
            throw new CharacteristicReadException(this, "BluetoothGatt.ReadCharacteristic() Failed, returned false");
        }

        return ValueTask.CompletedTask;
    }

    public void OnCharacteristicChanged(BluetoothGattCharacteristic? characteristic, byte[]? value)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(characteristic, nameof(characteristic));

            // Should not happen ... but just in case
            if (!(characteristic.Uuid?.Equals(NativeCharacteristic.Uuid) ?? false))
            {
                throw new CharacteristicReadException(this, $"OnCharacteristicChanged : {characteristic.Uuid} != {NativeCharacteristic.Uuid}");
            }

            OnReadValueSucceeded(value ?? []);
        }
        catch (Exception e)
        {
            OnReadValueFailed(e);
        }
    }

    public void OnCharacteristicRead(GattStatus status, BluetoothGattCharacteristic? characteristic, byte[]? value)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(characteristic, nameof(characteristic));

            // Should not happen ... but just in case
            if (!(characteristic.Uuid?.Equals(NativeCharacteristic.Uuid) ?? false))
            {
                throw new CharacteristicReadException(this, $"OnCharacteristicRead : {characteristic.Uuid} != {NativeCharacteristic.Uuid}");
            }

            AndroidNativeGattCallbackStatusException.ThrowIfNotSuccess(status);

            OnReadValueSucceeded(value ?? []);
        }
        catch (Exception e)
        {
            OnReadValueFailed(e);
        }
    }

}
