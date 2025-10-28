using Plugin.Bluetooth.Abstractions;
using Plugin.Bluetooth.Exceptions;

namespace Plugin.Bluetooth.CharacteristicAccess;

public abstract partial class CharacteristicAccessService<TRead, TWrite>
{
    /// <inheritdoc />
    public async ValueTask<bool> CanWriteAsync(IBluetoothDevice device)
    {
        try
        {
            if (!await HasCharacteristicAsync(device).ConfigureAwait(false))
            {
                return false; // Avoid throwing exceptions if the characteristic doesn't exist
            }

            var characteristic = await GetCharacteristicAsync(device).ConfigureAwait(false);
            return characteristic.CanWrite;
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            // LOG : ERROR - Error checking if characteristic can be written {ex}
            return false;
        }
    }

    /// <inheritdoc />
    public async Task WriteAsync(IBluetoothDevice device, TWrite value, bool skipIfOldValueMatchesNewValue = false, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(device, nameof(device));

        var characteristic = await GetCharacteristicAsync(device).ConfigureAwait(false);
        if (characteristic.CanWrite == false)
        {
            throw new CharacteristicWriteException(characteristic, message: "Characteristic does not support writing");
        }
        await characteristic.WriteValueAsync(ToBytes(value), skipIfOldValueMatchesNewValue, timeout, cancellationToken).ConfigureAwait(false);
    }
}
