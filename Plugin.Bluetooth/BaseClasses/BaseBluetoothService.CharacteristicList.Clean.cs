using Plugin.Bluetooth.EventArgs;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothService
{
    public async ValueTask ClearCharacteristicsAsync()
    {
        foreach (var characteristic in Characteristics)
        {
            await characteristic.DisposeAsync().ConfigureAwait(false);
        }

        lock (Characteristics)
        {
            Characteristics.Clear();
        }
    }
}
