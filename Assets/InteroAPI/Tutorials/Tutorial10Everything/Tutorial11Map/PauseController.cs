using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject panel;
    public WorkoutManager wogManager;
    // Start is called before the first frame update
    public void ShowPauseMenu(bool value)
    {
        panel.SetActive(value);
    }
    public void ContinueRowing()
    {
        ShowPauseMenu(false);

    }
    public void StopWorkout()
    {
        ShowPauseMenu(false);
        wogManager.EndWorkout();
    }

}
