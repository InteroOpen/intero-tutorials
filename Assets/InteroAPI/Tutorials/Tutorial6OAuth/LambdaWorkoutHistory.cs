using Intero.Common;
using Intero.Workouts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static InteroAPI.OAuth.InteroCloud;
using static TestInteroCloud;

public class LambdaWorkoutHistory : AuthManager
{
    public int historyId;
    public int classId;
    public void SetClassId(int classId)
    {
        this.classId = classId;
    }
    public async Task PostStartWorkout()
    {
        StartWorkoutJSON workoutHistory = await interoCloud.PostStartWorkout(classId);
        historyId = workoutHistory.id;
    }

    public async Task PostMessage(int segmentNum, ErgData e)
    {
        // print("PostMessage" + historyId+ segmentNum+ e);

        await interoCloud.PostMessage(historyId, segmentNum, e);
    }

    public async Task<Dictionary<int, WorkoutJSON>> GetWorkoutDic()
    {
        Dictionary<int, WorkoutJSON> workoutDic = new Dictionary<int, WorkoutJSON>();
        Debug.Log("fetching workocouts");
        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

        foreach (WorkoutJSON workout in workouts)
        {
            workoutDic.Add(int.Parse(workout.id), workout);
            // UnityEngine.Debug.Log(workout);
        }
        return workoutDic;
    }
    /*
    public async Task<TestInteroCloud.ContributorClass> GetWorkoutClasses()
    {
        Debug.Log("getting workouts");
        // List<WorkoutClassJSON> workoutClasses =
        TestInteroCloud.ContributorClass c = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        Debug.Log("Got workouts "+ c.response);
        return c;
//        return c.classes;
    }*/
    /*
    public async Task<List<WorkoutClassJSON>> GetWorkoutClasses()
    {
        Debug.Log("getting workouts");
        List < WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        // Debug.Log("Got workouts " + classes.Count);
        return workoutClasses;
        //        return c.classes;
    }*/
    
    public async Task<ContributorClassT> GetWorkoutClasses()
    {
        Debug.Log("getting workouts");
        ContributorClassT workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        return workoutClasses;
        //        return c.classes;
    }
}