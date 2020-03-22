using InteroAPI.OAuth;
using UnityEngine;
using System.Threading.Tasks;
using Intero.Workouts;
using System.Collections.Generic;

public class ScheduleController : MonoBehaviour
{
    InteroCloud interoCloud = new InteroCloud();
    // Start is called before the first frame update
    void Start()
    {
        GetWorkouts();
    }

    public async Task GetWorkouts()
    {
        await interoCloud.OAuth("rodrigosavage-at-gmail.com", "rtopdfrtio");
        
        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

        foreach (WorkoutJSON workout in workouts)
        {
            UnityEngine.Debug.Log(workout);
        }
        List<WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");

        foreach (WorkoutClassJSON workout in workoutClasses)
        {
            UnityEngine.Debug.Log(workout.dateStart + " de  "+workout.workoutId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
