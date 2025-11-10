namespace Plugin.Bluetooth.BaseClasses;

/// <inheritdoc cref="IBluetoothScanner" />
public abstract partial class BaseBluetoothScanner : BaseBluetoothActivity, IBluetoothScanner
{
    /// <summary>
    /// Initializes the Bluetooth scanner.
    /// </summary>
    protected async virtual ValueTask InitializeAsync(Dictionary<string, object>? options = null)
    {
        await KnownServicesAndCharacteristicsRepository.AddAllServiceDefinitionsInCurrentAssemblyAsync().ConfigureAwait(false);
        await NativeInitializeAsync(options).ConfigureAwait(false);
    }

    protected abstract ValueTask NativeInitializeAsync(Dictionary<string, object>? options = null);

    public IBluetoothCharacteristicAccessServicesRepository KnownServicesAndCharacteristicsRepository { get; } = new CharacteristicAccessServicesRepository();

    protected abstract IBluetoothDevice NativeCreateDevice(IBluetoothAdvertisement advertisement);

    protected virtual IBluetoothDevice AddDeviceFromAdvertisement(IBluetoothAdvertisement advertisement)
    {
        var device = NativeCreateDevice(advertisement);
        lock (Devices)
        {
            Devices.Add(device);
        }
        return device;
    }
}
