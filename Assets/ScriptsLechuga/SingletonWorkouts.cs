
using UnityEngine;
using InteroAPI.OAuth;

public class SingletonWorkouts : MonoBehaviour
{
    public static SingletonWorkouts instancia;
    public WorkoutJSON[] workouts;
    public WorkoutClassJSON[] workoutClasses;
    public SegmentJSON[] segments;
    public string id;

    private void Awake()
    {
        //Si no tenemos un Manejador de estres activo. . . 
        if (SingletonWorkouts.instancia == null)
        {
            //...hacemos que este sea el activo...
            instancia = this;
        }
        //...en otro caso...
        else if (instancia != this)
        {
            //Destruimos este objeto porque se ha duplicado.
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    

}
