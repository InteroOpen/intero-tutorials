using UnityEngine;
using UnityEngine.UI;
using Intero.Workouts;


public class ClassUiItem : MonoBehaviour{

    //Class name
    public Text className;

    //Class time
    public Text classStart;

    //Class day
    public Text Day;

    //Id to the other scene
    // public string idItem;

    public WorkoutJSON workout;
    
    public SegmentFiller segmentFiller;
    public void ChangeScene()
    {

        segmentFiller.ShowSelectedWorkout(workout);
        // SingletonWorkouts.instancia.id = idItem;
        // SceneManager.LoadScene("LobbyEscene");
    }
}
