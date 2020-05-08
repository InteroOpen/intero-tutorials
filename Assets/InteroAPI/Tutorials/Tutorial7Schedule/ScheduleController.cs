using InteroAPI.OAuth;
using UnityEngine;
using System.Threading.Tasks;
using Intero.Workouts;
using System.Collections.Generic;
using System;

public class ScheduleController : MonoBehaviour
{

    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    public void ShowWorkouts(WorkoutClassJSON[] workoutClasses, Dictionary<int, WorkoutJSON> workouts)
    {
        for (int i = 0; i < workoutClasses.Length; i++)
        {
            WorkoutJSON workout = workouts[workoutClasses[i].workoutId];

            // workout.MemberwiseClone();
            print (workout.name);
            //fechas[i] = workoutClasses[i].dateStart;
            // 60 width of item
            float spawnY = i * 60;
            //newSpawn Position
            Vector3 pos = new Vector3(0, -spawnY, 0);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ClassUiItem classuiItem = SpawnedItem.GetComponent<ClassUiItem>();

            classuiItem.workout = workout;
            //set name
            classuiItem.className.text = workout.name;
            //set hour
            classuiItem.classStart.text = workoutClasses[i].dateStart.TimeOfDay.ToString();
            //set day
            classuiItem.Day.text = workoutClasses[i].dateStart.DayOfWeek.ToString();
        }
        print("ScheduleController.ShowWorkouts Count" + workoutClasses.Length);

    }
    private async Task GetWorkouts()
    {
        /*
        InteroCloud interoCloud = new InteroCloud();

        Dictionary<int, WorkoutJSON> workoutDic = new Dictionary<int, WorkoutJSON>();
        await interoCloud.OAuth("rodrigosavage-at-gmail.com", "rtopdfrtio");

        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

        foreach (WorkoutJSON workout in workouts)
        {
            workoutDic.Add( int.Parse( workout.id), workout);
            UnityEngine.Debug.Log(workout);
        }
        List<WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        ShowWorkouts(workoutClasses, workoutDic);*/
    }
 
}
