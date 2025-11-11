namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothCharacteristic
{
    /// <summary>
    /// Gets a value indicating whether a read value operation is currently in progress.
    /// </summary>
    public bool IsReadingValue
    {
        get => GetValue(false);
        private set => SetValue(value);
    }

    private TaskCompletionSource<ReadOnlyMemory<byte>>? ReadValueTcs
    {
        get => GetValue<TaskCompletionSource<ReadOnlyMemory<byte>>?>(null);
        set => SetValue(value);
    }

    #region IBluetoothCharacteristic Members

    /// <inheritdoc/>
    public async ValueTask<ReadOnlyMemory<byte>> ReadValueAsync(bool skipIfPreviouslyRead = false, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure Device is Connected
        DeviceNotConnectedException.ThrowIfNotConnected(Service.Device);

        // Ensure READ is supported
        CharacteristicCantReadException.ThrowIfCantRead(this);

        // Check if the value is already read and skipIfPreviouslyRead is true
        if (skipIfPreviouslyRead && Value.Length != 0)
        {
            return Value;
        }

        // Prevents multiple calls to ReadValueAsync
        if (ReadValueTcs is { Task.IsCompleted: false })
        {
            return await ReadValueTcs.Task.ConfigureAwait(false);
        }

        ReadValueTcs = new TaskCompletionSource<ReadOnlyMemory<byte>>(); // Reset the TCS
        IsReadingValue = true; // Set the flag to true

        try // try-catch to dispatch exceptions rising from start reading
        {
            await NativeReadValueAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false); // Actual start reading native call
        }
        catch (Exception e)
        {
            // if exception is thrown during start, we trigger the failure
            OnReadValueFailed(e);
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnReadValueSuccess to be called or timeout
            return await ReadValueTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            // Reset the reading flag
            IsReadingValue = false;
            ReadValueTcs = null;
        }
    }

    #endregion

    /// <summary>
    /// Platform-specific implementation to read the characteristic's value.
    /// </summary>
    /// <param name="nativeOptions">Platform-specific options for the read operation.</param>
    /// <param name="timeout">Optional timeout for the operation.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task that completes when the native read operation is initiated.</returns>
    protected abstract ValueTask NativeReadValueAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Called when reading the characteristic's value succeeds. Updates the Value property and completes the task.
    /// </summary>
    /// <param name="value">The value read from the characteristic.</param>
    protected void OnReadValueSucceeded(ReadOnlyMemory<byte> value)
    {
        var old = Value;
        Value = value;

        // Attempt to dispatch success to the TaskCompletionSource
        if (ReadValueTcs != null && ReadValueTcs.TrySetResult(value))
        {
            return;
        }

        // If the TaskCompletionSource wasn't available, but we are listening, dispatch the value to the listener
        if (IsListening)
        {
            OnValueUpdated(value, old);
            return;
        }

        // Else throw an exception
        throw new CharacteristicUnexpectedReadException(this);
    }

    /// <summary>
    /// Called when reading the characteristic's value fails. Completes the task with an exception or dispatches to the unhandled exception listener.
    /// </summary>
    /// <param name="e">The exception that occurred during the read operation.</param>
    protected void OnReadValueFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = ReadValueTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    #region CanRead

    /// <summary>
    /// Platform-specific implementation to determine if the characteristic can be read.
    /// </summary>
    /// <returns>True if the characteristic supports read operations; otherwise, false.</returns>
    protected abstract bool NativeCanRead();

    private Lazy<bool> LazyCanRead { get; }

    /// <inheritdoc/>
    public bool CanRead => LazyCanRead.Value;

    #endregion
}
