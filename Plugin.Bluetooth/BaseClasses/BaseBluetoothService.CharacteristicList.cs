using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothService
{
    public event EventHandler<CharacteristicsAddedEventArgs>? CharacteristicsAdded;

    public event EventHandler<CharacteristicsRemovedEventArgs>? CharacteristicsRemoved;

    public event EventHandler<CharacteristicListChangedEventArgs>? CharacteristicListChanged;

    private ObservableCollection<IBluetoothCharacteristic>? _characteristics;

    protected ObservableCollection<IBluetoothCharacteristic> Characteristics
    {
        get
        {
            if (_characteristics == null)
            {
                _characteristics = [];
                _characteristics.CollectionChanged += CharacteristicsOnCollectionChanged;
            }

            return _characteristics;
        }
    }

    private void CharacteristicsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs ea)
    {
        var listChangedEventArgs = new CharacteristicListChangedEventArgs(ea);
        if (listChangedEventArgs.AddedItems != null)
        {
            CharacteristicsAdded?.Invoke(this, new CharacteristicsAddedEventArgs(listChangedEventArgs.AddedItems));
        }
        if (listChangedEventArgs.RemovedItems != null)
        {
            CharacteristicsRemoved?.Invoke(this, new CharacteristicsRemovedEventArgs(listChangedEventArgs.RemovedItems));
        }
        CharacteristicListChanged?.Invoke(this, listChangedEventArgs);
    }
}
