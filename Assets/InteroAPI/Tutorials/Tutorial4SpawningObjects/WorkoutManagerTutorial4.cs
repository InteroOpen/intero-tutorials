using UnityEngine;
using Intero.Workouts;
using Intero.Events;

public class WorkoutManagerTutorial4 : MonoBehaviour
{
    SegmentManager segmentManager;

    void Start()
    {
        segmentManager = new SegmentManager();
        segmentManager.Push(new SegmentTime(6, 20, SegmentIntensity.EASY));
        segmentManager.Push(new SegmentDistance(100, 18, SegmentIntensity.EASY));
        segmentManager.Push(new SegmentTime(12, 22, SegmentIntensity.MEDIUM));
        segmentManager.Push(new SegmentTime(3, 24, SegmentIntensity.FAST));
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
