using Microsoft.Extensions.Logging;

using Plugin.Bluetooth.Abstractions;
using Plugin.Bluetooth.PlatformSpecific;

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

    protected override ValueTask RefreshNativeAdapterValuesAsync()
    {
        BluetoothAdapterProxy.Logger.RefreshValues();
        if (IsBluetoothOn != BluetoothAdapterProxy.BluetoothAdapter.IsEnabled)
        {
            IsBluetoothOn = BluetoothAdapterProxy.BluetoothAdapter.IsEnabled;
            OnBluetoothStateChanged(new BluetoothStateChangedEventArgs(IsBluetoothOn));
        }
        return ValueTask.CompletedTask;
    }
}
