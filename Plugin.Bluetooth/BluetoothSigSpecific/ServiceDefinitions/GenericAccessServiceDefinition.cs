namespace Plugin.Bluetooth.BluetoothSigSpecific.ServiceDefinitions;

/// <summary>
///     Generic Access Service | generic_access_service | 1800
/// </summary>
[ServiceDefinition]
public static class GenericAccessServiceDefinition
{
    // https:// www.bluetooth.com/specifications/gatt/characteristics/
    public readonly static string Name = "Generic Access Service";

    public readonly static Guid Id = new Guid($"00001800{BluetoothSigConstants.StandardGuidExtension}");

    public readonly static CharacteristicAccessService<string> DeviceName = CharacteristicAccessServiceFactory.CreateForUtf8String(new Guid($"00002a00{BluetoothSigConstants.StandardGuidExtension}"), "Device Name");

    public readonly static CharacteristicAccessService<byte[]> Appearance = CharacteristicAccessServiceFactory.CreateForByteArray(new Guid($"00002a01{BluetoothSigConstants.StandardGuidExtension}"), "Appearance");

    public readonly static CharacteristicAccessService<byte[]> PeripheralPrivacyFlag = CharacteristicAccessServiceFactory.CreateForByteArray(new Guid($"00002a02{BluetoothSigConstants.StandardGuidExtension}"), "Peripheral Privacy Flag");

    public readonly static CharacteristicAccessService<byte[]> ReconnectionAddress = CharacteristicAccessServiceFactory.CreateForByteArray(new Guid($"00002a03{BluetoothSigConstants.StandardGuidExtension}"), "Reconnection Address");

    public readonly static CharacteristicAccessService<byte[]> PeripheralPreferredConnectionParameters = CharacteristicAccessServiceFactory.CreateForByteArray(new Guid($"00002a04{BluetoothSigConstants.StandardGuidExtension}"), "Peripheral Preferred Connection Parameters");

}
