namespace Plugin.Bluetooth.Exceptions;

public abstract class BluetoothException(string message, Exception? innerException) : Exception(message, innerException)
{
    protected BluetoothException() : this("Unknown bluetooth exception", null)
    {
    }

    protected BluetoothException(string message) : this(message, null)
    {
    }
}
