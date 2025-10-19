using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth.CharacteristicAccess;

/// <summary>
/// Implementation of characteristic access service for predefined types that implement the required interfaces.
/// </summary>
/// <typeparam name="TRead">The type for reading values, must implement <see cref="IBluetoothCharacteristicObjectReadable"/>.</typeparam>
/// <typeparam name="TWrite">The type for writing values, must implement <see cref="IBluetoothCharacteristicObjectWritable"/>.</typeparam>
public class CharacteristicAccessServiceForPredefinedTypes<TRead, TWrite> : CharacteristicAccessService<TRead, TWrite> where TRead : IBluetoothCharacteristicObjectReadable, new()
                                                                                                                       where TWrite : IBluetoothCharacteristicObjectWritable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicAccessServiceForPredefinedTypes{TRead, TWrite}"/> class.
    /// </summary>
    /// <param name="characteristicId">The unique identifier of the characteristic.</param>
    /// <param name="name">The name of the characteristic.</param>
    public CharacteristicAccessServiceForPredefinedTypes(Guid characteristicId, string name) : base(characteristicId, name)
    {
    }

    /// <inheritdoc />
    protected override ReadOnlySpan<byte> ToBytes(TWrite input)
    {
        try
        {
            return input.ToByteArray();
        }
        catch (Exception)
        {
            // LOG : WARNING - Error occurred serializing input of type {typeof(TWrite)} into bytes
            throw;
        }
    }

    /// <inheritdoc />
    protected override TRead FromBytes(ReadOnlySpan<byte> value)
    {
        var output = new TRead();
        try
        {
            output.FromByteArray(value);
            if (output is IBluetoothCharacteristicObjectReadableMultiType multiType)
            {
                output = (TRead)multiType.ToFinalObjectType();
            }
        }
        catch (Exception)
        {
            // LOG : WARNING - Error occurred parsing value into {typeof(TRead)}
            throw;
        }

        return output;
    }
}
