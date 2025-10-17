using System.ComponentModel;

namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface for managing Bluetooth operations and state.
/// </summary>
public interface IBluetoothManager : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether Bluetooth is currently enabled on the device.
    /// </summary>
    bool IsBluetoothOn { get; }

    /// <summary>
    /// Gets a value indicating whether the Bluetooth manager is actively running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Asynchronously initializes the Bluetooth manager. Might trigger permission popups.
    /// </summary>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    ValueTask InitializeAsync();

    #region Start

    /// <summary>
    /// Gets a value indicating whether the Bluetooth manager is starting.
    /// </summary>
    bool IsStarting { get; }

    event EventHandler Starting;

    event EventHandler Started;

    /// <summary>
    /// Asynchronously starts the Bluetooth manager with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout in milliseconds. Defaults to -1, indicating no timeout.</param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    /// <remarks>Ensures that the Bluetooth manager is initialized and ready for use.</remarks>
    Task StartAsync(int timeout = -1);

    /// <summary>
    /// Asynchronously starts the Bluetooth manager if it is not already running, with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout in milliseconds. Defaults to -1, indicating no timeout.</param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    /// <remarks>Checks if the Bluetooth manager is already running before attempting to start it.</remarks>
    Task StartIfNeededAsync(int timeout = -1);

    #endregion

    #region Stop

    /// <summary>
    /// Gets a value indicating whether the Bluetooth manager is stopping.
    /// </summary>
    bool IsStopping { get; }

    event EventHandler Stopping;

    event EventHandler Stopped;

    /// <summary>
    /// Asynchronously stops the Bluetooth manager with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout in milliseconds. Defaults to -1, indicating no timeout.</param>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    /// <remarks>Ensures that the Bluetooth manager and its resources are safely released.</remarks>
    Task StopAsync(int timeout = -1);

    /// <summary>
    /// Asynchronously stops the Bluetooth manager if it is running, with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout in milliseconds. Defaults to -1, indicating no timeout.</param>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    /// <remarks>Checks if the Bluetooth manager is running before attempting to stop it.</remarks>
    Task StopIfNeededAsync(int timeout = -1);

    #endregion


    /// <summary>
    /// Asynchronously restarts the Bluetooth manager with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout in milliseconds. Defaults to -1, indicating no timeout.</param>
    /// <returns>A task that represents the asynchronous restart operation.</returns>
    /// <remarks>Combines a call to <see cref="StopAsync" /> followed by <see cref="StartAsync" /> to reset the manager.</remarks>
    Task RestartAsync(int timeout = -1);
}
