using System.Diagnostics;

using Microsoft.Maui.Controls.PlatformConfiguration;

using Plugin.Bluetooth.Maui.Helpers;
using Plugin.Bluetooth.Maui.PlatformSpecific.Exceptions;
using Plugin.ByteArrays;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic
{
    protected override bool NativeCanListen()
    {
        return NativeCharacteristic.Properties.HasFlag(GattProperty.Indicate) || NativeCharacteristic.Properties.HasFlag(GattProperty.Notify);
    }

    protected async override ValueTask NativeReadIsListeningAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure BluetoothGatt exists and is available
        ArgumentNullException.ThrowIfNull(BluetoothGattProxy, nameof(BluetoothGattProxy));

        // Get the descriptor
        if (NativeCharacteristic.GetDescriptor(UUID.FromString(NotificationDescriptorId)) is not { } descriptor)
        {
            throw new CharacteristicNotifyException(this, "BluetoothGattCharacteristic.GetDescriptor() Failed");
        }

        // descriptor.GetValue() is Deprecated in Android 33
        // New way : BluetoothGatt.ReadDescriptor(descriptor)
        if (!BluetoothGattProxy.BluetoothGatt.ReadDescriptor(descriptor))
        {
            throw new CharacteristicNotifyException(this, "BluetoothGatt.ReadDescriptor() Failed");
        }

        await Task.Delay(100, cancellationToken).ConfigureAwait(false); // Give time to Android to process this request
    }

    protected async override ValueTask NativeWriteIsListeningAsync(bool shouldBeListening, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure LISTEN is supported
        CharacteristicCantListenException.ThrowIfCantListen(this);

        // Ensure BluetoothGatt exists and is available
        if (!BluetoothGattProxy.BluetoothGatt.SetCharacteristicNotification(NativeCharacteristic, enable: true))
        {
            throw new CharacteristicNotifyException(this, "BluetoothGatt.SetCharacteristicNotification() Failed");
        }

        // Get the descriptor
        if (NativeCharacteristic.GetDescriptor(UUID.FromString(NotificationDescriptorId)) is not { } descriptor)
        {
            throw new CharacteristicNotifyException(this, "BluetoothGattCharacteristic.GetDescriptor() Failed");
        }

        // Get which bytes to write
        byte[] enableNotificationBytes;
        if (!shouldBeListening)
        {
            ArgumentNullException.ThrowIfNull(BluetoothGattDescriptor.DisableNotificationValue, nameof(BluetoothGattDescriptor.DisableNotificationValue));
            enableNotificationBytes = BluetoothGattDescriptor.DisableNotificationValue.ToArray();
        }
        else if (NativeCharacteristic.Properties.HasFlag(GattProperty.Notify))
        {
            ArgumentNullException.ThrowIfNull(BluetoothGattDescriptor.EnableNotificationValue, nameof(BluetoothGattDescriptor.EnableNotificationValue));
            enableNotificationBytes = BluetoothGattDescriptor.EnableNotificationValue.ToArray();
        }
        else if (NativeCharacteristic.Properties.HasFlag(GattProperty.Indicate))
        {
            ArgumentNullException.ThrowIfNull(BluetoothGattDescriptor.EnableIndicationValue, nameof(BluetoothGattDescriptor.EnableIndicationValue));
            enableNotificationBytes = BluetoothGattDescriptor.EnableIndicationValue.ToArray();
        }
        else
        {
            throw new UnreachableException("This case should be caught by CharacteristicCantListenException.ThrowIfCantListen");
        }

        await RetryTools.RunWithRetriesAsync(() => BluetoothGattCharacteristicWriteDescriptor(descriptor, enableNotificationBytes), maxRetries: 3, delayBetweenRetries: TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
    }


    private void BluetoothGattCharacteristicWriteDescriptor(BluetoothGattDescriptor descriptor, byte[] value)
    {
        // Ensure BluetoothGatt exists and is available
        ArgumentNullException.ThrowIfNull(BluetoothGattProxy, nameof(BluetoothGattProxy));

        // Ensure NOTIFY ot INDICATE is supported
        CharacteristicCantListenException.ThrowIfCantListen(this);

        if (OperatingSystem.IsAndroidVersionAtLeast(33))
        {
            // Write the value
            var writeResult = (Android.Bluetooth.CurrentBluetoothStatusCodes) BluetoothGattProxy.BluetoothGatt.WriteDescriptor(descriptor, value);

            AndroidNativeCurrentBluetoothStatusCodesException.ThrowIfNotSuccess(writeResult);
        }
        else
        {
            // Write the value
            if (!descriptor.SetValue(value))
            {
                throw new CharacteristicNotifyException(this, $"descriptor.SetValue() failed writing {value.ToDebugString()} bytes");
            }

            // Write the characteristic
            if (!BluetoothGattProxy.BluetoothGatt.WriteDescriptor(descriptor))
            {
                throw new CharacteristicNotifyException(this, "BluetoothGatt.WriteDescriptor() Failed");
            }
        }
    }


    public void OnDescriptorRead(GattStatus status, BluetoothGattDescriptor? descriptor, byte[]? value)
    {
        try
        {
            AndroidNativeGattCallbackStatusException.ThrowIfNotSuccess(status);
            ArgumentNullException.ThrowIfNull(descriptor, nameof(descriptor));
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            // Should not happen ... but just in case, check we're dealing with the correct descriptor
            if (descriptor.Uuid?.ToString() != NotificationDescriptorId)
            {
                return;
            }

            OnReadIsListeningSucceeded(value is [1, 0]);

        }
        catch (Exception e)
        {
            OnReadIsListeningFailed(e);
        }
    }

    public virtual void OnDescriptorWrite(GattStatus status, BluetoothGattDescriptor? descriptor)
    {
        try
        {
            AndroidNativeGattCallbackStatusException.ThrowIfNotSuccess(status);
            ArgumentNullException.ThrowIfNull(descriptor, nameof(descriptor));

            // Should not happen ... but just in case, check we're dealing with the correct descriptor
            if (descriptor.Uuid?.ToString() != NotificationDescriptorId)
            {
                return;
            }

            OnWriteIsListeningSucceeded();
        }
        catch (Exception e)
        {
            OnWriteIsListeningFailed(e);
        }
    }

    protected override string ToListenString()
    {
        if (NativeCharacteristic.Properties.HasFlag(GattProperty.Notify))
        {
            return IsListening ? "N*" : "N";
        }
        if (NativeCharacteristic.Properties.HasFlag(GattProperty.Indicate))
        {
            return IsListening ? "I*" : "I";
        }
        return string.Empty;
    }
}
