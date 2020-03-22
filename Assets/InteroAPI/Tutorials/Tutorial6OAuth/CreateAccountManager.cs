using InteroAPI.OAuth;

using UnityEngine;
using UnityEngine.UI;

public class CreateAccountManager : MonoBehaviour
{
    InteroCloud interoCloud = new InteroCloud();
    public InputField emailInput;
    public InputField passwordInput;
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
        UnityEngine.Debug.Log(emailInput.text);
    }
}
