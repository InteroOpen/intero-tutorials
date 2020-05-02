using System;
using System.Threading.Tasks;
using UnityEngine;

namespace InteroAPI.OAuth
{
    public class OAuthManager : MonoBehaviour
    {
        public InteroCloudUnity interoCloud = null;
        public PasswordManager passwordManager;
        public string accessToken = null;
        void Awake()
        {
            Debug.Log("AuthManager.Start!!" + passwordManager);
            interoCloud = new InteroCloudUnity();
            passwordManager = new PasswordManager();
#if UNITY_STANDALONE || UNITY_EDITOR
#elif UNITY_ANDROID
        PluginCognito.InitCognito();
#endif
        }

        public async Task<string> Login()
        {
            UnityEngine.Debug.Log("Trying to Login");
            passwordManager.ReadCredentials();
            string username = passwordManager.username;
            string password = passwordManager.password;
            UnityEngine.Debug.Log("Login leyo correcto " + username + " dd " + password);
            return await Login(username, password, false);
        }
        public async Task<string> Login(string username, string password, bool saveCreds = true)
        {
            string s;
#if UNITY_STANDALONE || UNITY_EDITOR
            AWSOAuth awsOAuth = AWSOAuth.GetAWSOAuth();
            Debug.Log("AWSOAuth " + awsOAuth);
            try
            {
            s = await awsOAuth.SignInUser(username, password);
            }
            catch (Exception e)
            {
                s = e.Message;
                Debug.Log("Singing timeout " + s);
            }
            Debug.Log("Singing after " + s);
            if (s == null || s.Length < 1)
                accessToken = awsOAuth.GetOAuthToken();
#elif UNITY_ANDROID
        Debug.Log("Login 6969 android" );
        LoginInfo loginInfo = await PluginCognito.SignIn(username, password);
        s = loginInfo.error == "" ? null : loginInfo.error;
        Debug.Log("Login 6969 error " + s);
        accessToken = loginInfo.idToken;
        Debug.Log("Login 6969 tok " + accessToken);
#endif
            if (s == null || s.Length < 1)
            {

                Debug.Log("Setting access tok " + accessToken);
                interoCloud.SetOAuthHeader(accessToken);
                if (saveCreds)
                    passwordManager.SaveCredentials(username, password);
                Debug.Log("No error " + s + " toc " + accessToken);
            }
            Debug.Log("jojo " + s);
            return s;
        }

        public async Task<string> Signup(string user, string email, string pass)
        {
            AWSOAuth awsOAuth = AWSOAuth.GetAWSOAuth();
            UnityEngine.Debug.Log("password man " + passwordManager);
            passwordManager.SaveCredentials(user, pass);
            string s = null;
#if UNITY_STANDALONE || UNITY_EDITOR
            s = await awsOAuth.Register(user, email, pass);
            if (s != null) accessToken = awsOAuth.GetAccessToken();
#elif UNITY_ANDROID
        LoginInfo loginInfo = await PluginCognito.SignIn(user, pass);
        s = loginInfo.error == "" ? null : loginInfo.error;
        accessToken = loginInfo.accessToken;
#endif
            interoCloud.SetOAuthHeader(accessToken);
            UnityEngine.Debug.Log("error " + s);
            return s;
        }
        public bool AreCredentialsSaved()
        {
            return passwordManager.AreCredentialsSaved();
        }
    }
}


