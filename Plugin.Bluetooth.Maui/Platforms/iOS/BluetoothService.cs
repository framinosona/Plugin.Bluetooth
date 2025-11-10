namespace Plugin.Bluetooth.Maui;

public partial class BluetoothService : BaseBluetoothService
{

    public BluetoothService(IBluetoothDevice device, Guid serviceUuid) : base(device, serviceUuid)
    {
    }

    #region BaseBluetoothService

    protected async override ValueTask NativeCharacteristicsExplorationAsync(Dictionary<string, object>? nativeOptions = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

}
