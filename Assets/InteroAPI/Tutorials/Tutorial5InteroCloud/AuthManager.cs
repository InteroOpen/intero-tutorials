using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using InteroAPI.OAuth;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("AuthManager.Start!!");
        // TestUserOAuth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async Task TestUserOAuth()
    {

        InteroCloud interoCloud = new InteroCloud();
        //string userName = "rodrigosavage-at-gmail.com";
        // string userPass = "rtopdfrtio";
        await interoCloud.OAuth("rodrigosavage-at-gmail.com", "rtopdfrtio");
        var values = new Dictionary<string, string>
            {
               { "thing1", "hello" },
               { "thing2", "world" }
            };
        var responseString = await interoCloud.SendMessage(values);
        UnityEngine.Debug.Log(responseString);
    }
}
