namespace Plugin.Bluetooth.BluetoothSigSpecific.ServiceDefinitions;

/// <summary>
///     Generic Attribute Service | generic_attribute_service | 1801
/// </summary>
[ServiceDefinition]
public static class GenericAttributeServiceDefinition
{
    // https:// www.bluetooth.com/specifications/gatt/characteristics/
    public readonly static string Name = "Generic Attribute Service";

    public readonly static Guid Id = new Guid($"00001801{BluetoothSigConstants.StandardGuidExtension}");

    public readonly static CharacteristicAccessService<byte[]> ServiceChanged = CharacteristicAccessServiceFactory.CreateForByteArray(new Guid($"00002a05{BluetoothSigConstants.StandardGuidExtension}"), "Service Changed");

}
