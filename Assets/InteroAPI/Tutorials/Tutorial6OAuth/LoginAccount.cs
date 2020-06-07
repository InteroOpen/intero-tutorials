using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using InteroAPI.OAuth;

public class LoginAccount : MonoBehaviour
{
    // public LambdaWorkoutHistory interoCloud;
    public InputField userInput;
    public InputField passwordInput;

    public CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        // print("passwordManager " + interoCloud.passwordManager);
    }
    public void OnLoginAccount()
    {
        Login();
    }

    public async Task Login()
    {
        string user = userInput.text;
        string pass = passwordInput.text;
        UnityEngine.Debug.Log(user);
        UnityEngine.Debug.Log(pass);
        // print("passwordManager " + interoCloud.passwordManager);
        string error = await canvasController.Login(user, pass);

    }
}