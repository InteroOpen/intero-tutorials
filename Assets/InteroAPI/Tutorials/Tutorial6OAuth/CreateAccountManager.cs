using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using InteroAPI.OAuth;

public class CreateAccountManager : MonoBehaviour
{
    // public OAuthManager interoCloud;
    public InputField emailInput;
    public InputField userInput;
    public InputField passwordInput;

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
        string error = await canvasController.Signup(user, email, pass);
    }
}
