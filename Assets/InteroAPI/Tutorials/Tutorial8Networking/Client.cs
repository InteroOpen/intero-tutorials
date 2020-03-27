using Intero.TCP;
using UnityEngine;
using Intero.Events;
using Intero.Common;
using UnityEngine.UI;
using Intero.Workouts;

public class Client : MonoBehaviour, IListenerOSC
{
    public InteroClientTCP client;
    public string name;
    public WorkoutManager workoutManager;
    public LeaderboardController leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        client = new InteroClientTCP();
        client.Connect(8080);
        client.username = name;
        //  InvokeRepeating("genErgdata", 2.0f, 0.3f);
        InteroEventManager.GetEventManager().AddListener((IListenerOSC)this);
    }

    void Update()
    {
        if (client != null) client.unityThread.Update();
    }

    void IListenerOSC.OnOSCClientConnectedEvent(OSCClientConnectedEvent connectedEvent)
    {
        print(name + " OnOSCClientConnectedEvent " + connectedEvent);
    }

    void IListenerOSC.OnOSCErgDataEvent(OSCErgDataEvent ergEvent)
    {

        print(name + " OnOSCErgDataEvent " + ergEvent.ergData);
        ErgData e = ergEvent.ergData;
        Segment s = ergEvent.segment;
        leaderboard.UpdateRank(ergEvent.socketSender.username, s.getProgressedDistance(e), e);

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
        client.OnDestroy();
        Debug.Log("OnDestroy1");
    }

    void IListenerOSC.OnOSCStartWorkoutDataEvent(OSCStartWorkoutEvent startWorkoutEvent)
    {
        print(name + "  Start Workout!!!");
        workoutManager.StartWorkout();
        // client.SendMessage(new ErgData(1,2,3,4,5));
    }
}
