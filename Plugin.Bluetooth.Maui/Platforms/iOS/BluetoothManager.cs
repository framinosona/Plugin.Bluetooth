using Microsoft.Extensions.Logging;

using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth;

/// <inheritdoc />
public class BluetoothManager : BaseBluetoothManager
{
    /// <inheritdoc />
    public BluetoothManager(ILogger? logger = null)
        : base(logger)
    {
    }

    /// <inheritdoc />
    public override bool IsBluetoothOn { get; protected set; }

    /// <inheritdoc />
    public override ValueTask<IBluetoothBroadcaster> GetBroadcasterAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override ValueTask<IBluetoothScanner> GetScannerAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override ValueTask RefreshNativeAdapterValuesAsync()
    {
        return ValueTask.CompletedTask;
    }
}
