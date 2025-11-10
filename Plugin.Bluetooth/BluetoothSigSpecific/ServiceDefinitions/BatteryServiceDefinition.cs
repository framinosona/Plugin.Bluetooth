namespace Plugin.Bluetooth.BluetoothSigSpecific.ServiceDefinitions;

/// <summary>
///     Battery Service | battery_service | 180F
/// </summary>
[ServiceDefinition]
public static class BatteryServiceDefinition
{
    // https:// www.bluetooth.com/specifications/gatt/characteristics/
    public readonly static string Name = "Battery Service";

    public readonly static Guid Id = new Guid($"0000180F{BluetoothSigConstants.StandardGuidExtension}");

    public readonly static CharacteristicAccessService<sbyte> BatteryLevel = CharacteristicAccessServiceFactory.CreateForSByte(new Guid($"00002a19{BluetoothSigConstants.StandardGuidExtension}"), "Battery Level");
}
