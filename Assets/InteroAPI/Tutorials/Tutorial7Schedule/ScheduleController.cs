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

    [SerializeField]
    private RectTransform content = null;

    [SerializeField]
    private int numberOfItems = 0;
    private int numberOfWorkouts = 0;

    public ClassUiItem[] workoutItems = null;
    Dictionary<string, string> nombres = new Dictionary<string, string>();
    DateTime[] fechasInicio; 

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
        SingletonWorkouts.instancia.workouts = workouts;
        foreach (WorkoutJSON workout in workouts){
            numberOfWorkouts++;
            nombres.Add(workout.id, workout.name);
            UnityEngine.Debug.Log(workout);
        }

        List<WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        SingletonWorkouts.instancia.workoutClasses = workoutClasses;
        foreach (WorkoutClassJSON workout in workoutClasses){
            numberOfItems++;
            UnityEngine.Debug.Log(workout.dateStart + " de  "+workout.workoutId);
        }

        content.sizeDelta = new Vector2(0, numberOfItems * 60);

        for (int i = 0; i < numberOfItems; i++)
        {
            //fechas[i] = workoutClasses[i].dateStart;
            // 60 width of item
            float spawnY = i * 60;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x -100, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ClassUiItem classuiItem = SpawnedItem.GetComponent<ClassUiItem>();
            //set name
            classuiItem.className.text = nombres[workoutClasses[i].workoutId.ToString()];
            //set hour
            classuiItem.classStart.text = workoutClasses[i].dateStart.TimeOfDay.ToString();
            //set day
            classuiItem.Day.text = workoutClasses[i].dateStart.DayOfWeek.ToString();
            //set id
            classuiItem.idItem = workoutClasses[i].workoutId.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
