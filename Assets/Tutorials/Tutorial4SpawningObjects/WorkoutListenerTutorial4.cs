using Intero.Events;
using UnityEngine;

public class WorkoutListenerTutorial4 : MonoBehaviour, IListenerWorkout
{
    public GameObject UIMenu;
    public GameObject Scene;
    void IListenerWorkout.OnEndWorkoutEvent(WorkoutEndEvent endWorkoutEvent)
    {
        UIMenu.SetActive(true);
        Scene.SetActive(false);
    }

    void IListenerWorkout.OnStartWorkoutEvent(WorkoutStartEvent startWorkoutEvent)
    {
        UIMenu.SetActive(false);
        Scene.SetActive(true);
    }

    void Start()
    {
        InteroEventManager.GetEventManager().AddListener(this);
    }
}
