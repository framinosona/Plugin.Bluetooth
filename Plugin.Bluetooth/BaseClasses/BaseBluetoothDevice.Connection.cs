using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    public bool IsConnected
    {
        get => GetValue(false);
        set
        {
            if (SetValue(value))
            {
                if (value)
                {
                    Connected?.Invoke(this, System.EventArgs.Empty);
                }
                else
                {
                    Disconnected?.Invoke(this, System.EventArgs.Empty);
                }
            }
        }
    }

    public ValueTask WaitForIsConnectedAsync(bool isConnected, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        return WaitForPropertyToBeOfValue(nameof(IsConnected), isConnected, timeout, cancellationToken);
    }

    protected abstract void NativeRefreshIsConnected();

    #region Connection - Connect

    public bool IsConnecting
    {
        get => GetValue(false);
        private set => SetValue(value);
    }

    public event EventHandler? Connected;

    public event EventHandler? Connecting;

    private TaskCompletionSource? ConnectionTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    protected void OnConnectSucceeded()
    {
        NativeRefreshIsConnected();

        // Attempt to dispatch success to the TaskCompletionSource
        var success = ConnectionTcs?.TrySetResult() ?? false;
        if (success)
        {
            return;
        }

        if(IsConnected)
        {
            return;
        }

        // Else throw an exception
        throw new DeviceFailedToConnectException(this);
    }

    protected void OnConnectFailed(Exception e)
    {
        NativeRefreshIsConnected();

        // Attempt to dispatch exception to the TaskCompletionSource
        var success = ConnectionTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    /// <inheritdoc/>
    public async virtual ValueTask ConnectIfNeededAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        NativeRefreshIsConnected();
        if (IsConnected)
        {
            return;
        }

        await ConnectAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async virtual Task ConnectAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure we are not already connected
        DeviceIsAlreadyConnectedException.ThrowIfAlreadyConnected(this);

        // Prevents multiple calls to ConnectAsync, if already starting, we merge the calls
        if (ConnectionTcs is { Task.IsCompleted: false })
        {
            await ConnectionTcs.Task.ConfigureAwait(false);
            return;
        }

        ConnectionTcs = new TaskCompletionSource(); // Reset the TCS
        IsConnecting = true; // Set the connecting state to true
        Connecting?.Invoke(this, System.EventArgs.Empty);

        try // try-catch to dispatch exceptions rising from start
        {
            NativeConnect(nativeOptions, timeout, cancellationToken); // actual start native call
        }
        catch (Exception e)
        {
            OnConnectFailed(e); // if exception is thrown during start, we trigger the failure
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnConnectSucceeded to be called
            await ConnectionTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);

            NativeRefreshIsConnected();
            if (!IsConnected)
            {
                throw new DeviceFailedToConnectException(this);
            }
        }
        finally
        {
            IsConnecting = false; // Set the connecting state to false
            ConnectionTcs = null;
        }
    }

    protected abstract void NativeConnect(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    #endregion

    #region Connection - Disconnect

    public bool IsDisconnecting
    {
        get => GetValue(false);
        private set => SetValue(value);
    }

    public event EventHandler? Disconnected;

    public event EventHandler? Disconnecting;

    private TaskCompletionSource? DisconnectionTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    protected void OnDisconnect(Exception? e = null)
    {
        NativeRefreshIsConnected();

        // Attempt to dispatch success to the TaskCompletionSource
        var success = DisconnectionTcs?.TrySetResultOrException() ?? false;
        if (success)
        {
            return;
        }

        OnUnexpectedDisconnection(e);
    }

    /// <inheritdoc/>
    public async ValueTask DisconnectIfNeededAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        NativeRefreshIsConnected();
        if (!IsConnected)
        {
            return;
        }

        await DisconnectAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task DisconnectAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure we are not already disconnected
        DeviceIsAlreadyDisconnectedException.ThrowIfAlreadyDisconnected(this);

        // Prevents multiple calls to ConnectAsync, if already starting, we merge the calls
        if (DisconnectionTcs is { Task.IsCompleted: false })
        {
            await DisconnectionTcs.Task.ConfigureAwait(false);
            return;
        }

        DisconnectionTcs = new TaskCompletionSource(); // Reset the TCS
        IsDisconnecting = true; // Set the disconnecting state to true
        Disconnecting?.Invoke(this, System.EventArgs.Empty);

        try // try-catch to dispatch exceptions rising from start
        {
            NativeDisconnect(nativeOptions, timeout, cancellationToken); // actual start native call
        }
        catch (Exception e)
        {
            OnDisconnect(e); // if exception is thrown during start, we trigger the failure
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnDisconnection to be called
            await DisconnectionTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);
            await ClearServicesAsync().ConfigureAwait(false);
            NativeRefreshIsConnected();
            if (IsConnected)
            {
                throw new DeviceFailedToDisconnectException(this);
            }
        }
        finally
        {
            IsDisconnecting = false; // Set the disconnecting state to false
            DisconnectionTcs = null;
        }
    }

    protected abstract void NativeDisconnect(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    #endregion

    #region Connection - UnexpectedDisconnection

    public event EventHandler<DeviceUnexpectedDisconnectionEventArgs>? UnexpectedDisconnection;

    public bool IgnoreNextUnexpectedDisconnection { get; set; }

    protected virtual void OnUnexpectedDisconnection(Exception? e = null)
    {
        ClearServicesAsync().StartAndForget(ex => BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, ex));
        if (IgnoreNextUnexpectedDisconnection)
        {
            IgnoreNextUnexpectedDisconnection = false;
        }
        else // unexpected disconnection
        {
            UnexpectedDisconnection?.Invoke(this, new DeviceUnexpectedDisconnectionEventArgs(e));
        }
    }

    #endregion
}
