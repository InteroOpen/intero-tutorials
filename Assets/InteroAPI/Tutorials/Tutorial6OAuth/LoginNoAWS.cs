using InteroAPI.OAuth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginNoAWS : MonoBehaviour
{

    public CanvasController canvasController;
    public LambdaWorkoutHistoryNoAWS lambda;
    public void Login(string user)
    {
        lambda.Login(user);
        canvasController.ShowrowingScheduleView();
    }
}
