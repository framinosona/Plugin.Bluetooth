using Plugin.Bluetooth.BluetoothSigSpecific.ServiceDefinitions;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    public double? BatteryLevelPercent
    {
        get => GetValue<double?>(null);
        private set => SetValue(value);
    }

    public async Task<double?> ReadBatteryLevelAsync()
    {
        if (await BatteryServiceDefinition.BatteryLevel.CanReadAsync(this).ConfigureAwait(false))
        {
            var batteryLevel = await BatteryServiceDefinition.BatteryLevel.ReadAsync(this).ConfigureAwait(false);
            BatteryLevelPercent = (double) batteryLevel / 100;
        }

        return BatteryLevelPercent;
    }
}
