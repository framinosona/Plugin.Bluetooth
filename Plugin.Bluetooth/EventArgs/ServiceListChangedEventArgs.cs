namespace Plugin.Bluetooth.EventArgs;

public class ServiceListChangedEventArgs : ItemListChangedEventArgs<IBluetoothService>
{
    public ServiceListChangedEventArgs(NotifyCollectionChangedEventArgs args) : base(args)
    {
    }

    public ServiceListChangedEventArgs(IEnumerable<IBluetoothService>? addedItems, IEnumerable<IBluetoothService>? removedItems) : base(addedItems, removedItems)
    {
    }
}

public class ServicesAddedEventArgs(IEnumerable<IBluetoothService> items) : ItemsChangedEventArgs<IBluetoothService>(items);

public class ServicesRemovedEventArgs(IEnumerable<IBluetoothService> items) : ItemsChangedEventArgs<IBluetoothService>(items);
