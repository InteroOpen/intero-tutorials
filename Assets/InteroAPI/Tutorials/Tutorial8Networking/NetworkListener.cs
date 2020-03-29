using Intero.TCP;
using UnityEngine;
using Intero.Events;
using Intero.Common;
using UnityEngine.UI;
using Intero.Workouts;

public class NetworkListener : MonoBehaviour, IListenerOSC
{
    public WorkoutManager workoutManager;
    public LeaderboardController leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener((IListenerOSC)this);
    }

    void IListenerOSC.OnOSCClientConnectedEvent(OSCClientConnectedEvent connectedEvent)
    {
        print(" ConnectedToServer ");
    }

    void IListenerOSC.OnOSCErgDataEvent(OSCErgDataEvent ergEvent)
    {

        string username = ergEvent.socketSender.username;
        ErgData e = ergEvent.ergData;
        Segment s = ergEvent.segment;
        float d = s.getProgressedDistance(e);
        print(username + " xx OSC " + ergEvent.ergData + "|"+d );
        leaderboard.UpdateRank(username, d, e, s);

    }

    void IListenerOSC.OnOSCMessageEvent(OSCMessageEvent messageEvent)
    {
    }

    void IListenerOSC.OnOSCStrokeDataEvent(OSCStrokeDataEvent strokeEvent)
    {
    }
    void OnDestroy()
    {
        //client.OnDestroy();
        // Debug.Log("OnDestroy1");
    }

    void IListenerOSC.OnOSCStartWorkoutDataEvent(OSCStartWorkoutEvent startWorkoutEvent)
    {
        print("Client  Start Workout!!!");
        workoutManager.StartWorkout();
        // client.SendMessage(new ErgData(1,2,3,4,5));
    }
}
