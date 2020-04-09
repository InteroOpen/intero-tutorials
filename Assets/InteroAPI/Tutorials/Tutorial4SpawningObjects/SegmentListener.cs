using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;

public class SegmentListener : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    private PhysicsManager physicsManager = new PhysicsManager();
    public GameObject player;
    public HUDController hud;
    public HUDController hudNext;
    // public ErgData currentErgData;
    public Segment currentSegment = null;
    UnityThreadEvents unityThreadEvents = new UnityThreadEvents(InteroEventManager.GetEventManager());
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        if(endSegmentEvent.nextSegment==null)
        {
            // end workout
            // InteroEventManager.GetEventManager().SendEvent(new WorkoutEndEvent());
            unityThreadEvents.EnqueueEvent(new WorkoutEndEvent());
        }
        physicsManager.ResetLocation();
    }

    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent) { }
    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        currentSegment = startSegmentEvent.currentSegment;
        if(startSegmentEvent.nextSegment != null)
            hudNext.DisplayCurrentSegment(startSegmentEvent.nextSegment, null);

        physicsManager.ResetLocation();
    } 

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent) { }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
        currentSegment = null;
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent) { }
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        // currentErgData = ergDataEvent.ergData;
        if (player.activeInHierarchy)
        {
            Rigidbody rigidBody = player.GetComponent<Rigidbody>();
            float v = rigidBody.velocity.x;
            float x = rigidBody.position.x;

            InteroBody1D body = null;  

            // update hud
            if (currentSegment != null)
            {
                ErgData e = new ErgData();
                e.Copy(ergDataEvent.ergData);
                e.distance = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                // float d = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                // print("loc xx erg " + ergDataEvent.ergData + "|"+d);
                print(ergDataEvent.ergData);

                body = physicsManager.UpdateLocation(x, v, e);
                hud.DisplayCurrentSegment(currentSegment, ergDataEvent.ergData);
                // physicsManager.se

            }
            else
            {
                body = physicsManager.UpdateLocation(x, v, ergDataEvent.ergData);
            }

            rigidBody.velocity = new Vector3(body.velocity, rigidBody.velocity.y, rigidBody.velocity.z);
            rigidBody.position = new Vector3(body.distance, rigidBody.position.y, rigidBody.position.z);
        }
    }
    void Start()
    {

        print("SegmentListener Start");

        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);
    }
    void Update()
    {
        unityThreadEvents.Update();
    }
}
