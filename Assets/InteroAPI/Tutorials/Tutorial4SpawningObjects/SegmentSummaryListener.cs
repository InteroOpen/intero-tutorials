using Intero.Common;
using Intero.Events;
using Intero.Statistics;
using Intero.Workouts;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSummaryListener : MonoBehaviour, IListenerErg, IListenerWorkout, IListenerSegment
{
    StatisticManager statisticManager = new StatisticManager();
    Queue<ErgData> ergStack = new Queue<ErgData>();
    Queue<Segment> segmentStack = new Queue<Segment>();
    public SegmentSummary segmentSummary;
    public WorkoutSummaryController workoutSummary;
    // public CanvasController canvas;
    // Workout events
    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent) { }
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
        print("End Workout");
        workoutSummary.UpdateWorkoutSummary(ergStack.ToArray(), segmentStack.ToArray());
        // canvas.ShowWorkoutResultsView();
    }
    // Segment Events
    void IListenerSegment.OnStartSegmentEvent(SegmentStartEvent startSegmentEvent) {
        print("start Segment ");

    }
    void IListenerSegment.OnProgressSegmentEvent(SegmentProgressEvent progressSegmentEvent) { }
    void IListenerSegment.OnEndSegmentEvent(SegmentEndEvent endSegmentEvent)
    {
        print("End Segment ");
        ergStack.Enqueue(statisticManager.GetSegmentErgDataStats());
        segmentStack.Enqueue(endSegmentEvent.currentSegment);
        print(statisticManager.PrintErgStack());
        segmentSummary.ShowSegmentSummary(endSegmentEvent, statisticManager);
    }
    // Ergdata events
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent) {
    }
    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent) { }

    // Start is called before the first frame update
    void Start()
    {
        print("SegmentSummaryListener Start");
        InteroEventManager.GetEventManager().AddListener((IListenerErg)this);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)this);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)this);

        // add statistic manager to the listener
        InteroEventManager.GetEventManager().AddListener((IListenerErg)statisticManager);
        InteroEventManager.GetEventManager().AddListener((IListenerWorkout)statisticManager);
        InteroEventManager.GetEventManager().AddListener((IListenerSegment)statisticManager);
    }


}
