using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;

/*
public class PhysicsManager
{
    InteroBody1D previousLocation;
    public float kP = 1.0f, kD = 1.0f;
    // this functions returns the new location
    // returns the new velocity of the object 
    public void ResetLocation()
    {
        previousLocation = null;
    }
    public InteroBody1D UpdateLocation(float realDistance, float realSpeed, ErgData e)
    {
        InteroBody1D targetLocation = new InteroBody1D(e);
        // InteroBody1D newLocation = new InteroBody1D(e);
        // first time?
        if (previousLocation == null)
        {
            // teleport to new location
            previousLocation = targetLocation;
            return new InteroBody1D(e.distance, 0);
        }
        else
        {
            float dT = targetLocation.time - previousLocation.time;

            if (dT < 0.0001f)
            {
                previousLocation.copy(targetLocation);
                // mantain the old speed
                return new InteroBody1D(realDistance, realSpeed);
            }

            float deltaDistance = targetLocation.distance - realDistance;
            float speed = deltaDistance / dT;
            // Debug.Log($"delta\t{e.distance}\t{deltaDistance}\t{dT}");
            if (System.Math.Abs(deltaDistance) > 15)
            {
                // teleport to new location
                previousLocation.copy(targetLocation);
                return new InteroBody1D(e.distance, realSpeed);
            }
            // PID controller
            float deltaForce = kD * speed + kP * (targetLocation.distance - realDistance);
            previousLocation.copy(targetLocation);
            return new InteroBody1D(realDistance, deltaForce);
        }
    }
}
*/


public class SegmentListener : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    private PhysicsManager physicsManager = new PhysicsManager();
    public GameObject player;
    public HUDController hud;
    public HUDController hudNext;
    // public ErgData currentErgData;
    public Segment currentSegment = null;
    UnityThreadEvents unityThreadEvents = new UnityThreadEvents(InteroEventManager.GetEventManager());

    private ErgData lastErgData;
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        if(endSegmentEvent.nextSegment==null)
        {
            // end workout
            // InteroEventManager.GetEventManager().SendEvent(new WorkoutEndEvent());
            unityThreadEvents.EnqueueEvent(new WorkoutEndEvent());
        }
       //  physicsManager.ResetLocation();
    }

    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent) { }
    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        currentSegment = startSegmentEvent.currentSegment;
        if(startSegmentEvent.nextSegment != null)
            hudNext.DisplayCurrentSegment(startSegmentEvent.nextSegment, null);

        // physicsManager.ResetLocation();
    } 

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent) {
        // physicsManager.ResetLocation();
    }
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
            /*

                // update hud
                if (currentSegment != null)
                {
                    ErgData e = new ErgData();
                    e.Copy(ergDataEvent.ergData);
                    e.distance = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                    // $"Hello, {name}! Today is {date.DayOfWeek},

                    print($"Progress d {e.distance}\t{x}\t{v}");
                    // float d = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                    // print("loc xx erg " + ergDataEvent.ergData + "|"+d);
                    // print(ergDataEvent.ergData);

                    body = physicsManager.UpdateLocation(x, v, e);
                    hud.DisplayCurrentSegment(currentSegment, ergDataEvent.ergData);
                    // physicsManager.se

                }
                else*/
            if (currentSegment != null)
            {
                ErgData e = new ErgData();
                e.Copy(ergDataEvent.ergData);
                e.distance = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                body = physicsManager.UpdateLocation(x, v, e);
                hud.DisplayCurrentSegment(currentSegment, ergDataEvent.ergData);
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
        // InvokeRepeating("")
    }
    void Update()
    {
        unityThreadEvents.Update();
    }
}
