using UnityEngine;
using Intero.Workouts;
using Intero.Events;

public class WorkoutManagerTutorial4 : MonoBehaviour
{
    SegmentManager segmentManager;

    void Start()
    {
        segmentManager = new SegmentManager();
        segmentManager.Push(new SegmentTime(6, 20, SegmentType.EASY));
        segmentManager.Push(new SegmentDistance(100, 18, SegmentType.EASY));
        segmentManager.Push(new SegmentTime(12, 22, SegmentType.MEDIUM));
        segmentManager.Push(new SegmentTime(3, 24, SegmentType.FAST));
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
