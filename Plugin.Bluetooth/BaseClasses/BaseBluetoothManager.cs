
using Microsoft.Extensions.Logging;

using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth;

/// <summary>
/// Base class for Bluetooth manager implementations.
/// </summary>
public abstract class BaseBluetoothManager : BaseBindableObject, IBluetoothManager, IDisposable
{

    #region Constructor & Dispose
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseBluetoothManager"/> class.
    /// </summary>
    protected BaseBluetoothManager(ILogger? logger = null)
    {
        StartPeriodicTimer();
    }

    /// <summary>
    /// Disposes the Bluetooth manager and releases resources.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            StopPeriodicTimer();
        }
    }

    /// <summary>
    /// Disposes the Bluetooth manager and releases resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion

    /// <inheritdoc />
    public abstract bool IsBluetoothOn { get; protected set; }

    /// <inheritdoc />
    public event EventHandler<BluetoothStateChangedEventArgs>? BluetoothStateChanged;

    /// <summary>
    /// Raises the <see cref="BluetoothStateChanged"/> event.
    /// </summary>
    protected virtual void OnBluetoothStateChanged(BluetoothStateChangedEventArgs e)
    {
        BluetoothStateChanged?.Invoke(this, e);
    }

    /// <inheritdoc />
    public abstract ValueTask<IBluetoothScanner> GetScannerAsync();

    /// <inheritdoc />
    public abstract ValueTask<IBluetoothBroadcaster> GetBroadcasterAsync();

    #region PeriodicTimer

    private PeriodicTimer? PeriodicTimer { get; set; }

    /// <summary>
    ///     Refresh loops cleans device list, checks for disappeared devices and triggers the devices refresh functions as well
    /// </summary>
    public TimeSpan TimeBetweenEachRefreshLoop { get; set; } = TimeSpan.FromSeconds(1);

    private async void StartPeriodicTimer()
    {
        PeriodicTimer ??= new PeriodicTimer(TimeBetweenEachRefreshLoop);
        while (PeriodicTimer != null && await PeriodicTimer.WaitForNextTickAsync().ConfigureAwait(false))
        {
            // Do something with await
            // if it takes more than TimeBetweenEachRefreshLoop ms then it "skips a beat"
            await RefreshNativeAdapterValuesAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Refreshes the native adapter values asynchronously.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    protected abstract ValueTask RefreshNativeAdapterValuesAsync();

    private void StopPeriodicTimer()
    {
        if (PeriodicTimer == null)
        {
            return;
        }

        PeriodicTimer.Dispose();
        PeriodicTimer = null;
    }

    #endregion
}
