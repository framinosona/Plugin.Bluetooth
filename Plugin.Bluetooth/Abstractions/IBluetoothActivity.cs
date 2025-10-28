using System.ComponentModel;

namespace Plugin.Bluetooth.Abstractions;

/// <summary>
/// Interface for managing a Bluetooth activity's lifecycle and state.
/// </summary>
public interface IBluetoothActivity : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether the Bluetooth activity is actively running.
    /// </summary>
    bool IsRunning { get; }

    ValueTask InitializeIfNeededAsync();

    #region Start

    /// <summary>
    /// Gets a value indicating whether the Bluetooth activity is starting.
    /// </summary>
    bool IsStarting { get; }

    event EventHandler Starting;

    event EventHandler Started;

    /// <summary>
    /// Asynchronously starts the Bluetooth activity with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout. Defaults to null, indicating no timeout.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    /// <remarks>Ensures that the Bluetooth activity is initialized and ready for use.</remarks>
    Task StartAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously starts the Bluetooth activity if it is not already running, with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout. Defaults to null, indicating no timeout.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    /// <remarks>Checks if the Bluetooth activity is already running before attempting to start it.</remarks>
    Task StartIfNeededAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);

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
    /// <param name="timeout">An optional timeout. Defaults to null, indicating no timeout.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    /// <remarks>Ensures that the Bluetooth manager and its resources are safely released.</remarks>
    Task StopAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously stops the Bluetooth manager if it is running, with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout. Defaults to null, indicating no timeout.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    /// <remarks>Checks if the Bluetooth manager is running before attempting to stop it.</remarks>
    Task StopIfNeededAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    #endregion


    /// <summary>
    /// Asynchronously restarts the Bluetooth manager with an optional timeout.
    /// </summary>
    /// <param name="timeout">An optional timeout. Defaults to null, indicating no timeout.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous restart operation.</returns>
    /// <remarks>Combines a call to <see cref="StopAsync" /> followed by <see cref="StartAsync" /> to reset the manager.</remarks>
    Task RestartAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);
}
