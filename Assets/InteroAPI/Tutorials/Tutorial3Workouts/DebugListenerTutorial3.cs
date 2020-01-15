using Intero.Events;
using UnityEngine;

public class DebugListenerTutorial3 : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        print("OnEndSegmentEvent " + endSegmentEvent.progressType + " " + endSegmentEvent.currentSegment);
    }

    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent)
    {
        // print("OnProgressSegmentEvent " + progressSegmentEvent.progressPercent + " " + progressSegmentEvent.progressType + " " + progressSegmentEvent.currentSegment);
        // print("next : " + progressSegmentEvent.nextSegment);
    }

    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent)
    {
        print("OnStartSegmentEvent " + startSegmentEvent.progressType + " " + startSegmentEvent.currentSegment);
    }

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
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
        // UnityEngine.Debug.Log("got ergData " + ergDataEvent.ergData);
    }
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);
    }
}
