using Intero.Events;
using UnityEngine;
using Intero.Physics;
using Intero.Workouts;
using Intero.Common;

public class SegmentListenerPhysicsProfiler : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{

    // private PhysicsManagerBasic physicsManagerBasic = new PhysicsManagerBasic();
    // public GameObject playerBasic;
    private PhysicsManager[] physicsManagers;
    public GameObject[] players;
    public GameObject player;
    public GameObject playerref;
    // public HUDController hud;
    //  public HUDController hudNext;
    // public ErgData currentErgData;
    public Segment currentSegment = null;
    UnityThreadEvents unityThreadEvents = new UnityThreadEvents(InteroEventManager.GetEventManager());

    // private ErgData lastErgData;
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        if (endSegmentEvent.nextSegment == null)
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
        for(int i =0;i< physicsManagers.Length; ++i)
            physicsManagers[i].ResetLocation();
    }

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
        // physicsManager.ResetLocation();
    }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
        // currentSegment = null;
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent) { }
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        // currentErgData = ergDataEvent.ergData;
        if (playerref.activeInHierarchy)
        {
            for (int i = 0; i < physicsManagers.Length; ++i)
            {
                Rigidbody rigidBody = players[i].GetComponent<Rigidbody>();

                float v = rigidBody.velocity.x;
                float x = rigidBody.position.x;

                InteroBody1D body = null;
                if (currentSegment != null)
                {
                    ErgData e = new ErgData();
                    e.Copy(ergDataEvent.ergData);
                    e.distance = currentSegment.getProgressedDistance(ergDataEvent.ergData);
                    body = physicsManagers[i].UpdateLocation(x, v, e);
                    //hud.DisplayCurrentSegment(currentSegment, ergDataEvent.ergData);
                }
                else
                {
                    body = physicsManagers[i].UpdateLocation(x, v, ergDataEvent.ergData);
                }

                rigidBody.velocity = new Vector3(body.velocity, rigidBody.velocity.y, rigidBody.velocity.z);
                rigidBody.position = new Vector3(body.distance, rigidBody.position.y, rigidBody.position.z);
                // print(i+ "update " + body.distance);
            }
        }
    }
    void Start()
    {

        print("SegmentListener Start");

        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);
        players = new GameObject[3];
        physicsManagers = new PhysicsManager[3];
        /*
        for (int i = 0; i < numberEntries; i++)
        {
            GameObject g = Instantiate(rankPrefab, new Vector3(0, -i * 90.0F - 90F, 0), Quaternion.identity);// rankPrefab.transform.position, rankPrefab.transform.rotation);// new Vector3(0, -i * 30.0F, 0), Quaternion.identity);
            g.SetActive(true);                                                                                         //setParent
            g.transform.SetParent(transform, false);
            rankNameTexts[i] = g.transform.Find("RankName").GetComponent<Text>();
            rankStatsTexts[i] = g.transform.Find("RankStats").GetComponent<Text>();
        }*/

        for (int i = 0; i < physicsManagers.Length; ++i)
        {
            // print("physicsManagers new");
            players[i] = Instantiate(player, new Vector3(4*i, 0, 0), Quaternion.identity);
            players[i].transform.SetParent(playerref.transform, false);
            players[i].SetActive(true);
        }
        physicsManagers[0] = new PhysicsManager() ;
        physicsManagers[1] = new PhysicsManagerBasic();
        physicsManagers[2] = new PhysicsManagerModificado();
    }
    void Update()
    {
        unityThreadEvents.Update();
    }
}
