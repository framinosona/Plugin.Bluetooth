namespace Plugin.Bluetooth.BaseClasses;

/// <inheritdoc cref="IBluetoothService" />
public abstract partial class BaseBluetoothService : BaseBindableObject, IBluetoothService
{
    protected BaseBluetoothService(IBluetoothDevice device, Guid serviceUuid)
    {
        Device = device;
        Id = serviceUuid;
        Name = Device.Scanner.KnownServicesAndCharacteristicsRepository.GetServiceName(serviceUuid);
    }

    public IBluetoothDevice Device { get; }

    public Guid Id { get; }

    public string Name { get;}

    protected async virtual ValueTask DisposeAsyncCore()
    {
        // Cancel any pending explorations
        CharacteristicsExplorationTcs?.TrySetCanceled();

        // Unsubscribe from event
        Characteristics.CollectionChanged -= CharacteristicsOnCollectionChanged;

        // Unsubscribe from other events
        CharacteristicListChanged = null;
        CharacteristicsAdded = null;
        CharacteristicsRemoved = null;

        await ClearCharacteristicsAsync().ConfigureAwait(false);
    }

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}
