namespace Plugin.Bluetooth.Maui;

public partial class BluetoothService : BaseBluetoothService
{
    public BluetoothGattService NativeService { get; }

    public BluetoothService(IBluetoothDevice device, Guid serviceUuid, BluetoothGattService nativeService) : base(device, serviceUuid)
    {
        NativeService = nativeService;
    }

    #region BaseBluetoothService

    protected async override ValueTask NativeCharacteristicsExplorationAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion
}
