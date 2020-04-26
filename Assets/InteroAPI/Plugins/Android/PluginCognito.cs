using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoginInfo
{
    public string username;
    public string password;
    public string idToken;
    public string accessToken;
    public string refreshToken;
    public string error;
    public LoginInfo(string error, string accessToken, string idToken, string refreshToken)
    {
        this.error = error;
        this.accessToken = accessToken;
        this.idToken = idToken;
        this.refreshToken = refreshToken;
    }
}
public class PluginCognito
{
    public const string pluginName = "com.intero.unity.CognitoManager";

    static AndroidJavaClass pluginClass = null;
    static AndroidJavaObject pluginInstance = null;
    static AndroidJavaObject activity = null;

    static CognitoCallback cognitoCallback = null;

    public static void InitCognito()
    {
        Debug.Log("Start PluginCognito Unity");
        pluginClass = new AndroidJavaClass(pluginName);
        pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
        Debug.Log("Start PluginCognito Instance Unity" + pluginInstance);
        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            cognitoCallback = new CognitoCallback();
            Debug.Log("Start PluginCognito Instance Unity runOnUiThread" + cognitoCallback);

            pluginInstance.Call("Init", new object[] { activity, cognitoCallback });
            Debug.Log("Start PluginCognito Instance Unity initialized" + cognitoCallback);
        }));
    }

    public static void SignIn(string user, string password, Action<LoginInfo> onCompleted)
    {
        Debug.Log("Unity Intero SignIn");
        pluginInstance.Call("SignIn", new object[] { user, password });
        cognitoCallback.onCompletedSignIn = onCompleted;
    }

    public static void SignUp(string user, string password, Action<LoginInfo> onCompleted)
    {
        Debug.Log("Unity Intero SignUp");
        pluginInstance.Call("SignUp", new object[] { user, password });
        cognitoCallback.onCompletedSignUp = onCompleted;
    }

    public static async Task<LoginInfo> SignIn(string user, string password)
    {
        Debug.Log("Unity Intero SignIn 6969");

        var t = new TaskCompletionSource<LoginInfo>();
        SignIn(user, password, (LoginInfo s) => {
            s.username = user;
            s.password = password;
            // print("unity log success "+ user);
            t.TrySetResult(s);
        });
        return await t.Task;
    }
    public static async Task<LoginInfo> SignUp(string user, string password)
    {
        // return Task.Run(() =>
        // {
        var t = new TaskCompletionSource<LoginInfo>();
        SignUp(user, password, (LoginInfo s) => {
            s.username = user;
            s.password = password;
            t.TrySetResult(s);
        });
        return await t.Task;
        //`});
    }
    public class CognitoCallback : AndroidJavaProxy
    {
        public Action<LoginInfo> onCompletedSignIn = null;
        public Action<LoginInfo> onCompletedSignUp = null;
        public CognitoCallback() : base(pluginName + "$CognitoListener")
        { }

        public void onSignIn(string error, string accessToken, string idToken, string refreshToken)
        {
            LoginInfo info = new LoginInfo(error, accessToken, idToken, refreshToken);
            Debug.Log("intero.onSignIn From unity " + error + " idToken " + idToken + " accessToken " + accessToken);
            onCompletedSignIn(info);
        }
        public void onSignUp(string error)
        {
            LoginInfo info = new LoginInfo(error, "", "", "");

            Debug.Log("intero.onSignUp From unity " + error);
            onCompletedSignUp(info);

        }
    }

}