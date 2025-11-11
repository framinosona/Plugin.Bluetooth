namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothCharacteristic
{
    private SemaphoreSlim WriteSemaphoreSlim { get; } = new SemaphoreSlim(1, 1);

    /// <summary>
    /// Gets a value indicating whether a write value operation is currently in progress.
    /// </summary>
    public bool IsWritingValue
    {
        get => GetValue(false);
        private set => SetValue(value);
    }

    private TaskCompletionSource? WriteValueTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    #region IBluetoothCharacteristic Members

    /// <inheritdoc/>
    public async Task WriteValueAsync(ReadOnlyMemory<byte> value, bool skipIfOldValueMatchesNewValue = false, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure Device is Connected
        DeviceNotConnectedException.ThrowIfNotConnected(Service.Device);

        // Ensure WRITE is supported
        CharacteristicCantWriteException.ThrowIfCantWrite(this);

        // Check if the value is already written and skipIfOldValueMatchesNewValue is true
        if (skipIfOldValueMatchesNewValue && Value.IsIdenticalTo(value))
        {
            return;
        }

        // Prevents multiple calls to WriteValueAsync, putting them in a queue. WARNING: If the queue gets too long, timeout might be reached
        await WriteSemaphoreSlim.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);

        // Should not happen because of the semaphore, but just in case
        if (WriteValueTcs is { Task.IsCompleted: false })
        {
            throw new CharacteristicAlreadyWritingException(this);
        }

        WriteValueTcs = new TaskCompletionSource(); // Reset the TCS
        IsWritingValue = true; // Set the writing flag

        try
        {
            // Actual start writing native call
            await NativeWriteValueAsync(value, nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            // if exception is thrown during start, we trigger the failure
            OnWriteValueFailed(e);
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnWriteValueSuccess to be called
            await WriteValueTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            // Reset the writing flag
            IsWritingValue = false;
            WriteValueTcs = null;
            WriteSemaphoreSlim.Release();
        }
    }

    #endregion

    /// <inheritdoc/>
    protected abstract ValueTask NativeWriteValueAsync(ReadOnlyMemory<byte> value, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Called when writing the characteristic's value succeeds. Completes the task successfully.
    /// </summary>
    protected void OnWriteValueSucceeded()
    {
        // Attempt to dispatch success to the TaskCompletionSource
        var success = WriteValueTcs?.TrySetResult() ?? false;
        if (success)
        {
            return;
        }

        // Else throw an exception
        throw new CharacteristicUnexpectedWriteException(this);
    }

    /// <summary>
    /// Called when writing the characteristic's value fails. Completes the task with an exception or dispatches to the unhandled exception listener.
    /// </summary>
    /// <param name="e">The exception that occurred during the write operation.</param>
    protected void OnWriteValueFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = WriteValueTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    #region CanWrite

    /// <summary>
    /// Platform-specific implementation to determine if the characteristic can be written to.
    /// </summary>
    /// <returns>True if the characteristic supports write operations; otherwise, false.</returns>
    protected abstract bool NativeCanWrite();

    private Lazy<bool> LazyCanWrite { get; }

    /// <inheritdoc/>
    public bool CanWrite => LazyCanWrite.Value;

    #endregion
}
