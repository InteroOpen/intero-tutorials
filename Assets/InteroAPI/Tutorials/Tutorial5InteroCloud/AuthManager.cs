using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using InteroAPI.OAuth;
using UnityEngine;
using Intero.Common;
using static InteroAPI.OAuth.InteroCloud;
using Intero.Workouts;

public class AuthManager : MonoBehaviour
{
    public InteroCloud interoCloud = new InteroCloud();
    public WorkoutManager workoutManager;
    public int historyId;
    public int classId;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("AuthManager.Start!!");
        Login();
    }
    async Task Login()
    {
        await interoCloud.OAuth("rodrigosavage-at-gmail.com", "rtopdfrtio");
        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");
        WorkoutJSON work = workouts[0];
        workoutManager.LoadWorkout(work);
    }
    public void SetClassId(int classId)
    {
        this.classId = classId;
        print("Setting SetClassId id " + classId);

    }
    public async Task PostStartWorkout()
    {
        StartWorkoutJSON workoutHistory = await interoCloud.PostStartWorkout(classId);
        historyId = workoutHistory.id;
        print("Setting history id " + historyId);
    }

    public void  PostMessage( int segmentNum, ErgData e)
    {
        interoCloud.PostMessage(historyId, segmentNum, e);
    }

}
