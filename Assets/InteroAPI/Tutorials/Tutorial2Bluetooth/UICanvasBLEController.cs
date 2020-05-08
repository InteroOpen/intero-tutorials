using UnityEngine.UI;
using UnityEngine;
using Intero.Events;
using Intero.Common;

public class UICanvasBLEController : MonoBehaviour, IListenerBLE
{
    public Text textStatus;

    void IListenerBLE.OnBLEAvailableDevicesEvent(BLEAvailableDevicesEvent e)
    {
        textStatus.text = "Device List";
    }

    void IListenerBLE.OnBLEConnectedEvent(BLEConnectedEvent e)
    {
        textStatus.text = "BLE Connected";
    }

    void IListenerBLE.OnBLEDisconnectedEvent(BLEDisconnectedEvent e)
    {
        textStatus.text = "BLE Disconnected";

    }

    void IListenerBLE.OnBLEOffEvent(BLEOffEvent e)
    {
        textStatus.text = "BLE Off";
    }

    void IListenerBLE.OnBLEOnEvent(BLEOnEvent e)
    {
        textStatus.text = "BLE On";
    }


    // Update is called once per frame
    void Start()
    {
        textStatus.text = "Started BLE Listener";
        InteroEventManager.GetEventManager().AddListener(this);
    }
}
