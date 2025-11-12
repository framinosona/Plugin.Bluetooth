using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothService : BaseBluetoothService, CbPeripheralProxy.ICbServiceDelegate
{
    public CBService NativeService { get; }

    public BluetoothService(IBluetoothDevice device, Guid serviceUuid, CBService nativeService) : base(device, serviceUuid)
    {
        NativeService = nativeService;
    }

    #region BaseBluetoothService

    protected async override ValueTask NativeCharacteristicsExplorationAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region CbPeripheralProxy.ICbServiceDelegate
    public void DiscoveredIncludedService(NSError? error, CBService service)
    {
        throw new NotImplementedException();
    }

    public void DiscoveredCharacteristics(NSError? error, CBService service)
    {
        throw new NotImplementedException();
    }

    public CbPeripheralProxy.ICbCharacteristicDelegate GetCharacteristic(CBCharacteristic? characteristic)
    {
        throw new NotImplementedException();
    }
    #endregion
}
