using Intero.TCP;
using UnityEngine;
using Intero.Events;
using Intero.Common;
using UnityEngine.UI;

public class Server : MonoBehaviour, IListenerOSC
{
    InteroServerTCP server;
    public Text textOut;
    public Text[] textOutUser;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        textOut.text = "Server waitiing...\n";
        server = new InteroServerTCP();
        server.Serve(10, 8080);
        InteroEventManager.GetEventManager().AddListener((IListenerOSC)this);

    }
    public void StartWorkout()
    {
        // broadcast to all
        server.BroadcastToClients(OSCErgDataManager.StartWorkoutMessage(), -1);
    }

    void Update()
    {
        if (server != null) server.unityThread.Update();
    }
    void IListenerOSC.OnOSCClientConnectedEvent(OSCClientConnectedEvent connectedEvent)
    {
        print(name + " OnOSCClientConnectedEvent ");
        textOut.text += connectedEvent.socketSender.username + " connected ...\n";
    }

    void IListenerOSC.OnOSCErgDataEvent(OSCErgDataEvent ergEvent)
    {
        textOutUser[ergEvent.senderId].text =  name+" "+ ergEvent.socketSender.username + " "+ ergEvent.ergData;
       // print(name + " Ergata " + ergEvent.senderId);
        server.BroadcastToClients(ergEvent.socketSender.username, ergEvent.ergData, ergEvent.segment, ergEvent.senderId);
        // print(name + " " + ergEvent.ergData + " ss " + ergEvent.segment.index + " s " + ergEvent.segment.startDistance);
    }

    void IListenerOSC.OnOSCMessageEvent(OSCMessageEvent messageEvent)
    {
        print(name + " Got Message " + messageEvent.msgReceive.Address);
        // print(name + " Sending ergdata 2 ");
        // messageEvent.socketSender.Send(new ErgData());
        // connectedEvent.socketSender.Send()
        // server.BroadcastToClients()
    }

    void IListenerOSC.OnOSCStrokeDataEvent(OSCStrokeDataEvent strokeEvent)
    {
        print(name + " OnOSCStrokeDataEvent " + strokeEvent.strokeData);

    }
    void OnDestroy()
    {
        server.OnDestroy();
        Debug.Log("OnDestroy1");
    }

    void IListenerOSC.OnOSCStartWorkoutDataEvent(OSCStartWorkoutEvent startWorkoutEvent)
    {
        print(name + "  Start Workout!!! " );
    }
}
