using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using InteroAPI.OAuth;

public class CreateAccountManager : MonoBehaviour
{
    public OAuthManager interoCloud;
    public InputField emailInput;
    public InputField userInput;
    public InputField passwordInput;

    public ModalInfoController modalInfo;
    public CanvasController canvasController;

    public void OnCreateAccount()
    {
        CreateAccount();
    }

    public async Task CreateAccount()
    {
        string user = userInput.text;
        string pass = passwordInput.text;
        string email = emailInput.text;
        UnityEngine.Debug.Log(email);
        UnityEngine.Debug.Log(user);
        UnityEngine.Debug.Log(pass);
        print("passwordManager " + interoCloud.passwordManager);
        string error = await interoCloud.Signup(user, email, pass);

        // string error = await interoCloud.Signup(user, email, pass);
        UnityEngine.Debug.Log("ress " + error);
        if(error != null)
        {
            modalInfo.Show(error);
        }
        else
        {
            print("Showign ShowrowingScheduleView");
            canvasController.ShowrowingScheduleView();
        }
    }
}
