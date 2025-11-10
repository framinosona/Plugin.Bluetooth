using System.Diagnostics;

using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothCharacteristic
{
    #region IBluetoothCharacteristic Members

    public bool IsListening
    {
        get => GetValue(false);
        protected set => SetValue(value);
    }


    private void OnValueUpdated(ReadOnlyMemory<byte> newValue, ReadOnlyMemory<byte> oldValue)
    {
        ValueUpdated?.Invoke(this, new ValueUpdatedEventArgs(newValue, oldValue));
    }

    #endregion

    #region CanListen

    protected abstract bool NativeCanListen();

    private Lazy<bool> LazyCanListen { get; }

    public bool CanListen => LazyCanListen.Value;

    #endregion

    /// <inheritdoc/>
    public async ValueTask StartListeningAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        await ReadIsListeningAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        if (IsListening)
        {
            return;
        }

        await WriteIsListeningAsync(true, nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        await ReadIsListeningAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async ValueTask StopListeningAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        await ReadIsListeningAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        if (!IsListening)
        {
            return;
        }

        await WriteIsListeningAsync(false, nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        await ReadIsListeningAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask<ReadOnlyMemory<byte>> WaitForValueChangeAsync(Func<ReadOnlyMemory<byte>, bool>? valueFilter = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<ReadOnlyMemory<byte>>();

        ValueUpdated += EventCallback;

        return await tcs.Task.WaitBetterAsync(timeout, cancellationToken: cancellationToken).ConfigureAwait(false);

        void EventCallback(object? sender, ValueUpdatedEventArgs valueUpdatedEventArgs)
        {
            if (valueFilter != null && !valueFilter.Invoke(valueUpdatedEventArgs.NewValue))
            {
                return;
            }

            ValueUpdated -= EventCallback;
            tcs.TrySetResult(valueUpdatedEventArgs.NewValue);
        }
    }

    #region ReadIsListening

    /// <inheritdoc/>
    protected abstract ValueTask NativeReadIsListeningAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    protected void OnReadIsListeningSucceeded(bool isListening)
    {
        IsListening = isListening;

        // Attempt to dispatch success to the TaskCompletionSource
        var success = ReadIsListeningTcs?.TrySetResult(isListening) ?? false;
        if (success)
        {
            return;
        }

        // Else throw an exception
        throw new CharacteristicUnexpectedReadNotifyException(this);
    }

    protected void OnReadIsListeningFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = ReadIsListeningTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    public bool IsReadingIsListening
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    private TaskCompletionSource<bool>? ReadIsListeningTcs
    {
        get => GetValue<TaskCompletionSource<bool>?>(null);
        set => SetValue(value);
    }

    /// <inheritdoc/>
    public async ValueTask<bool> ReadIsListeningAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure Device is Connected
        DeviceNotConnectedException.ThrowIfNotConnected(Service.Device);

        // Ensure LISTEN is supported
        CharacteristicCantListenException.ThrowIfCantListen(this);

        // Prevents multiple calls to ReadIsListeningAsync
        if (ReadIsListeningTcs is { Task.IsCompleted: false })
        {
            return await ReadIsListeningTcs.Task.ConfigureAwait(false);
        }

        ReadIsListeningTcs = new TaskCompletionSource<bool>(); // Reset the TCS
        IsReadingIsListening = true; // Set the flag to true

        try // try-catch to dispatch exceptions rising from start reading
        {
            // Actual start reading native call
            await NativeReadIsListeningAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            // if exception is thrown during start, we trigger the failure
            OnReadIsListeningFailed(e);
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnReadValueSuccess to be called
            IsListening = await ReadIsListeningTcs.Task.WaitBetterAsync(timeout, cancellationToken: cancellationToken).ConfigureAwait(false);
            return IsListening;
        }
        finally
        {
            // Reset the reading flag
            IsReadingIsListening = false;
            ReadIsListeningTcs = null;
        }
    }

    #endregion

    #region WriteIsListening

    /// <inheritdoc/>
    protected abstract ValueTask NativeWriteIsListeningAsync(bool shouldBeListening, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    protected void OnWriteIsListeningSucceeded()
    {
        // Attempt to dispatch success to the TaskCompletionSource
        var success = WriteIsListeningTcs?.TrySetResult() ?? false;
        if (success)
        {
            return;
        }

        // Else throw an exception
        throw new CharacteristicUnexpectedWriteNotifyException(this);
    }

    protected void OnWriteIsListeningFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = WriteIsListeningTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    private SemaphoreSlim WriteIsListeningSemaphoreSlim { get; } = new SemaphoreSlim(1, 1);

    public bool IsWritingIsListening
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    private TaskCompletionSource? WriteIsListeningTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    /// <inheritdoc/>
    public async ValueTask WriteIsListeningAsync(bool shouldBeListening, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure Device is Connected
        DeviceNotConnectedException.ThrowIfNotConnected(Service.Device);

        // Ensure LISTEN is supported
        CharacteristicCantListenException.ThrowIfCantListen(this);

        // Check if the characteristic is already listened to
        if (IsListening == shouldBeListening)
        {
            return;
        }

        // Prevents multiple calls to WriteIsListeningAsync, putting them in a queue. WARNING: If the queue gets too long, timeout might be reached
        await WriteIsListeningSemaphoreSlim.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);

        // Should not happen because of the semaphore, but just in case
        if (WriteIsListeningTcs is { Task.IsCompleted: false })
        {
            throw new UnreachableException("Already writing IsListening");
        }

        WriteIsListeningTcs = new TaskCompletionSource(); // Reset the TCS
        IsWritingIsListening = true; // Set the flag to true

        try // try-catch to dispatch exceptions rising from start reading
        {
            // Actual start writing native call
            await NativeWriteIsListeningAsync(shouldBeListening, nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
        }
        catch (CharacteristicException e)
        {
            // if exception is thrown during start, we trigger the failure
            OnWriteIsListeningFailed(e);
        }
        catch (Exception e)
        {
            // if exception is thrown during start, we trigger the failure
            OnWriteIsListeningFailed(new CharacteristicNotifyException(this, innerException: e));
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnWriteIsListeningSuccess to be called
            await WriteIsListeningTcs.Task.WaitBetterAsync(timeout, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            // Reset the writing flag
            IsWritingIsListening = false;
            WriteIsListeningTcs = null;
            WriteIsListeningSemaphoreSlim.Release();
        }
    }

    #endregion
}
