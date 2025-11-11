
namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    /// <inheritdoc/>
    public bool HasService(Guid id)
    {
        return HasService(service => service.Id == id);
    }

    /// <inheritdoc/>
    public bool HasService(Func<IBluetoothService, bool> filter)
    {
        return Services.Any(filter);
    }

    /// <inheritdoc/>
    public ValueTask<bool> HasServiceAsync(Guid id, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        return HasServiceAsync(service => service.Id == id, nativeOptions, timeout, cancellationToken);
    }

    /// <inheritdoc/>
    public async ValueTask<bool> HasServiceAsync(Func<IBluetoothService, bool> filter, Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        if (HasService(filter))
        {
            return true;
        }
        await ExploreServicesAsync(false, true, nativeOptions, timeout, cancellationToken).ConfigureAwait(false);

        return HasService(filter);
    }

}
