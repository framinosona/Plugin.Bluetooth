namespace Plugin.Bluetooth.EventArgs;

public class CharacteristicListChangedEventArgs : ItemListChangedEventArgs<IBluetoothCharacteristic>
{
    public CharacteristicListChangedEventArgs(NotifyCollectionChangedEventArgs args) : base(args)
    {
    }

    public CharacteristicListChangedEventArgs(IEnumerable<IBluetoothCharacteristic>? addedItems, IEnumerable<IBluetoothCharacteristic>? removedItems) : base(addedItems, removedItems)
    {
    }
}

public class CharacteristicsAddedEventArgs(IEnumerable<IBluetoothCharacteristic> items) : ItemsChangedEventArgs<IBluetoothCharacteristic>(items);

public class CharacteristicsRemovedEventArgs(IEnumerable<IBluetoothCharacteristic> items) : ItemsChangedEventArgs<IBluetoothCharacteristic>(items);
