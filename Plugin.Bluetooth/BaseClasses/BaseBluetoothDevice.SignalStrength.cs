namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    // Max and Min are dynamic, meaning if a value higher or lower is detected it will push those bounds
    private static double _closeRssiValue = -50;
    private static double _farRssiValue = -100;

    private readonly ConcurrentQueue<int> _rssiHistory = new ConcurrentQueue<int>();

    /// <summary>
    ///     The algorithm will store the last <see cref="SignalStrengthJitterSmoothingStrengthOnAdvertisement" /> values and
    ///     average them out.
    ///     This has the effect of smoothing out the jitter in the signal strength.
    ///     The higher the value the smoother the signal strength will be, but also the less reactive it will be.
    ///     This is the value to use when not connected : the signal strength is more jittery when not connected.
    /// </summary>
    public static int SignalStrengthJitterSmoothingStrengthOnAdvertisement { get; set; } = 5;

    /// <summary>
    ///     The algorithm will store the last <see cref="SignalStrengthJitterSmoothingStrengthConnected" /> values and average
    ///     them out.
    ///     This has the effect of smoothing out the jitter in the signal strength.
    ///     The higher the value the smoother the signal strength will be, but also the less reactive it will be.
    ///     This is the value to use when connected : the signal strength is more stable when connected.
    /// </summary>
    public static int SignalStrengthJitterSmoothingStrengthConnected { get; set; } = 3;

    public bool IsReadingSignalStrength
    {
        get => GetValue(false);
        protected set => SetValue(value);
    }

    private TaskCompletionSource? SignalStrengthReadingTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    #region IBluetoothDevice Members

    /// <inheritdoc />
    public int SignalStrengthDbm
    {
        get => GetValue(0);
        private set
        {
            SetValue(value);
            _rssiHistory.Enqueue(value, IsConnected ? SignalStrengthJitterSmoothingStrengthConnected : SignalStrengthJitterSmoothingStrengthOnAdvertisement);
            SignalStrengthPercent = RssiToSignalStrengthConverter(_rssiHistory.Average());
        }
    }

    /// <inheritdoc />
    public double SignalStrengthPercent
    {
        get => GetValue(0d);
        private set => SetValue(value);
    }

    #endregion

    /// <summary>
    ///     Turns a DBM RSSI value into a percentage, compared to the highest RSSI recorded and tha lowest.
    /// </summary>
    /// <param name="rssi">Input in DBM.</param>
    /// <returns>Percentage.</returns>
    protected static double RssiToSignalStrengthConverter(double rssi)
    {
        if (rssi <= _farRssiValue)
        {
            _farRssiValue = rssi;
            return 0;
        }

        if (rssi >= _closeRssiValue)
        {
            _closeRssiValue = rssi;
            return 1;
        }

        // Distance = ( rssi - Far ) / ( Close - Far)

        // Example :
        // Rssi : -64
        // ( -64 - ( -100 )) / (( -26 ) - ( -100 ))
        // (-64+100) / (-26+100)
        // 36/74
        // 0.49
        return (rssi - _farRssiValue) / (_closeRssiValue - _farRssiValue);
    }

    protected void OnSignalStrengthRead(int rssi)
    {
        SignalStrengthDbm = rssi;
        SignalStrengthReadingTcs?.TrySetResult();
    }

    protected void OnSignalStrengthReadFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = SignalStrengthReadingTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    protected abstract void NativeReadSignalStrength();

    public async Task TryReadSignalStrengthAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await ReadSignalStrengthAsync(timeout, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
        }
    }

    public async Task ReadSignalStrengthAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Ensure Device is Connected
        DeviceNotConnectedException.ThrowIfNotConnected(this);

        // Prevents multiple calls to ReadValueAsync, if already reading signal strength, we merge the calls
        if (SignalStrengthReadingTcs is { Task.IsCompleted: false })
        {
            await SignalStrengthReadingTcs.Task.ConfigureAwait(false);
            return;
        }

        SignalStrengthReadingTcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously); // Reset the TCS
        IsReadingSignalStrength = true;

        // try-catch to dispatch exceptions rising from start
        try
        {
            // Actual signal strength reading native call
            NativeReadSignalStrength();
        }
        catch (Exception e)
        {
            // if exception is thrown during start, we trigger the failure
            OnSignalStrengthReadFailed(e);
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnSignalStrengthReadSucceeded to be called
            await SignalStrengthReadingTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            // Reset the reading flag
            IsReadingSignalStrength = false;
            SignalStrengthReadingTcs = null;
        }
    }
}
