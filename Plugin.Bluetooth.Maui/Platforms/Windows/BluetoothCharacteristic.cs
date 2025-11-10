namespace Plugin.Bluetooth.Maui;

public partial class BluetoothCharacteristic : BaseBluetoothCharacteristic
{

    public BluetoothCharacteristic(IBluetoothService service, Guid id) : base(service, id)
    {
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
