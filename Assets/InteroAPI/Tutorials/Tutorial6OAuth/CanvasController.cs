// using Intero.Workouts;
// using System.Collections;
using InteroAPI.OAuth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
// using static TestInteroCloud;

public class CanvasController : MonoBehaviour
{
    public GameObject loginView;
    public GameObject loginAccountView;
    public GameObject createAccountView;
    public GameObject completeProfileView;
    public GameObject rowingScheduleView;
    public GameObject workoutSumaryView;
    public GameObject workoutResultsView;
    public GameObject profileView;
    public GameObject syncErgView;

    public ModalInfoController modalInfo;
    public LambdaWorkoutHistory authManager;
    // public LambdaWorkoutHistoryNoAWS authManager;
    public ScheduleController schedule;

    bool showInfo = false;
    string infoMsg;
    // public PasswordManager passwordManager;
    public void Awake()
    {
        authManager = new LambdaWorkoutHistory();
        HideAll();
        CheckLogin();
    }

    public void Update()
    {
        if (showInfo)
        {
            showInfo = false;
            modalInfo.Show(infoMsg);
        }
    }

    public void ShowInfo(string msg)
    {
        showInfo = true;
        infoMsg = msg;
    }

    public Text testOut;
    async Task LoadWorkouts()
    {
        testOut.text = "loading workouts...";
        testOut.text = "loading " + authManager.accessToken; // interoCloud..GetOAuthToken();

        WorkoutClassJSON[] workoutClasses = await authManager.GetWorkoutClasses();
        Dictionary<int, WorkoutJSON> workouts = await authManager.GetWorkoutDic();
        testOut.text = "loading Finished..";
        testOut.text = "N workout " + workoutClasses.Length;
        print("got LoadWorkouts " + workoutClasses.Length);
        print("got " + workouts);
        schedule.ShowWorkouts(workoutClasses, workouts);
        /*
        ContributorClassT c = await authManager.GetWorkoutClasses();
        Debug.Log("Melanie! " + c.response.IsSuccessStatusCode);
        if(c.response.IsSuccessStatusCode == false){
            modalInfo.Show(c.response.ToString());
        } else 
        
        {
            List<WorkoutClassJSON> workoutClasses = c.classes;
            // List<WorkoutClassJSON> workoutClasses = await authManager.GetWorkoutClasses();
            Debug.Log("Got classes " + workoutClasses);
            // List<WorkoutClassJSON> workoutClasses = await authManager.GetWorkoutClasses();
            Dictionary<int, WorkoutJSON> workouts = await authManager.GetWorkoutDic();
            testOut.text = "loading Finished..";
            testOut.text = "N workout " + workoutClasses.Count;
            print("got LoadWorkouts " + workoutClasses.Count);
            print("got " + workouts);
            schedule.ShowWorkouts(workoutClasses, workouts);
        }*/

    }

    async Task CheckLogin() {
        try
        {
            ShowloginView();
            // return;
        }catch (Exception e)
        {
            Debug.LogError(e);
        }
        print("jojo");
        print("Are creds save? "+ authManager.AreCredentialsSaved());
        if (authManager.AreCredentialsSaved())
        {
            string error = await authManager.Login();
            print("error! ! Fumar" + error);
            if (error==null)
            {
                ShowrowingScheduleView();
            }
            else
            {
                modalInfo.Show(error);
            }
        }
        print("passwordManager " + authManager.passwordManager);
    }
    public async Task<string> Login(string u, string p)
    {
        return await authManager.Login(u,p);
    }
    public async Task<string> Signup(string user, string email,string pass) { 
        return await authManager.Signup(user, email, pass);
        
    }


    public void HideAll()
    {

        //print("CanvasCOntroller.HideAll");
        
        loginView.SetActive(false);
        loginAccountView.SetActive(false);

        createAccountView.SetActive(false);
        completeProfileView.SetActive(false);
        rowingScheduleView.SetActive(false);
        workoutSumaryView.SetActive(false);
        workoutResultsView.SetActive(false);

        profileView.SetActive(false);
        syncErgView.SetActive(false);

       // print("CanvasCOntroller.HideAll success");
    }

    public void ShowloginView()
    {
        HideAll();
        loginView.SetActive(true);
    }
    public void ShowloginAccountView()
    {
        HideAll();
        loginAccountView.SetActive(true);
    }
    public string GetUsername()
    {
        return authManager.passwordManager.username;
    }
    public void ShowcreateAccountView()
    {
        HideAll();
        print("passwordManager " + authManager.passwordManager);

        createAccountView.SetActive(true);
        print("passwordManager " + authManager.passwordManager);

    }
    public void ShowcompleteProfileView()
    {
        HideAll();
        completeProfileView.SetActive(true);
    }
    public void ShowrowingScheduleView()
    {
        LoadWorkouts();
        HideAll();
        rowingScheduleView.SetActive(true);
    }
    public void ShowworkoutSumaryView()
    {
        HideAll();
        workoutSumaryView.SetActive(true);
    }
    public void ShowWorkoutResultsView()
    {
        HideAll();
        workoutResultsView.SetActive(true);
    }
    
    public void ShowprofileView()
    {
        HideAll();
        profileView.SetActive(true);
    }
    public void ShowsyncErgView()
    {
        HideAll();
        syncErgView.SetActive(true);
    }
}
