using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public class BluetoothBroadcaster : BaseBluetoothBroadcaster, AdvertiseCallbackProxy.IBroadcaster, BluetoothGattServerCallbackProxy.IBluetoothGattServerCallbackProxyDelegate
{
    public BluetoothGattServerCallbackProxy? BluetoothGattServerCallbackProxy { get; protected set; }

    public AdvertiseCallbackProxy AdvertiseCallbackProxy { get; }

    public BluetoothBroadcaster()
    {
        AdvertiseCallbackProxy = new AdvertiseCallbackProxy(this);
    }

    private AdvertiseData? GetScanResponseData(Dictionary<string, object>? nativeOptions)
    {
        throw new NotImplementedException();
    }

    private AdvertiseData? GetAdvertiseData(Dictionary<string, object>? nativeOptions)
    {
        throw new NotImplementedException();
    }

    private AdvertiseSettings? GetSettings(Dictionary<string, object>? nativeOptions)
    {
        throw new NotImplementedException();
    }

    #region AdvertiseCallbackProxy.IBroadcaster

    public void OnStartSuccess(AdvertiseSettings? settingsInEffect)
    {
        throw new NotImplementedException();
    }

    public void OnStartFailure(AdvertiseFailure errorCode)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region BaseBluetoothBroadcaster

    protected override void NativeRefreshIsBluetoothOn()
    {
        IsBluetoothOn = BluetoothAdapterProxy.BluetoothAdapter.IsEnabled;
    }

    protected override void NativeRefreshIsRunning()
    {
        IsRunning = BluetoothAdapterProxy.BluetoothAdapter.IsDiscovering;
    }

    protected override void NativeStart(Dictionary<string, object>? nativeOptions = null)
    {
        var settings = GetSettings(nativeOptions);
        var advertiseData = GetAdvertiseData(nativeOptions);
        var scanResponse = GetScanResponseData(nativeOptions);
        BluetoothLeAdvertiserProxy.BluetoothLeAdvertiser.StartAdvertising(settings, advertiseData, scanResponse, AdvertiseCallbackProxy);
        BluetoothGattServerCallbackProxy = new BluetoothGattServerCallbackProxy(this);
    }

    protected override void NativeStop()
    {
        BluetoothLeAdvertiserProxy.BluetoothLeAdvertiser.StopAdvertising(AdvertiseCallbackProxy);
        BluetoothGattServerCallbackProxy?.Dispose();
        BluetoothGattServerCallbackProxy = null;
    }


    protected async override ValueTask NativeInitializeAsync(Dictionary<string, object>? nativeOptions = null)
    {
        NativeRefreshIsBluetoothOn();
        await BluetoothPermissions.BluetoothPermission.RequestIfNeededAsync().ConfigureAwait(false);

        if (OperatingSystem.IsAndroidVersionAtLeast(31))
        {
            await BluetoothPermissions.BluetoothAdvertisePermission.RequestIfNeededAsync().ConfigureAwait(false);
        }
        else if (OperatingSystem.IsAndroidVersionAtLeast(29))
        {
            await BluetoothPermissions.FineLocationPermission.RequestIfNeededAsync().ConfigureAwait(false);

            // For using Bluetooth LE in Background
            await BluetoothPermissions.BackgroundLocationPermission.RequestIfNeededAsync().ConfigureAwait(false);
        }
        else
        {
            await BluetoothPermissions.CoarseLocationPermission.RequestIfNeededAsync().ConfigureAwait(false);
        }
    }

    public async override Task NativeSetAdvertisingDataAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region BluetoothGattServerCallbackProxy.IBluetoothGattServerCallbackProxyDelegate

    public void OnMtuChanged(int mtu)
    {
        throw new NotImplementedException();
    }

    public void OnExecuteWrite(int requestId, bool execute)
    {
        throw new NotImplementedException();
    }

    public void OnNotificationSent(GattStatus status)
    {
        throw new NotImplementedException();
    }

    public void OnPhyRead(GattStatus status, ScanSettingsPhy txPhy, ScanSettingsPhy rxPhy)
    {
        throw new NotImplementedException();
    }

    public void OnPhyUpdate(GattStatus status, ScanSettingsPhy txPhy, ScanSettingsPhy rxPhy)
    {
        throw new NotImplementedException();
    }

    public void OnServiceAdded(GattStatus status, BluetoothGattService? service)
    {
        throw new NotImplementedException();
    }

    public void OnConnectionStateChange(ProfileState status, ProfileState newState)
    {
        throw new NotImplementedException();
    }

    public void OnCharacteristicReadRequest(int requestId, int offset, BluetoothGattCharacteristic? characteristic)
    {
        throw new NotImplementedException();
    }

    public void OnCharacteristicWriteRequest(int requestId,
        BluetoothGattCharacteristic? characteristic,
        bool preparedWrite,
        bool responseNeeded,
        int offset,
        byte[]? value)
    {
        throw new NotImplementedException();
    }

    public void OnDescriptorReadRequest(int requestId, int offset, BluetoothGattDescriptor? descriptor)
    {
        throw new NotImplementedException();
    }

    public void OnDescriptorWriteRequest(int requestId,
        BluetoothGattDescriptor? descriptor,
        bool preparedWrite,
        bool responseNeeded,
        int offset,
        byte[]? value)
    {
        throw new NotImplementedException();
    }

    #endregion

}
