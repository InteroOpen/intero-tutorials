
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using InteroAPI.OAuth;
using System.Threading.Tasks;
using System;
/*
using Amazon;
using Amazon.CognitoIdentity;
using UnityEngine.UI;
*/
public class FBController 
{
    OAuthManager oAuthManager;

    Action<string> SucessfullLoginCompleted;

    //public Text texto;
    /*CognitoAWSCredentials credentials = new CognitoAWSCredentials(
    "us-east-2:23bc2412-4c10-42de-8919-d5e45f594824", // Identity Pool ID
    RegionEndpoint.USEast2 // Region);
    */

    public FBController(OAuthManager oAuthManager)
    {
        this.oAuthManager = oAuthManager;
    }
    public async Task<string> Login()
    {
        var t = new TaskCompletionSource<string>();
        SucessfullLoginCompleted = (string error) => {
            t.TrySetResult(error);
        };
        LoginCallback();

        return await t.Task;
    }
    public  void LoginCallback() { 
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK      
            FB.Init(InitCallback, OnHideUnity);
            Debug.Log("Inicializado por primera vez");
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
            Debug.Log("Inicializado por segunda vez");
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            var perms = new List<string>() { "public_profile", "email", "user_link"  };
            FB.LogInWithReadPermissions(perms, AuthCallback);

            if (FB.IsLoggedIn)
            { //User already logged in from a previous session
                Debug.Log("User log");
                AddFacebookTokenToCognito();
            }
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            AddFacebookTokenToCognito();

            // Print current access token's User ID
            Debug.Log("toktok : "+aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void GetFacebookInfo(IResult result)
    {
        GetFacebookInfoAsync(result);
    }
    public async Task GetFacebookInfoAsync(IResult result)
    { 
        if (result.Error == null)
        {
            string fbId = result.ResultDictionary["id"].ToString();
            string fbName = result.ResultDictionary["name"].ToString().Replace(" ",".");
            string fbEmail = result.ResultDictionary["email"].ToString();
            Debug.Log("GetFacebookInfo" );
            Debug.Log("GetFacebookInfo" + result.ResultDictionary["name"].ToString());
            Debug.Log("GetFacebookInfo" + result.ResultDictionary["email"].ToString());
            //Debug.Log("GetFacebookInfo" + result.ResultDictionary["user_link"].ToString());
            await oAuthManager.RegisterFB(fbName, fbId, fbEmail);
        }
        else
        {
            Debug.Log(result.Error);
            // result.Error;
        }
        SucessfullLoginCompleted(result.Error);
    }
    void AddFacebookTokenToCognito()
    {
       Debug.Log("AddFacebookTokenToCognito");
       FB.API ("/me?fields=id,name,email,user_link", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string> () { });
        // oAuthManager.LoginFB()
        // oAuthManager.LoginFB
        // Facebook.Unity.em
        // AccessToken.CurrentAccessToken.UserId;
        // AccessToken.CurrentAccessToken.em.UserId;

        // credentials.AddLogin("graph.facebook.com", AccessToken.CurrentAccessToken.TokenString);
        // texto.text = "Datos enviados aws";
    }

}