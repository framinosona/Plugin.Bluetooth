using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic : BaseBluetoothCharacteristic, CbPeripheralProxy.ICbCharacteristicDelegate
{
    public CBCharacteristic NativeCharacteristic { get; }

    public BluetoothCharacteristic(IBluetoothService service, Guid id, CBCharacteristic native) : base(service, id)
    {
        NativeCharacteristic = native;
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

    #region CbPeripheralProxy.ICbCharacteristicDelegate
    public void DiscoveredDescriptor(NSError? error, CBCharacteristic characteristic)
    {
        throw new NotImplementedException();
    }

    public void WroteCharacteristicValue(NSError? error, CBCharacteristic characteristic)
    {
        throw new NotImplementedException();
    }

    public void UpdatedCharacteristicValue(NSError? error, CBCharacteristic characteristic)
    {
        throw new NotImplementedException();
    }

    public void UpdatedNotificationState(NSError? error, CBCharacteristic characteristic)
    {
        throw new NotImplementedException();
    }

    public void UpdatedValue(NSError? error, CBDescriptor descriptor)
    {
        throw new NotImplementedException();
    }

    public void WroteDescriptorValue(NSError? error, CBDescriptor descriptor)
    {
        throw new NotImplementedException();
    }
    #endregion
}
