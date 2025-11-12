using Plugin.Bluetooth.Maui.PlatformSpecific.Exceptions;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic
{
    /// <inheritdoc/>
    protected override bool NativeCanRead()
    {
        return NativeCharacteristic.Properties.HasFlag(GattProperty.Read);
    }

    /// <inheritdoc/>
    protected override ValueTask NativeReadValueAsync(Dictionary<string, object>? nativeOptions = null)
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

    /// <summary>
    /// Called when a characteristic value changes (notification or indication received) on the Android platform.
    /// </summary>
    /// <param name="characteristic">The characteristic that changed.</param>
    /// <param name="value">The new value of the characteristic.</param>
    /// <exception cref="CharacteristicReadException">Thrown when the characteristic UUID doesn't match the expected UUID.</exception>
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

    /// <summary>
    /// Called when a GATT characteristic read operation completes on the Android platform.
    /// </summary>
    /// <param name="status">The status of the GATT operation.</param>
    /// <param name="characteristic">The characteristic that was read.</param>
    /// <param name="value">The value read from the characteristic.</param>
    /// <exception cref="AndroidNativeGattCallbackStatusException">Thrown when the GATT status indicates an error.</exception>
    /// <exception cref="CharacteristicReadException">Thrown when the characteristic UUID doesn't match the expected UUID.</exception>
    public void OnCharacteristicRead(GattStatus status, BluetoothGattCharacteristic? characteristic, byte[]? value)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(characteristic, nameof(characteristic));
            AndroidNativeGattCallbackStatusException.ThrowIfNotSuccess(status);

            // Should not happen ... but just in case
            if (!(characteristic.Uuid?.Equals(NativeCharacteristic.Uuid) ?? false))
            {
                throw new CharacteristicReadException(this, $"OnCharacteristicRead : {characteristic.Uuid} != {NativeCharacteristic.Uuid}");
            }

            OnReadValueSucceeded(value ?? []);
        }
        catch (Exception e)
        {
            OnReadValueFailed(e);
        }
    }

}
