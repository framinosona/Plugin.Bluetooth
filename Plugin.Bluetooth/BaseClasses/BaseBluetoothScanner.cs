
using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth;

public abstract partial class BaseBluetoothScanner : BaseBluetoothManager, IBluetoothScanner
{
    public abstract IBluetoothCharacteristicAccessServicesRepository KnownServicesAndCharacteristicsRepository { get; }
    public abstract Func<IBluetoothAdvertisement, bool>? AdvertisementFilter { get; set; }
    public abstract bool IsRunning { get; }
    public abstract bool IsStarting { get; }
    public abstract bool IsStopping { get; }

    public abstract event EventHandler<AdvertisementReceivedEventArgs> AdvertisementReceived;
    public abstract event EventHandler<DeviceListChangedEventArgs> DeviceListChanged;
    public abstract event EventHandler Starting;
    public abstract event EventHandler Started;
    public abstract event EventHandler Stopping;
    public abstract event EventHandler Stopped;

    public abstract ValueTask CleanAsync(IEnumerable<IBluetoothDevice> devices);
    public abstract ValueTask CleanAsync(IBluetoothDevice? device);
    public abstract ValueTask CleanAsync(string deviceId);
    public abstract ValueTask CleanAsync();
    public abstract IBluetoothDevice? GetClosestDeviceOrDefault();
    public abstract IBluetoothDevice? GetDeviceOrDefault(Func<IBluetoothDevice, bool> filter);
    public abstract IBluetoothDevice? GetDeviceOrDefault(string id);
    public abstract IEnumerable<IBluetoothDevice> GetDevices(Func<IBluetoothDevice, bool>? filter = null);
    public abstract ValueTask InitializeIfNeededAsync();
    public abstract void ResetAdvertisementFilter();
    public abstract Task RestartAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    public abstract Task StartAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    public abstract Task StartIfNeededAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    public abstract Task StopAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    public abstract Task StopIfNeededAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    public abstract Task<IBluetoothDevice> WaitForDeviceToAppearAsync(string id, TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    public abstract Task<IBluetoothDevice> WaitForDeviceToAppearAsync(Func<IBluetoothDevice, bool> filter, TimeSpan? timeout = null, CancellationToken cancellationToken = default);
}
