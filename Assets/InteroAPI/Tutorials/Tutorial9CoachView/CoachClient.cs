using Intero.TCP;
using UnityEngine;
using Intero.Common;

public class CoachClient : MonoBehaviour
{
    InteroClientTCP client = null;
    public GameObject mainScene = null;
    public GameObject UIMenu = null;
    // Start is called before the first frame update
    void Start()
    {
        StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        if (client != null) client.unityThread.Update();
    }
    public void StartClient()
    {
        client = new InteroClientTCP();
        client.username = "coach";
        client.Connect(8080);
        print("Connecting to Server");
    }
    public void StartWorkout()
    {
        client.SendMessage(OSCErgDataManager.StartWorkoutMessage());
        mainScene.SetActive(true);
        UIMenu.SetActive(false);
    }
}
