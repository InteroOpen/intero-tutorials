using Intero.Events;

using UnityEngine;

public class DebugListenerTutorial8 : MonoBehaviour, IListenerOSC
{
    void IListenerOSC.OnOSCClientConnectedEvent(OSCClientConnectedEvent connectedEvent)
    {
       // print("OnOSCClientConnectedEvent " + connectedEvent);
    }

    void IListenerOSC.OnOSCClientDisconnectedEvent(OSCClientDisconnectedEvent connectedEvent)
    {
        throw new System.NotImplementedException();
    }

    void IListenerOSC.OnOSCErgDataEvent(OSCErgDataEvent ergEvent)
    {
        //print("OnOSCErgDataEvent " + ergEvent.ergData);

    }

    void IListenerOSC.OnOSCMessageEvent(OSCMessageEvent messageEvent)
    {
     //   print("OnOSCMessageEvent " + messageEvent.msgReceive);

    }

    void IListenerOSC.OnOSCStartWorkoutDataEvent(OSCStartWorkoutEvent startWorkoutEvent)
    {
        //throw new System.NotImplementedException();
    }

    void IListenerOSC.OnOSCStrokeDataEvent(OSCStrokeDataEvent strokeEvent)
    {
      //  print("OnOSCStrokeDataEvent " + strokeEvent.strokeData);

    }

    // Start is called before the first frame update
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener((IListenerOSC)this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
