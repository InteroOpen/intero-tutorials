using InteroAPI.OAuth;

using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class CreateAccountManager : MonoBehaviour
{
    InteroCloud interoCloud = new InteroCloud();
    public InputField emailInput;
    public InputField userInput;
    public InputField passwordInput;

    public ModalInfoController modalInfo;
    // Start is called before the first frame update
    void Start()
    {

        // interoCloud.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        string error = await interoCloud.Signup(user, email, pass);
        UnityEngine.Debug.Log("ress " + error);
        if(error != null)
        {
            modalInfo.Show(error);
        }
    }
}
