namespace Plugin.Bluetooth.Abstractions.Exceptions;

/// <summary>
///     Represents an exception that occurs in Bluetooth manager operations.
/// </summary>
/// <remarks>
///     This exception provides information about the Bluetooth manager associated with the error,
///     allowing for easier debugging and tracking of manager-related issues.
/// </remarks>
/// <seealso cref="IBluetoothManager" />
public abstract class ManagerException : BluetoothException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ManagerException"/> class.
    /// </summary>
    protected ManagerException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ManagerException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected ManagerException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ManagerException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected ManagerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ManagerException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    protected ManagerException(
        IBluetoothManager manager,
        string message = "Unknown BLE exception",
        Exception? innerException = null)
        : base(message, innerException)
    {
        ArgumentNullException.ThrowIfNull(manager);
        Manager = manager;
    }

    /// <summary>
    ///     Gets the Bluetooth manager associated with the exception.
    /// </summary>
    public IBluetoothManager? Manager { get; }
}

/// <summary>
///     Represents an exception that occurs when Bluetooth is turned off.
/// </summary>
/// <seealso cref="ManagerException" />
public class BluetoothIsOffException : ManagerException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BluetoothIsOffException"/> class.
    /// </summary>
    public BluetoothIsOffException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BluetoothIsOffException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BluetoothIsOffException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BluetoothIsOffException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public BluetoothIsOffException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BluetoothIsOffException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public BluetoothIsOffException(
        IBluetoothManager manager,
        string message = "Bluetooth is off",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }

    /// <summary>
    ///     Throws a <see cref="BluetoothIsOffException"/> if Bluetooth is turned off.
    /// </summary>
    /// <param name="manager">The Bluetooth manager to check.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="manager"/> is null.</exception>
    /// <exception cref="BluetoothIsOffException">Thrown when Bluetooth is turned off.</exception>
    public static void ThrowIfBluetoothIsOff(IBluetoothManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        if (!manager.IsBluetoothOn)
        {
            throw new BluetoothIsOffException(manager);
        }
    }
}

/// <summary>
///     Represents an exception that occurs when a Bluetooth manager fails to start.
/// </summary>
/// <seealso cref="ManagerException" />
public class FailedToStartException : ManagerException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStartException"/> class.
    /// </summary>
    public FailedToStartException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStartException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public FailedToStartException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStartException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public FailedToStartException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStartException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public FailedToStartException(
        IBluetoothManager manager,
        string message = "Failed to start",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }
}

/// <summary>
///     Represents an exception that occurs when a Bluetooth manager is already started.
/// </summary>
/// <seealso cref="FailedToStartException" />
public class IsAlreadyStartedException : FailedToStartException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStartedException"/> class.
    /// </summary>
    public IsAlreadyStartedException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStartedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public IsAlreadyStartedException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStartedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public IsAlreadyStartedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStartedException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public IsAlreadyStartedException(
        IBluetoothManager manager,
        string message = "Already started",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }

    /// <summary>
    ///     Throws an <see cref="IsAlreadyStartedException"/> if the Bluetooth manager is already started.
    /// </summary>
    /// <param name="manager">The Bluetooth manager to check.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="manager"/> is null.</exception>
    /// <exception cref="IsAlreadyStartedException">Thrown when the manager is already running.</exception>
    public static void ThrowIfIsStarted(IBluetoothManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        if (manager.IsRunning)
        {
            throw new IsAlreadyStartedException(manager);
        }
    }
}

/// <summary>
///     Represents an exception that occurs when a Bluetooth manager starts unexpectedly.
/// </summary>
/// <seealso cref="ManagerException" />
public class UnexpectedStartException : ManagerException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStartException"/> class.
    /// </summary>
    public UnexpectedStartException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStartException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public UnexpectedStartException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStartException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public UnexpectedStartException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStartException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public UnexpectedStartException(
        IBluetoothManager manager,
        string message = "Unexpected start",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }
}

/// <summary>
///     Represents an exception that occurs when a Bluetooth manager fails to stop.
/// </summary>
/// <seealso cref="ManagerException" />
public class FailedToStopException : ManagerException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStopException"/> class.
    /// </summary>
    public FailedToStopException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStopException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public FailedToStopException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStopException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public FailedToStopException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailedToStopException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public FailedToStopException(
        IBluetoothManager manager,
        string message = "Failed to stop",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }
}

/// <summary>
///     Represents an exception that occurs when a Bluetooth manager is already stopped.
/// </summary>
/// <seealso cref="FailedToStopException" />
public class IsAlreadyStoppedException : FailedToStopException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStoppedException"/> class.
    /// </summary>
    public IsAlreadyStoppedException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStoppedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public IsAlreadyStoppedException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStoppedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public IsAlreadyStoppedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="IsAlreadyStoppedException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public IsAlreadyStoppedException(
        IBluetoothManager manager,
        string message = "Already stopped",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }

    /// <summary>
    ///     Throws an <see cref="IsAlreadyStoppedException"/> if the Bluetooth manager is already stopped.
    /// </summary>
    /// <param name="manager">The Bluetooth manager to check.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="manager"/> is null.</exception>
    /// <exception cref="IsAlreadyStoppedException">Thrown when the manager is already stopped.</exception>
    public static void ThrowIfIsStopped(IBluetoothManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        if (!manager.IsRunning)
        {
            throw new IsAlreadyStoppedException(manager);
        }
    }
}

/// <summary>
///     Represents an exception that occurs when a Bluetooth manager stops unexpectedly.
/// </summary>
/// <seealso cref="ManagerException" />
public class UnexpectedStopException : ManagerException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStopException"/> class.
    /// </summary>
    public UnexpectedStopException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStopException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public UnexpectedStopException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStopException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public UnexpectedStopException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnexpectedStopException"/> class.
    /// </summary>
    /// <param name="manager">The Bluetooth manager associated with the exception.</param>
    /// <param name="message">A message that describes the error.</param>
    /// <param name="innerException">The inner exception that caused the current exception, if any.</param>
    public UnexpectedStopException(
        IBluetoothManager manager,
        string message = "Unexpected stop",
        Exception? innerException = null)
        : base(manager, message, innerException)
    {
    }
}
