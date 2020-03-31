using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intero.Workouts;

public class SingletonWorkouts : MonoBehaviour
{
    public static SingletonWorkouts instancia;
    public List<WorkoutJSON> workouts;
    public List<WorkoutClassJSON> workoutClasses;
    public List<SegmentJSON> segments;
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
