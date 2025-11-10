using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    public event EventHandler<ServicesAddedEventArgs>? ServicesAdded;

    public event EventHandler<ServicesRemovedEventArgs>? ServicesRemoved;

    public event EventHandler<ServiceListChangedEventArgs>? ServiceListChanged;

    private ObservableCollection<IBluetoothService>? _services;

    protected ObservableCollection<IBluetoothService> Services
    {
        get
        {
            if (_services == null)
            {
                _services = [];
                _services.CollectionChanged += ServicesOnCollectionChanged;
            }

            return _services;
        }
    }

    private void ServicesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs ea)
    {
        var listChangedEventArgs = new ServiceListChangedEventArgs(ea);
        if (listChangedEventArgs.AddedItems != null)
        {
            ServicesAdded?.Invoke(this, new ServicesAddedEventArgs(listChangedEventArgs.AddedItems));
        }
        if (listChangedEventArgs.RemovedItems != null)
        {
            ServicesRemoved?.Invoke(this, new ServicesRemovedEventArgs(listChangedEventArgs.RemovedItems));
        }
        ServiceListChanged?.Invoke(this, listChangedEventArgs);
    }
}
