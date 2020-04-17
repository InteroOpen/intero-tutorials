using Intero.Workouts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
    public AuthManager authManager;
    public ScheduleController schedule;
    // public PasswordManager passwordManager;
    private void Start()
    {
        HideAll();
        CheckLogin();
    }
    public Text testOut;
    async Task LoadWorkouts()
    {
        testOut.text = "loading workouts...";
        testOut.text = "loading " + authManager.interoCloud.GetOAuthToken();
        List<WorkoutClassJSON> workoutClasses = await authManager.GetWorkoutClasses();
        Dictionary<int, WorkoutJSON> workouts = await authManager.GetWorkoutDic();
        testOut.text = "loading Finished..";
        testOut.text = "N workout " + workoutClasses.Count;
        print("got LoadWorkouts " + workoutClasses.Count);
        print("got " + workouts);

         schedule.ShowWorkouts(workoutClasses, workouts);
    }

    async Task CheckLogin() {
        ShowloginView();
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

    public void HideAll()
    {
        loginView.SetActive(false);
        loginAccountView.SetActive(false);

        createAccountView.SetActive(false);
        completeProfileView.SetActive(false);
        rowingScheduleView.SetActive(false);
        workoutSumaryView.SetActive(false);
        workoutResultsView.SetActive(false);

        profileView.SetActive(false);
        syncErgView.SetActive(false);
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
