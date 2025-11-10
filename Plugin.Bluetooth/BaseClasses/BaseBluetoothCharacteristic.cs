
using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

/// <inheritdoc cref="IBluetoothCharacteristic" />
public abstract partial class BaseBluetoothCharacteristic : BaseBindableObject, IBluetoothCharacteristic
{

    protected BaseBluetoothCharacteristic(IBluetoothService service, Guid id)
    {
        ArgumentNullException.ThrowIfNull(service);
        Service = service;
        Id = id;
        AccessService = service.Device.Scanner.KnownServicesAndCharacteristicsRepository.GetCharacteristicAccessService(id);
        Name = AccessService.CharacteristicName;
        if (AccessService is not UnknownCharacteristicAccessService && AccessService.ServiceId != service.Id)
        {
            // Characteristic defined somewhere, but for another service
            throw new CharacteristicFoundInWrongServiceException(AccessService, this, service.Id);
        }

        LazyCanRead = new Lazy<bool>(NativeCanRead);
        LazyCanWrite = new Lazy<bool>(NativeCanWrite);
        LazyCanListen = new Lazy<bool>(NativeCanListen);
    }



    protected async virtual ValueTask DisposeAsyncCore()
    {
        // Stop listening if active
        if (CanListen && IsListening)
        {
            try
            {
                await StopListeningAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, ex);
            }
        }

        // Cancel any pending operations
        ReadValueTcs?.TrySetCanceled();
        WriteValueTcs?.TrySetCanceled();
        ReadIsListeningTcs?.TrySetCanceled();
        WriteIsListeningTcs?.TrySetCanceled();

        // Dispose semaphores
        WriteIsListeningSemaphoreSlim.Dispose();
        WriteSemaphoreSlim.Dispose();

        // Unsubscribe from events
        ValueUpdated = null;
    }

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public IBluetoothCharacteristicAccessService AccessService { get; }

    public IBluetoothService Service { get; }

    public Guid Id { get; }

    public string Name { get; }

    public ReadOnlySpan<byte> ValueSpan => Value.Span;

    public ReadOnlyMemory<byte> Value
    {
        get => GetValue<ReadOnlyMemory<byte>>(default);
        protected set => SetValue(value);
    }


    public event EventHandler<ValueUpdatedEventArgs>? ValueUpdated;

    /// <summary>
    ///     Returns a string representation of the Bluetooth characteristic, including its ID and access capabilities.
    /// </summary>
    /// <remarks>
    ///     The returned string includes a short description of the characteristic, its unique ID, and a shorthand notation
    ///     for its access capabilities:
    ///     <list type="bullet">
    ///         <item>
    ///             <description><c>R</c> if the characteristic is readable.</description>
    ///         </item>
    ///         <item>
    ///             <description><c>W</c> if the characteristic is writable.</description>
    ///         </item>
    ///         <item>
    ///             <description><c>N*</c> if the characteristic supports notifications and is actively listening.</description>
    ///         </item>
    ///         <item>
    ///             <description><c>N</c> if the characteristic supports notifications but is not currently listening.</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <returns>
    ///     A formatted string that includes the characteristic's short description, ID, and access permissions.
    /// </returns>
    /// <example>
    ///     <code>
    /// var characteristicString = characteristic.ToString();
    /// Console.WriteLine(characteristicString); // Output example: CharacteristicName (CharacteristicId) (R/W/N*)
    /// </code>
    /// </example>
    public override string ToString()
    {
        var access = new List<string>
        {
            ToReadString(),
            ToWriteString(),
            ToListenString()
        };
        return $"{Name} ({Id}) ({string.Join("/", access.Where(s => !string.IsNullOrEmpty(s)))})";
    }

    protected virtual string ToReadString()
    {
        return CanRead ? "R" : string.Empty;
    }
    protected virtual string ToWriteString()
    {
        return CanWrite ? "W" : string.Empty;
    }
    protected virtual string ToListenString()
    {
        return CanListen ? IsListening ? "N*" : "N" : string.Empty;
    }
}
