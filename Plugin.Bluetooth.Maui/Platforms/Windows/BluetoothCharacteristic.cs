using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic : BaseBluetoothCharacteristic, GattCharacteristicProxy.IBluetoothCharacteristicProxyDelegate
{
    public GattCharacteristicProxy GattCharacteristicProxy { get; }

    public BluetoothCharacteristic(IBluetoothService service, Guid id, GattCharacteristic nativeCharacteristic) : base(service, id)
    {
        GattCharacteristicProxy = new GattCharacteristicProxy(nativeCharacteristic, bluetoothCharacteristicProxyDelegate: this);
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

    #region GattCharacteristicProxy.IBluetoothCharacteristicProxyDelegate

    public void OnValueChanged(byte[] value, DateTimeOffset argsTimestamp)
    {
        throw new NotImplementedException();
    }

    #endregion

}
