using Intero.Events;
using UnityEngine;

public class WorkoutListenerTutorial4 : MonoBehaviour, IListenerWorkout
{
    public GameObject UIMenu;
    public GameObject Scene;
    public GameObject WorkoutSummary;
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
        UIMenu.SetActive(true);
        Scene.SetActive(false);

        if (WorkoutSummary) WorkoutSummary.SetActive(true);
    }

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
        UIMenu.SetActive(false);
        Scene.SetActive(true);
        if(WorkoutSummary) WorkoutSummary.SetActive(false);

    }

    void Start()
    {
        InteroEventManager.GetEventManager().AddListener(this);
    }
}
