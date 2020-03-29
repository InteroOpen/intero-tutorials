using UnityEngine;
using Intero.Workouts;
using Intero.Events;
using System.Threading.Tasks;
using InteroAPI.OAuth;
using System.Collections.Generic;
using Intero.Common;

public class WorkoutManager : MonoBehaviour
{
    SegmentManager segmentManager;
    Segment[] segments;
    void Start()
    {
        segmentManager = new SegmentManager();
        
        segmentManager.Push(new SegmentTime(6, 20, SegmentIntensity.EASY));
        segmentManager.Push(new SegmentTime(12, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(6, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(3, 24, SegmentIntensity.FAST));
        segmentManager.Push(new SegmentTime(30, 18, SegmentIntensity.EASY));

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
        print("Loading " + work);
        foreach (SegmentJSON segmentJSON in work.segments)
        {

            Segment s = Segment.Factory(segmentJSON);
            segmentManager.Push(s);
        }

    }
   
    public void StartWorkout()
    {
        segmentManager.OnStartWorkout();
        print("SegmentManager.StartWorkout");
        InteroEventManager.GetEventManager().SendEvent(new WorkoutStartEvent());
    }

    public void EndWorkout()
    {
        print("SegmentManager.EndWorkout");
        InteroEventManager.GetEventManager().SendEvent(new WorkoutEndEvent());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            EndWorkout();
        }
    }
}
