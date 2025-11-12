using Plugin.Bluetooth.Maui.PlatformSpecific;

namespace Plugin.Bluetooth.Maui;

/// <inheritdoc />
public abstract partial class BluetoothActivity : BaseBluetoothActivity, RadioProxy.IRadioProxyDelegate, BluetoothAdapterProxy.IBluetoothAdapterProxyDelegate
{

    public BluetoothAdapterProxy? BluetoothAdapterProxy { get; protected set; }

    public RadioProxy? RadioProxy { get; protected set; }

    #region RadioProxy.IRadioProxyDelegate

    public void OnRadioStateChanged(RadioState senderState)
    {
        throw new NotImplementedException();
    }

    #endregion

}
