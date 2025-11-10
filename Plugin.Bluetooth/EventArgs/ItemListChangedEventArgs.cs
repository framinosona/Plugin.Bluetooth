namespace Plugin.Bluetooth.EventArgs;

public class ItemListChangedEventArgs<T> : System.EventArgs
{
    protected ItemListChangedEventArgs(NotifyCollectionChangedEventArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        AddedItems = args.NewItems?.Cast<T>() ?? [];
        RemovedItems = args.OldItems?.Cast<T>() ?? [];
    }

    protected ItemListChangedEventArgs(IEnumerable<T>? addedItems, IEnumerable<T>? removedItems)
    {
        AddedItems = addedItems;
        RemovedItems = removedItems;
    }

    public IEnumerable<T>? AddedItems { get; }

    public IEnumerable<T>? RemovedItems { get; }
}
