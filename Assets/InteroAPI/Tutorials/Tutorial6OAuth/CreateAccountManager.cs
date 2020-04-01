using InteroAPI.OAuth;

using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class CreateAccountManager : MonoBehaviour
{
    public AuthManager interoCloud;
    public InputField emailInput;
    public InputField userInput;
    public InputField passwordInput;

    public ModalInfoController modalInfo;
    public CanvasController canvasController;
    // Start is called before the first frame update
    void Start()
    {

        // interoCloud.
        print("passwordManager " + interoCloud.passwordManager);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreateAccount()
    {
        print("passwordManager " + interoCloud.passwordManager);

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
            // await interoCloud.Login(user, pass);
        }
    }
}
