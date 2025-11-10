using System.Globalization;
using System.Text;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    public Task WaitForNameToChangeAsync(TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        return WaitForPropertyToChange<string>(nameof(AdvertisedName), timeout, cancellationToken).AsTask();
    }

    public string DebugName
    {
        get => GetValue<string>("");
        protected set => SetValue(value);
    }

    public string AdvertisedName
    {
        get => GetValue<string>("");
        private set
        {
            if (!SetValue(value))
            {
                return;
            }

            UpdateName();
        }
    }

    public string Name
    {
        get => GetValue("");
        protected set => SetValue(value);
    }

    public string CachedName
    {
        get => GetValue<string>("");
        protected set
        {
            if (!SetValue(value))
            {
                return;
            }

            UpdateName();
        }
    }

    private void UpdateName()
    {
        var nameSb = new StringBuilder();
        var debugNameSb = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(AdvertisedName))
        {
            nameSb.Append(AdvertisedName);
            debugNameSb.Append(AdvertisedName);
            if (!string.IsNullOrWhiteSpace(CachedName) && AdvertisedName != CachedName)
            {
                debugNameSb.Append(CultureInfo.InvariantCulture, $" (cached: {CachedName})");
            }
        }
        else if (!string.IsNullOrWhiteSpace(CachedName))
        {
            nameSb.Append(CachedName);
            debugNameSb.Append(CachedName);
        }

        Name = nameSb.ToString(); // Name is the name that is shown in the UI (AdvertisedName OR CachedName)
        DebugName = debugNameSb.ToString(); // DebugName is the name that is shown in the logs (AdvertisedName + CachedName)
    }
}
