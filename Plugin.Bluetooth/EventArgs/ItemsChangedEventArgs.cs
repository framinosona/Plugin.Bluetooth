namespace Plugin.Bluetooth.EventArgs;

public class ItemsChangedEventArgs<T> : System.EventArgs
{
    public ItemsChangedEventArgs(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        Items = items;
    }

    public IEnumerable<T> Items { get; }
}