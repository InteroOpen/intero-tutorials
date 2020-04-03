using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;

public class SegmentListenerCloud : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    public GameObject player;
    public AuthManager auth;
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
        // ergDataEvent
        
        // hud.DisplayCurrentSegment(progressSegmentEvent.currentSegment, progressSegmentEvent.progressPercent);

        // print("OnProgressSegmentEvent " + progressSegmentEvent.progressPercent + " " + progressSegmentEvent.progressType + " " + progressSegmentEvent.currentSegment);
        // print("next : " + progressSegmentEvent.nextSegment);
    }

    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        print("OnStartSegmentEvent " + startSegmentEvent.progressType + " " + startSegmentEvent.currentSegment);
        currentSegment = startSegmentEvent.currentSegment;
        print("OnStartSegmentEvent " + currentSegment.index);
        // hud.DisplayCurrentSegment(startSegmentEvent.currentSegment, 0);
        // hudNext.DisplayCurrentSegment(startSegmentEvent.nextSegment, null);
    } 

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
        auth.SetClassId(6969);
        auth.PostStartWorkout();
    }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent)
    {
        // UnityEngine.Debug.Log("got strokeData " + strokeDataEvent.strokeData);
    }
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        ErgData currentErgData = ergDataEvent.ergData;
        if (player.activeInHierarchy)
        {
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
