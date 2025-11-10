using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    public async ValueTask ClearServicesAsync()
    {
        foreach (var service in Services)
        {
            await service.ClearCharacteristicsAsync().ConfigureAwait(false);
            await service.DisposeAsync().ConfigureAwait(false);
        }

        lock (Services)
        {
            Services.Clear();
        }
    }
}
