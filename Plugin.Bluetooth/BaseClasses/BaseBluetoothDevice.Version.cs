using Plugin.Bluetooth.BluetoothSigSpecific.ServiceDefinitions;

namespace Plugin.Bluetooth.BaseClasses;

public abstract partial class BaseBluetoothDevice : BaseBindableObject, IBluetoothDevice
{
    /// <inheritdocs/>
    public Version? FirmwareVersion
    {
        get => GetValue<Version?>(null);
        protected set => SetValue(value);
    }

    /// <inheritdocs/>
    public async Task<Version> ReadFirmwareVersionAsync()
    {
        FirmwareVersion = await DeviceInformationServiceDefinition.FirmwareRevision.ReadAsync(this, true).ConfigureAwait(false);
        return FirmwareVersion;
    }

    /// <inheritdocs/>
    public Version? SoftwareVersion
    {
        get => GetValue<Version?>(null);
        protected set => SetValue(value);
    }

    /// <inheritdocs/>
    public async Task<Version> ReadSoftwareVersionAsync()
    {
        SoftwareVersion = await DeviceInformationServiceDefinition.SoftwareRevision.ReadAsync(this, true).ConfigureAwait(false);
        return SoftwareVersion;
    }

    /// <inheritdocs/>
    public string? HardwareVersion
    {
        get => GetValue<string?>(null);
        protected set => SetValue(value);
    }

    /// <inheritdocs/>
    public async Task<string> ReadHardwareVersionAsync()
    {
        HardwareVersion = await DeviceInformationServiceDefinition.HardwareRevision.ReadAsync(this, true).ConfigureAwait(false);
        return HardwareVersion;
    }

    /// <inheritdocs/>
    public async Task ReadVersionsAsync()
    {
        await ReadFirmwareVersionAsync().ConfigureAwait(false);
        await ReadSoftwareVersionAsync().ConfigureAwait(false);
        await ReadHardwareVersionAsync().ConfigureAwait(false);
    }
}
