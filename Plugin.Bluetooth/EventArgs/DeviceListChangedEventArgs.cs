namespace Plugin.Bluetooth.EventArgs;

public class DeviceListChangedEventArgs : ItemListChangedEventArgs<IBluetoothDevice>
{
    public DeviceListChangedEventArgs(NotifyCollectionChangedEventArgs args) : base(args)
    {
    }

    public DeviceListChangedEventArgs(IEnumerable<IBluetoothDevice>? addedItems, IEnumerable<IBluetoothDevice>? removedItems) : base(addedItems, removedItems)
    {
    }
}

public class DevicesAddedEventArgs(IEnumerable<IBluetoothDevice> items) : ItemsChangedEventArgs<IBluetoothDevice>(items);

public class DevicesRemovedEventArgs(IEnumerable<IBluetoothDevice> items) : ItemsChangedEventArgs<IBluetoothDevice>(items);
