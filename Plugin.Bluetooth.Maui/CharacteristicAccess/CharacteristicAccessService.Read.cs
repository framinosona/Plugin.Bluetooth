using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth.CharacteristicAccess;

public abstract partial class CharacteristicAccessService<TRead, TWrite>
{
    /// <inheritdoc />
    public async ValueTask<bool> CanReadAsync(IBluetoothDevice device)
    {
        try
        {
            if (!await HasCharacteristicAsync(device).ConfigureAwait(false))
            {
                return false; // Avoid throwing exceptions if the characteristic doesn't exist
            }

            var characteristic = await GetCharacteristicAsync(device).ConfigureAwait(false);
            return characteristic.CanRead;
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            // LOG : ERROR - Error checking if characteristic can be read
            _ = ex; // Suppress unused variable warning
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<TRead> ReadAsync(IBluetoothDevice device, bool useLastValueIfPreviouslyRead = false, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        var characteristic = await GetCharacteristicAsync(device).ConfigureAwait(false);
        var rawByteArray = await characteristic.ReadValueAsync(useLastValueIfPreviouslyRead, timeout, cancellationToken).ConfigureAwait(false);
        return FromBytes(rawByteArray.Span);
    }
}
