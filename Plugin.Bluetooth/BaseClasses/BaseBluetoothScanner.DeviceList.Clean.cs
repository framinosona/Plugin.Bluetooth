namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothScanner
{
    public async ValueTask CleanAsync(IEnumerable<IBluetoothDevice> devices)
    {
        foreach (var device in devices ?? [])
        {
            await CleanAsync(device).ConfigureAwait(false);
        }
    }

    public async ValueTask CleanAsync(IBluetoothDevice? device)
    {
        if (device == null)
        {
            return;
        }

        if (device.IsConnected)
        {
            await device.DisconnectAsync().ConfigureAwait(false);
        }

        lock (Devices)
        {
            Devices.Remove(device);
        }
    }

    public ValueTask CleanAsync(string deviceId)
    {
        return CleanAsync(GetDeviceOrDefault(deviceId));
    }

    public async ValueTask CleanAsync()
    {
        var devices = GetDevices().ToList();
        foreach (var device in devices)
        {
            await CleanAsync(device).ConfigureAwait(false);
        }
    }
}
