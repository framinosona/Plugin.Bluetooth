using System.Diagnostics;

using Plugin.Bluetooth.Maui.Helpers;
using Plugin.Bluetooth.Maui.PlatformSpecific.Exceptions;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic
{
    protected override bool NativeCanWrite()
    {
        return NativeCharacteristic.Properties.HasFlag(GattProperty.WriteNoResponse) || NativeCharacteristic.Properties.HasFlag(GattProperty.Write) || NativeCharacteristic.Properties.HasFlag(GattProperty.SignedWrite);
    }

    protected async override ValueTask NativeWriteValueAsync(ReadOnlyMemory<byte> value, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure BluetoothGatt exists and is available
        ArgumentNullException.ThrowIfNull(BluetoothGattProxy, nameof(BluetoothGattProxy));

        // Ensure READ is supported
        CharacteristicCantWriteException.ThrowIfCantWrite(this);

        // Call ReadCharacteristic and Handle return value
        await RetryTools.RunWithRetriesAsync(() => BluetoothGattCharacteristicWrite(value), maxRetries: 3, delayBetweenRetries: TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
    }

    private void BluetoothGattCharacteristicWrite(ReadOnlyMemory<byte> value)
    {
        // Ensure BluetoothGatt exists and is available
        ArgumentNullException.ThrowIfNull(BluetoothGattProxy, nameof(BluetoothGattProxy));

        // Ensure WRITE is supported
        CharacteristicCantWriteException.ThrowIfCantWrite(this);

        // Get WriteType
        NativeCharacteristic.WriteType = GetBluetoothGattCharacteristicWriteType();

        if (OperatingSystem.IsAndroidVersionAtLeast(33))
        {
            // Write the value
            var writeResult = (Android.Bluetooth.CurrentBluetoothStatusCodes) BluetoothGattProxy.BluetoothGatt.WriteCharacteristic(NativeCharacteristic, value.ToArray(), (int) GetBluetoothGattCharacteristicWriteType());

            AndroidNativeCurrentBluetoothStatusCodesException.ThrowIfNotSuccess(writeResult);
        }
        else
        {
            // Write the value
            if (!NativeCharacteristic.SetValue(value.ToArray()))
            {
                throw new CharacteristicWriteException(this, value, $"BluetoothGattCharacteristic.SetValue() Failed");
            }

            // Write the characteristic
            if (!BluetoothGattProxy.BluetoothGatt.WriteCharacteristic(NativeCharacteristic))
            {
                throw new CharacteristicWriteException(this, value, "BluetoothGatt.WriteCharacteristic() Failed");
            }
        }
    }

    private GattWriteType GetBluetoothGattCharacteristicWriteType()
    {
        if (NativeCharacteristic.Properties.HasFlag(GattProperty.WriteNoResponse))
        {
            return GattWriteType.NoResponse;
        }

        if (NativeCharacteristic.Properties.HasFlag(GattProperty.SignedWrite))
        {
            return GattWriteType.Signed;
        }

        if (NativeCharacteristic.Properties.HasFlag(GattProperty.Write))
        {
            return GattWriteType.Default;
        }

        throw new UnreachableException("This case should be cought by CharacteristicCantWriteException.ThrowIfCantWrite");
    }

    public void OnCharacteristicWrite(GattStatus status, BluetoothGattCharacteristic? characteristic)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(characteristic, nameof(characteristic));
            AndroidNativeGattStatusException.ThrowIfNotSuccess(status);
            OnWriteValueSucceeded();
        }
        catch (Exception e)
        {
            OnWriteValueFailed(e);
        }
    }

    protected override string ToWriteString()
    {
        if (NativeCharacteristic.Properties.HasFlag(GattProperty.WriteNoResponse))
        {
            return "WNR";
        }

        if (NativeCharacteristic.Properties.HasFlag(GattProperty.SignedWrite))
        {
            return "WS";
        }

        if (NativeCharacteristic.Properties.HasFlag(GattProperty.Write))
        {
            return "W";
        }

        return string.Empty;
    }
}
