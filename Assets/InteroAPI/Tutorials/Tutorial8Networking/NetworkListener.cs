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
    public RivalController rivalController;
    // Start is called before the first frame update
    public CanvasController cc;
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
        print(" OSC got ergta");
        string username = ergEvent.socketSender.username;

        ErgData e = ergEvent.ergData;
        Segment s = ergEvent.segment;
        float d = s.getProgressedDistance(e);
        print(username + " xx OSC " + ergEvent.ergData + "|"+d );
        leaderboard.UpdateRank(username, d, e, s);
        rivalController.UpdateRival(ergEvent);

    }

    void IListenerOSC.OnOSCMessageEvent(OSCMessageEvent messageEvent)
    {
    }

    void IListenerOSC.OnOSCStrokeDataEvent(OSCStrokeDataEvent strokeEvent)
    {
    }

    void IListenerOSC.OnOSCStartWorkoutDataEvent(OSCStartWorkoutEvent startWorkoutEvent)
    {
        print("Client  Start Workout!!!");
        // Spawn rival boats
        workoutManager.StartWorkout();
        rivalController.StartWorkout(6);
    }

    void IListenerOSC.OnOSCClientDisconnectedEvent(OSCClientDisconnectedEvent connectedEvent)
    {
        throw new System.NotImplementedException();
    }

    void IListenerOSC.OnOSCNoServerResponseEvent(Intero.Events.OSCNoServerResponseEvent e)
    {

        cc.ShowInfo("Error contactando el servidor de Intero.\nNo podra ver el progreso de otros:\n"+e.errorMsg);
        print("Client  OnOSCNoServerResponseEvent !!! "+e.errorMsg);
    }
}