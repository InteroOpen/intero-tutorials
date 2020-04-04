using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;
using Intero.Statistics;

public class SegmentListener : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    private PhysicsManager physicsManager = new PhysicsManager();
    public GameObject player;
    public HUDController hud;
    public HUDController hudNext;
    public SegmentSummary segmentSummary;
    StatisticManager statisticManager = new StatisticManager();
    // public ErgData currentErgData;
    public Segment currentSegment = null;
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        // print("OnEndSegmentEvent " + endSegmentEvent.progressType + " " + endSegmentEvent.currentSegment);
        physicsManager.ResetLocation();
        segmentSummary.ShowSegmentSummary(endSegmentEvent, statisticManager);
    }

    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent)
    { }

    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        // print("OnStartSegmentEvent " + startSegmentEvent.progressType + " " + startSegmentEvent.currentSegment);
        currentSegment = startSegmentEvent.currentSegment;
        // hud.DisplayCurrentSegment(startSegmentEvent.currentSegment, 0);
        if(startSegmentEvent.nextSegment != null)
            hudNext.DisplayCurrentSegment(startSegmentEvent.nextSegment, null);
        // reset location
        physicsManager.ResetLocation();
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

         // player.GetComponent<Rigidbody>();
        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);
        // add statistic manager to the listener
        InteroEventManager.GetEventManager().AddListener((IListenerErg)statisticManager);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)statisticManager);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)statisticManager);
    }
}
