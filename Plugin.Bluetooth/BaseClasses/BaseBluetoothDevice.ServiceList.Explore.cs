namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    public bool IsExploringServices
    {
        get => GetValue(false);
        private set => SetValue(value);
    }

    private TaskCompletionSource? ServicesExplorationTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    protected void OnServicesExplorationSucceeded<TNativeServiceType>(IList<TNativeServiceType> services, Func<TNativeServiceType, IBluetoothService> fromInputTypeToOutputTypeConversion, Func<TNativeServiceType, IBluetoothService, bool> areRepresentingTheSameObject)
    {
        Services.UpdateFrom(services, areRepresentingTheSameObject, fromInputTypeToOutputTypeConversion);

        // Attempt to dispatch success to the TaskCompletionSource
        var success = ServicesExplorationTcs?.TrySetResult() ?? false;
        if (success)
        {
            return;
        }

        // Else throw an exception
        throw new UnexpectedServiceExplorationException(this);
    }

    protected void OnServicesExplorationFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = ServicesExplorationTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    /// <inheritdoc/>
    protected abstract ValueTask NativeServicesExplorationAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public async Task ExploreServicesAsync(bool clearBeforeExploring = false, bool exploreCharacteristicsToo = false, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Check if services have already been explored
        if (Services.Any() && !clearBeforeExploring)
        {
            return;
        }

        // Prevents multiple calls to ReadValueAsync, if already exploring, we merge the calls
        if (ServicesExplorationTcs is { Task.IsCompleted: false })
        {
            await ServicesExplorationTcs.Task.ConfigureAwait(false);
            return;
        }

        ServicesExplorationTcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously); // Reset the TCS
        IsExploringServices = true; // Set the flag to true

        try // try-catch to dispatch exceptions rising from start
        {

            // Check if services need to be cleaned
            if (Services.Any() && clearBeforeExploring)
            {
                await ClearServicesAsync().ConfigureAwait(false);
            }

            // Ensure Device is Connected
            DeviceNotConnectedException.ThrowIfNotConnected(this);

            await NativeServicesExplorationAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false); // actual service exploration native call
        }
        catch (Exception e)
        {
            OnServicesExplorationFailed(e); // if exception is thrown during start, we trigger the failure
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnServicesExplorationSucceeded to be called
            await ServicesExplorationTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);

            if (exploreCharacteristicsToo) // EXPLORE CHARACTERISTICS IF NEEDED
            {
                foreach (var service in Services.ToList())
                {
                    await service.ExploreCharacteristicsAsync(false, nativeOptions, timeout, cancellationToken).ConfigureAwait(false);
                }
            }
        }
        finally
        {
            IsExploringServices = false; // Reset the flag
            ServicesExplorationTcs = null;
        }
    }
}
