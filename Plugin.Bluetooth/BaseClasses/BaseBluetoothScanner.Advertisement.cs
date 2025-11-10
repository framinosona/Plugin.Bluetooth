using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothScanner
{
    public bool IgnoreDuplicateAdvertisements
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public Func<IBluetoothAdvertisement, bool> AdvertisementFilter { get; set; } = DefaultAdvertisementFilter;

    private static bool DefaultAdvertisementFilter(IBluetoothAdvertisement arg)
    {
        return false;
    }

    public event EventHandler<AdvertisementReceivedEventArgs>? AdvertisementReceived;

    protected void OnAdvertisementReceived(IBluetoothAdvertisement advertisement)
    {
        ArgumentNullException.ThrowIfNull(advertisement, nameof(advertisement));

        // Filter
        if (!AdvertisementFilter.Invoke(advertisement))
        {
            return;
        }

        // Throw event
        AdvertisementReceived?.Invoke(this, new AdvertisementReceivedEventArgs(advertisement));

        // Group by device
        var device = GetDeviceOrDefault(advertisement.BluetoothAddress) ?? AddDeviceFromAdvertisement(advertisement);

        // Filter out duplicates if needed
        if (IgnoreDuplicateAdvertisements && device.LastAdvertisement != null && device.LastAdvertisement.Equals(advertisement))
        {
            return;
        }

        // Process advertisement infos
        device.OnAdvertisementReceived(advertisement);
    }


    // Mainly for android Batch advertisement
    protected void OnAdvertisementsReceived(IEnumerable<IBluetoothAdvertisement> advertisements)
    {
        // Filter
        var filteredAdvertisements = advertisements.Where(advertisement => AdvertisementFilter.Invoke(advertisement)).ToList();

        // Throw event
        foreach (var advertisement in filteredAdvertisements)
        {
            AdvertisementReceived?.Invoke(this, new AdvertisementReceivedEventArgs(advertisement));
        }

        // Group by device
        var groupedAdvertisements = filteredAdvertisements.GroupBy(advertisement => advertisement.BluetoothAddress);
        foreach (var advertisementGroup in groupedAdvertisements)
        {
            // Get or create device
            var device = GetDeviceOrDefault(advertisementGroup.Key) ?? AddDeviceFromAdvertisement(advertisementGroup.First());

            // Process advertisement infos
            foreach (var advertisement in advertisementGroup)
            {
                device.OnAdvertisementReceived(advertisement);
            }
        }
    }
}
