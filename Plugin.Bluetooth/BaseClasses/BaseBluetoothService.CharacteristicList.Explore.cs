namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothService
{
    public bool IsExploringCharacteristics
    {
        get => GetValue(false);
        private set => SetValue(value);
    }

    private TaskCompletionSource? CharacteristicsExplorationTcs
    {
        get => GetValue<TaskCompletionSource?>(null);
        set => SetValue(value);
    }

    protected void OnCharacteristicsExplorationSucceeded<TNativeCharacteristicType>(IList<TNativeCharacteristicType> characteristics, Func<TNativeCharacteristicType, IBluetoothCharacteristic> fromInputTypeToOutputTypeConversion, Func<TNativeCharacteristicType, IBluetoothCharacteristic, bool> areRepresentingTheSameObject)
    {
        Characteristics.UpdateFrom(characteristics, areRepresentingTheSameObject, fromInputTypeToOutputTypeConversion);

        // Attempt to dispatch success to the TaskCompletionSource
        var success = CharacteristicsExplorationTcs?.TrySetResult() ?? false;
        if (success)
        {
            return;
        }

        // Else throw an exception
        throw new UnexpectedCharacteristicExplorationException(this);
    }

    protected void OnCharacteristicsExplorationFailed(Exception e)
    {
        // Attempt to dispatch exception to the TaskCompletionSource
        var success = CharacteristicsExplorationTcs?.TrySetException(e) ?? false;
        if (success)
        {
            return;
        }

        // If the TaskCompletionSource was already completed, dispatch the exception to the listener
        BluetoothUnhandledExceptionListener.OnBluetoothUnhandledException(this, e);
    }

    /// <inheritdoc/>
    protected abstract ValueTask NativeCharacteristicsExplorationAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public async Task ExploreCharacteristicsAsync(bool clearBeforeExploring = false, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        // Check if characteristics have already been explored
        if (Characteristics.Any() && !clearBeforeExploring)
        {
            return;
        }

        // Prevents multiple calls to ReadValueAsync, if already exploring, we merge the calls
        if (CharacteristicsExplorationTcs is { Task.IsCompleted: false })
        {
            await CharacteristicsExplorationTcs.Task.ConfigureAwait(false);
            return;
        }

        CharacteristicsExplorationTcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously); // Reset the TCS
        IsExploringCharacteristics = true; // Set the flag to true

        try // try-catch to dispatch exceptions rising from start
        {

            // Check if characteristics need to be cleaned
            if (Characteristics.Any() && clearBeforeExploring)
            {
                await ClearCharacteristicsAsync().ConfigureAwait(false);
            }

            // Ensure Device is Connected
            DeviceNotConnectedException.ThrowIfNotConnected(this.Device);

            await NativeCharacteristicsExplorationAsync(nativeOptions, timeout, cancellationToken).ConfigureAwait(false); // actual characteristic exploration native call
        }
        catch (Exception e)
        {
            OnCharacteristicsExplorationFailed(e); // if exception is thrown during start, we trigger the failure
        }

        // try-finally to ensure disposal and release of resources
        try
        {
            // Wait for OnCharacteristicsExplorationSucceeded to be called
            await CharacteristicsExplorationTcs.Task.WaitBetterAsync(timeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            IsExploringCharacteristics = false; // Reset the flag
            CharacteristicsExplorationTcs = null;
        }
    }
}
