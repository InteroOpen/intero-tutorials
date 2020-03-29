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

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
        
    }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent)
    {
    }
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        ErgData ergData = ergDataEvent.ergData;
        if (player.activeInHierarchy && currentSegment !=null)
        {
            print("Local " + ergData);
            
            //print("xx currentSegment " + currentSegment);
            netManager.SendMessage(ergData, currentSegment);
            leaderboard.UpdateRank(netManager.GetName(), currentSegment.getProgressedDistance(ergData), ergData, currentSegment);
            /*
            if (currentSegment != null)
            {
                int i = currentSegment.index;
                print("OnStartSegmentEventJJJ " +i);
                auth.PostMessage(i, currentErgData);

            }
            /*
            Rigidbody rigidBody = player.GetComponent<Rigidbody>();
            float v = rigidBody.velocity.x;
            float x = rigidBody.position.x;
            InteroBody1D body = physicsManager.UpdateLocation(x, v, ergDataEvent.ergData);
            rigidBody.velocity = new Vector3(body.velocity, rigidBody.velocity.y, rigidBody.velocity.z);
            rigidBody.position = new Vector3(body.distance, rigidBody.position.y, rigidBody.position.z);
            // update hud
            if(currentSegment!=null)
                hud.DisplayCurrentSegment(currentSegment, ergDataEvent.ergData);
            */
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
