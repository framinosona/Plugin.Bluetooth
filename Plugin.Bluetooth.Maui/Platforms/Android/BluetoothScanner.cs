using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

public partial class BluetoothScanner : BaseBluetoothScanner, ScanCallbackProxy.IScanner
{
    public ScanCallbackProxy ScanCallbackProxy { get; }

    public BluetoothScanner()
    {
        // Callbacks
        ScanCallbackProxy = new ScanCallbackProxy(this);
    }

    #region BaseBluetoothScanner

    protected override void NativeRefreshIsBluetoothOn()
    {
        throw new NotImplementedException();
    }

    protected override void NativeRefreshIsRunning()
    {
        throw new NotImplementedException();
    }

    protected override void NativeStart(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    protected override void NativeStop()
    {
        throw new NotImplementedException();
    }

    protected async override ValueTask NativeInitializeAsync(Dictionary<string, object>? nativeOptions = null)
    {
        throw new NotImplementedException();
    }

    protected override IBluetoothDevice NativeCreateDevice(IBluetoothAdvertisement advertisement)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region ScanCallbackProxy.IScanner

    public virtual void OnScanFailed(ScanFailure errorCode)
    {
        throw new NotImplementedException();
    }

    public virtual void OnScanResult(ScanCallbackType callbackType, IEnumerable<ScanResult> results)
    {
        throw new NotImplementedException();
    }

    public virtual void OnScanResult(ScanCallbackType callbackType, ScanResult results)
    {
        throw new NotImplementedException();
    }

    #endregion

}
