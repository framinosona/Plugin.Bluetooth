namespace Plugin.Bluetooth.Exceptions;

public class BluetoothUnhandledExceptionListener : ExceptionListener
{
    /// <summary>
    /// List of listeners for unhandled Bluetooth GATT exceptions.
    /// </summary>
    private readonly static List<BluetoothUnhandledExceptionListener> _bluetoothUnhandledExceptionListeners = [];

    public BluetoothUnhandledExceptionListener(EventHandler<ExceptionEventArgs> received) : base(received)
    {
        _bluetoothUnhandledExceptionListeners.Add(this);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _bluetoothUnhandledExceptionListeners.Remove(this);
        }
        base.Dispose(disposing);
    }

    public static void OnBluetoothUnhandledException(object? sender, Exception e)
    {
        if (_bluetoothUnhandledExceptionListeners.Count == 0)
        {
            // No listeners registered; rethrow the exception.
            throw e;
        }
        foreach (var listener in _bluetoothUnhandledExceptionListeners.ToList())
        {
            listener.OnReceived(sender, e);
        }
    }
}
