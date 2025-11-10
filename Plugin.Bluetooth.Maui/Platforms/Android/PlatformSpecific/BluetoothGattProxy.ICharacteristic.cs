namespace Plugin.Bluetooth.Maui.PlatformSpecific;

#pragma warning disable CA1034 // Nested types should not be visible

public partial class BluetoothGattProxy
{
    /// <summary>
    /// Interface for handling Bluetooth GATT characteristic operations.
    /// Extends the base characteristic interface with Android-specific callback methods.
    /// </summary>
    public interface ICharacteristic : Plugin.Bluetooth.Abstractions.IBluetoothCharacteristic
    {
        /// <summary>
        /// Called when a characteristic's value has changed.
        /// </summary>
        /// <param name="characteristic">The characteristic that changed.</param>
        /// <param name="value">The new value of the characteristic.</param>
        void OnCharacteristicChanged(BluetoothGattCharacteristic? characteristic, byte[]? value);

        /// <summary>
        /// Called when a characteristic read operation has completed.
        /// </summary>
        /// <param name="status">The status of the read operation.</param>
        /// <param name="characteristic">The characteristic that was read.</param>
        /// <param name="value">The value that was read from the characteristic.</param>
        void OnCharacteristicRead(GattStatus status, BluetoothGattCharacteristic? characteristic, byte[]? value);

        /// <summary>
        /// Called when a characteristic write operation has completed.
        /// </summary>
        /// <param name="status">The status of the write operation.</param>
        /// <param name="characteristic">The characteristic that was written to.</param>
        void OnCharacteristicWrite(GattStatus status, BluetoothGattCharacteristic? characteristic);

        /// <summary>
        /// Called when a descriptor read operation has completed.
        /// </summary>
        /// <param name="status">The status of the read operation.</param>
        /// <param name="descriptor">The descriptor that was read.</param>
        /// <param name="value">The value that was read from the descriptor.</param>
        void OnDescriptorRead(GattStatus status, BluetoothGattDescriptor? descriptor, byte[]? value);

        /// <summary>
        /// Called when a descriptor write operation has completed.
        /// </summary>
        /// <param name="status">The status of the write operation.</param>
        /// <param name="descriptor">The descriptor that was written to.</param>
        void OnDescriptorWrite(GattStatus status, BluetoothGattDescriptor? descriptor);
    }
}

#pragma warning restore CA1034 // Nested types should not be visible
