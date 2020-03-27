using Intero.Workouts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject loginView;
    public GameObject createAccountView;
    public GameObject completeProfileView;
    public GameObject rowingScheduleView;
    public GameObject profileView;
    public GameObject syncErgView;

    public ModalInfoController modalInfo;
    public AuthManager authManager;
    public ScheduleController schedule;
    // public PasswordManager passwordManager;
    private void Start()
    {
        HideAll();
        /*
        if (passwordManager.AreCredentialsSaved())
        {
            passwordManager.ReadCredentials();
            authManager.Login(passwordManager.username,passwordManager.password);
            ShowrowingScheduleView();
        }*/
        CheckLogin();
    }
    async Task CheckLogin() {
        ShowloginView();
        if (authManager.AreCredentialsSaved())
        {
            string error = await authManager.Login();
            print("error!! " + error);
            if (error==null)
            {
                ShowrowingScheduleView();

                List<WorkoutClassJSON> workoutClasses = await authManager.GetWorkoutClasses();
                Dictionary<int, WorkoutJSON> workouts = await authManager.GetWorkoutDic();
                print("got " + workoutClasses);
                print("got " + workouts);
                schedule.ShowWorkouts(workoutClasses, workouts);
            }
            else
            {
                modalInfo.Show(error);
            }
        }
    }

    public void HideAll()
    {
        loginView.SetActive(false);
        createAccountView.SetActive(false);
        completeProfileView.SetActive(false);
        rowingScheduleView.SetActive(false);
        profileView.SetActive(false);
        syncErgView.SetActive(false);
    }

    public void ShowloginView()
    {
        HideAll();
        loginView.SetActive(true);
    }
    public void ShowcreateAccountView()
    {
        HideAll();
        createAccountView.SetActive(true);
    }
    public void ShowcompleteProfileView()
    {
        HideAll();
        completeProfileView.SetActive(true);
    }
    public void ShowrowingScheduleView()
    {
        HideAll();
        rowingScheduleView.SetActive(true);
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
