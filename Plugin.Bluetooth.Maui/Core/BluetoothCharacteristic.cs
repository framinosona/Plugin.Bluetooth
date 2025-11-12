namespace Plugin.Bluetooth.Maui;

/// <inheritdoc/>
public partial class BluetoothCharacteristic : BaseBluetoothCharacteristic
{

    /// <summary>
    /// Initializes a new instance of the platform-agnostic <see cref="BluetoothCharacteristic"/> class.
    /// </summary>
    /// <param name="service">The Bluetooth service associated with this characteristic.</param>
    /// <param name="id">The unique identifier of the characteristic.</param>
    public BluetoothCharacteristic(IBluetoothService service, Guid id) : base(service, id)
    {
    }

    #region BaseBluetoothCharacteristic
    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected override bool NativeCanListen()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected async override ValueTask NativeReadIsListeningAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected async override ValueTask NativeWriteIsListeningAsync(bool shouldBeListening, Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected async override ValueTask NativeReadValueAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected override bool NativeCanRead()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected async override ValueTask NativeWriteValueAsync(ReadOnlyMemory<byte> value, Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">This platform-agnostic implementation throws NotImplementedException.</exception>
    protected override bool NativeCanWrite()
    {
        throw new NotImplementedException();
    }
    #endregion
}
