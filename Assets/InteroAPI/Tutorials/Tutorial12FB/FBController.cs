
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
/*
using Amazon;
using Amazon.CognitoIdentity;
using UnityEngine.UI;
*/
public class FBController 
{
    //public Text texto;
    /*CognitoAWSCredentials credentials = new CognitoAWSCredentials(
    "us-east-2:23bc2412-4c10-42de-8919-d5e45f594824", // Identity Pool ID
    RegionEndpoint.USEast2 // Region);
    */
    void InitFB()
    {
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
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);

            if (FB.IsLoggedIn)
            { //User already logged in from a previous session
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
            Debug.Log("AuthCallback" + result.ResultDictionary["id"].ToString());
            Debug.Log("AuthCallback" + result.ResultDictionary["name"].ToString());
            Debug.Log("AuthCallback" + result.ResultDictionary["email"].ToString());
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
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

    /* void FacebookLoginCallback(FBResult result)
     {
         if (FB.IsLoggedIn)
         {
             AddFacebookTokenToCognito();
         }
         else
         {
             Debug.Log("FB Login error");
         }
     */
    public void GetFacebookInfo(IResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("GetFacebookInfo" +result.ResultDictionary["id"].ToString());
            Debug.Log("GetFacebookInfo" + result.ResultDictionary["name"].ToString());
            Debug.Log("GetFacebookInfo" + result.ResultDictionary["email"].ToString());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    void AddFacebookTokenToCognito()
    {
       FB.API ("/me?fields=id,name,email", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string> () { });
        
       // Facebook.Unity.em
       // AccessToken.CurrentAccessToken.UserId;
       // AccessToken.CurrentAccessToken.em.UserId;

        // credentials.AddLogin("graph.facebook.com", AccessToken.CurrentAccessToken.TokenString);
        // texto.text = "Datos enviados aws";
    }

}