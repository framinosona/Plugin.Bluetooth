namespace Plugin.Bluetooth.Abstractions;

/// <summary>
///     Event arguments for when an advertisement is received.
/// </summary>
public class AdvertisementReceivedEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AdvertisementReceivedEventArgs"/> class.
    /// </summary>
    /// <param name="advertisement">The advertisement data received.</param>
    public AdvertisementReceivedEventArgs(IBluetoothAdvertisement advertisement)
    {
        ArgumentNullException.ThrowIfNull(advertisement);

        Advertisement = advertisement;
    }

    /// <summary>
    ///     Gets the advertisement data received.
    /// </summary>
    public IBluetoothAdvertisement Advertisement { get; }

}
