using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassUiItem : MonoBehaviour{

    //Class name
    public Text className;

    //Class time
    public Text classStart;

    //Class day
    public Text Day;

    //Id to the other scene
    public string idItem;

    public void ChangeScene()
    {
        SingletonWorkouts.instancia.id = idItem;
        SceneManager.LoadScene("LobbyEscene");
    }
}
