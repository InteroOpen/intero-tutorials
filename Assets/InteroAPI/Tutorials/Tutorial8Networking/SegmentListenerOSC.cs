using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;
using InteroAPI.Statistics;

public class SegmentListenerOSC : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    public GameObject player;
    public ClientUI netManager;
    public LeaderboardController leaderboard;

    /*
    private PhysicsManager physicsManager = new PhysicsManager();
    public HUDController hud;
    public HUDController hudNext;
    */
    // public ErgData currentErgData;
    public Segment currentSegment = null;
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        print("OnEndSegmentEvent " + endSegmentEvent.progressType + " " + endSegmentEvent.currentSegment);
    }

    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent)
    {
    }

    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        print("xx OnStartSegmentEvent " + currentSegment);
        currentSegment = startSegmentEvent.currentSegment;
    } 

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent){ }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent) {  }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent)
    {
    }
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        ErgData ergData = ergDataEvent.ergData;
        if (player.activeInHierarchy && currentSegment !=null)
        {
            print("Local " + ergData);
            netManager.SendMessage(ergData, currentSegment);
            leaderboard.UpdateRank(netManager.GetName(), currentSegment.getProgressedDistance(ergData), ergData, currentSegment);

        }
        // UnityEngine.Debug.Log("got ergData " + ergDataEvent.ergData);
    }
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);
    }
}
