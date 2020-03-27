using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using InteroAPI.OAuth;
using UnityEngine;
using Intero.Common;
using static InteroAPI.OAuth.InteroCloud;
using Intero.Workouts;
using System;

public class AuthManager : MonoBehaviour
{
    public InteroCloud interoCloud = new InteroCloud();
    public WorkoutManager workoutManager;
    public int historyId;
    public int classId;
    public PasswordManager passwordManager;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("AuthManager.Start!!");
        /*
        try
        {
            passwordManager.SaveCredentials("user", "password");
        } catch (Exception e)
        {
            print("e " + e.Message);
        }
        */
        // Login();
    }



    //public async Task Login(string username, string password)
    //{
    //    print("login start " + username);

    //    //await interoCloud.OAuth(username, password);
    //    print("login success " + username);
    //    passwordManager.SaveCredentials(username,password);
    //    print("SaveCredentials success " + username);

    //    //
    //    // List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");
    //    // WorkoutJSON work = workouts[0];
    //    // workoutManager.LoadWorkout(work);
    //}
    public async Task<string> Login()
    {
        passwordManager.ReadCredentials();
        string username = passwordManager.username;
        string password = passwordManager.password;
        print("Login leyo correcto "+ username + " dd " + password);
        string s  = await interoCloud.OAuth(username, password);
        print("error " + s);

        return s;
        //
        // List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");
        // WorkoutJSON work = workouts[0];
        // workoutManager.LoadWorkout(work);
    }

    public async Task<string> Signup(string user, string email, string pass)
    {
        string s = await interoCloud.Signup(user, email, pass);
        passwordManager.SaveCredentials(user, pass);
        return s;
    }
    public bool AreCredentialsSaved()
    {
        return passwordManager.AreCredentialsSaved();
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

    public async Task PostMessage(int segmentNum, ErgData e)
    {
        print("PostMessage" + historyId+ segmentNum+ e);

        await interoCloud.PostMessage(historyId, segmentNum, e);
    }

    public async Task<Dictionary<int, WorkoutJSON>> GetWorkoutDic()
    {
        Dictionary<int, WorkoutJSON> workoutDic = new Dictionary<int, WorkoutJSON>();
        print("fetching workocouts");
        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

        foreach (WorkoutJSON workout in workouts)
        {
            workoutDic.Add(int.Parse(workout.id), workout);
            UnityEngine.Debug.Log(workout);
        }
        return workoutDic;
    }
    public async Task<List<WorkoutClassJSON>> GetWorkoutClasses()
    {
        List<WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        // ShowWorkouts(workoutClasses, workoutDic);
        return workoutClasses;
    }

}
