using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothScanner
{
    public event EventHandler<DeviceListChangedEventArgs>? DeviceListChanged;

    public event EventHandler<DevicesAddedEventArgs>? DevicesAdded;

    public event EventHandler<DevicesRemovedEventArgs>? DevicesRemoved;

    private ObservableCollection<IBluetoothDevice>? _devices;

    protected ObservableCollection<IBluetoothDevice> Devices
    {
        get
        {
            if (_devices == null)
            {
                _devices = [];
                _devices.CollectionChanged += DevicesOnCollectionChanged;
            }

            return _devices;
        }
    }

    private void DevicesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs ea)
    {
        var listChangedEventArgs = new DeviceListChangedEventArgs(ea);
        if (listChangedEventArgs.AddedItems != null)
        {
            DevicesAdded?.Invoke(this, new DevicesAddedEventArgs(listChangedEventArgs.AddedItems));
        }
        if (listChangedEventArgs.RemovedItems != null)
        {
            DevicesRemoved?.Invoke(this, new DevicesRemovedEventArgs(listChangedEventArgs.RemovedItems));
        }
        DeviceListChanged?.Invoke(this, listChangedEventArgs);
    }
}
