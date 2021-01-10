using UnityEngine;
using Intero.Workouts;
using Intero.Events;
using InteroAPI.OAuth;

public class WorkoutManager : MonoBehaviour, IListenerWorkout
{
    SegmentManager segmentManager;
    public PauseController pauseController;
    public ClientUI client;
    // Segment[] segments;
    public bool inWorkout;
    void Start()
    {
        segmentManager = new SegmentManager();
        segmentManager.Push(new SegmentTime(500, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(5, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(20, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(20, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(20, 24, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(20, 24, SegmentIntensity.MEDIUM));
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().SendEvent(new WorkoutStartEvent());

        /*
        segmentManager.Push(new SegmentDistance(1000, 20, SegmentIntensity.EASY));
        segmentManager.Push(new SegmentDistance(500, 22, SegmentIntensity.FAST));
        segmentManager.Push(new SegmentTime(200, 24, SegmentIntensity.MEDIUM));

        inWorkout = false;
        /*
        segmentManager.Push(new SegmentTime(3, 20, SegmentIntensity.EASY));
        segmentManager.Push(new SegmentTime(3, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(3, 24, SegmentIntensity.FAST));
        /*
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(30, 18, SegmentIntensity.EASY));
        */
        // TestLoadClass();
        //  segments = segmentManager.ToArray();
    }
    //public float GetRemainingSegment (int segmentId, ErgData e)
    //{
    //    segments[segmentId].
    //}
    /*
    async Task TestLoadClass()
    {
        InteroCloud interoCloud = new InteroCloud();

        await interoCloud.OAuth("rodrigosavage-at-gmail.com", "rtopdfrtio");

        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");
        WorkoutJSON work = workouts[0];
        print("n " + work.name);
        foreach (SegmentJSON segmentJSON in work.segments)
        {
            
            Segment s = Segment.Factory(segmentJSON);
            print("segmentJSON " + segmentJSON);
            //print("segmentJSON " + (segmentJSON.type == SegmentType.distance));
            print("seg " + s);
            segmentManager.Push(s);
        }
    }*/

    public void LoadWorkout(WorkoutJSON work)
    {
        segmentManager.Clear();
        print("Loading " + work);
        if(work == null)
        {
            print("Error Workout is null");
           // return;
        }

        foreach (SegmentJSON segmentJSON in work.segments)
        {

            Segment s = Segment.Factory(segmentJSON);
            segmentManager.Push(s);
        }

    }

    public void StartWorkout()
    {
        // client.SendStartWorkout();
        InteroEventManager.GetEventManager().SendEvent(new WorkoutStartEvent());
    }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent) { }
    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
        segmentManager.OnStartWorkout();
        print("SegmentManager.StartWorkout");
        inWorkout = true;
    }
    //public void StartWorkout()
/*    {
        mainScene.SetActive(true);
        UIMenu.SetActive(false);
    }
    */
    public void EndWorkout()
    {
        print("SegmentManager.EndWorkout");
        InteroEventManager.GetEventManager().SendEvent(new WorkoutEndEvent());
        inWorkout = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && inWorkout)
        {
            EndWorkout();
        }
        if (Input.GetMouseButtonDown(0) && inWorkout)
        {
            Debug.Log("Click!! mewnue");
            pauseController.ShowPauseMenu(true);

        }
    }
}
