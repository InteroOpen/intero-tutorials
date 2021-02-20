using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;
using Mirror;
using Mirror.Examples.Chat;
using System.Collections.Generic;

public class SegmentListenerMirror : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    private PhysicsManagerModificado physicsManager = new PhysicsManagerModificado();
    public GameObject player;
    public HUDController hud;
    public HUDController hudNext;
    public Segment currentSegment = null;
    UnityThreadEvents unityThreadEvents = new UnityThreadEvents(InteroEventManager.GetEventManager());
    public RivalController rivalController;
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        if(endSegmentEvent.nextSegment==null)
        {
            // end workout
            // InteroEventManager.GetEventManager().SendEvent(new WorkoutEndEvent());
            unityThreadEvents.EnqueueEvent(new WorkoutEndEvent());
        }
    }

    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent) { }
    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        currentSegment = startSegmentEvent.currentSegment;
        Debug.Log("OnStartSegmentEvent CCC" + startSegmentEvent.nextSegment);
        if (startSegmentEvent.nextSegment != null){
            Debug.Log("Updating hud next CCC" + startSegmentEvent.nextSegment);
            hudNext.DisplayCurrentSegment(startSegmentEvent.nextSegment, null);

        }

        // Player playerMirror = NetworkClient.connection.identity.GetComponent<Player>();
        // Sala sala  = playerMirror.transform.parent.GetComponent<Sala>();
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");

        // int[] arr = new int[players.Length];

        rivalController.StartWorkout(players);
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
            Debug.Log("Senging CmdSendErgData");
            Player playerMirror = NetworkClient.connection.identity.GetComponent<Player>();
            playerMirror.CmdSendErgData(ergDataEvent.ergData);

            /*
            Rigidbody rigidBody = player.GetComponent<Rigidbody>();
            float v = rigidBody.velocity.x;
            float x = rigidBody.position.x;

            InteroBody1D body = null;
            */
            if (currentSegment != null)
            {
                ErgData e = new ErgData();
                e.Copy(ergDataEvent.ergData);
                e.distance = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                hud.DisplayCurrentSegment(currentSegment, ergDataEvent.ergData);
                //body = physicsManager.UpdateLocation(x, v, e);
            }/*
            else
            {
                body = physicsManager.UpdateLocation(x, v, ergDataEvent.ergData);
            }

            rigidBody.velocity = new Vector3(body.velocity, rigidBody.velocity.y, rigidBody.velocity.z);
            rigidBody.position = new Vector3(body.distance, rigidBody.position.y, rigidBody.position.z);
            */
        }
    }
    void Start()
    {
        print("SegmentListener Start");
        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);
        // Creamos los rivales
    }
    void Update()
    {
        unityThreadEvents.Update();
    }
}
