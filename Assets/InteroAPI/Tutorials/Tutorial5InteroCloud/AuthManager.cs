
using System.Collections.Generic;
using System.Threading.Tasks;
using InteroAPI.OAuth;
using UnityEngine;
using Intero.Common;
using Intero.Workouts;
// using static InteroAPI.OAuth.InteroCloud;

using System.Net.Http;
using System.Net;
using System;
using Newtonsoft.Json;

public class TestInteroCloud : InteroCloud
{
    public class ContributorClassT
    {
        public List<WorkoutClassJSON> classes { get; set; }
        public HttpResponseMessage response;
        public override string ToString()
        {
            return classes[0] + "";
        }
        public HttpStatusCode code;

 //       public string response;
    }
    // override
    public new async Task<ContributorClassT> GetWorkoutClasses(string user)
    {
        Debug.Log("GetWorkoutClasses " + user);
        HttpResponseMessage response = await client.GetAsync(backendAPIAddress + "/user?action=workoutclass&user=" + user);
        Debug.Log("GetWorkoutClasses1 " + response.ToString());
        Debug.Log("GetWorkoutClasses2 " + response.StatusCode);

        ContributorClassT contributors = null;
        Debug.Log("GetWorkoutClasses3 ");
        try
        {
            // contributors = response.Content.ReadAsAsync<ContributorClassT>().Result;
            string s = response.Content.ReadAsStringAsync().Result;
            Debug.Log("GetWorkoutClasses3.2 " + s);
            contributors = JsonConvert.DeserializeObject<ContributorClassT>(s);
            Debug.Log("GetWorkoutClasses3.4 pass " + contributors.classes[0].author);

        }
        catch (Exception e)
        {

            Debug.Log("GetWorkoutClasses3.5 err "+e.Message);


        }
        Debug.Log("GetWorkoutClasses4 ");
        contributors.code = response.StatusCode;
        contributors.response = response;
        Debug.Log("JOjijo " + response.StatusCode);

        return (contributors);
    }
}


 public class AuthManager : MonoBehaviour
{
    public TestInteroCloud interoCloud = null;
    public PasswordManager passwordManager;
    public string accessToken = null;
    public void Awake()
    {
        interoCloud = new TestInteroCloud();
        passwordManager = new PasswordManager();
        Debug.Log("AuthManager.Start!!" + passwordManager);
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
        s = await awsOAuth.SignInUser(username, password);
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
        if (s == null || s.Length < 1) accessToken = awsOAuth.GetAccessToken();
#elif UNITY_ANDROID
        LoginInfo loginInfo = await PluginCognito.SignIn(user, pass);
        s = loginInfo.error == "" ? null : loginInfo.error;
        accessToken = loginInfo.accessToken;
#endif
        if (s == null || s.Length < 1)
        {
            interoCloud.SetOAuthHeader(accessToken);
            UnityEngine.Debug.Log("error " + s);
        }
        return s;
    }
    public bool AreCredentialsSaved()
    {
        return passwordManager.AreCredentialsSaved();
    }
}

/*
public int historyId;
public int classId;
public void SetClassId(int classId)
{
    this.classId = classId;
}
public async Task PostStartWorkout()
{
    StartWorkoutJSON workoutHistory = await interoCloud.PostStartWorkout(classId);
    historyId = workoutHistory.id;
}

public async Task PostMessage(int segmentNum, ErgData e)
{
    // print("PostMessage" + historyId+ segmentNum+ e);

    await interoCloud.PostMessage(historyId, segmentNum, e);
}

public async Task<Dictionary<int, WorkoutJSON>> GetWorkoutDic()
{
    Dictionary<int, WorkoutJSON> workoutDic = new Dictionary<int, WorkoutJSON>();
    print("fetching workocouts");
    List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

    foreach (WorkoutJSON workout in workouts)
    {
        workoutDic.Add(int.Parse(workout.id), workout);
        // UnityEngine.Debug.Log(workout);
    }
    return workoutDic;
}
public async Task<List<WorkoutClassJSON>> GetWorkoutClasses()
{
    List<WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
    // ShowWorkouts(workoutClasses, workoutDic);
    return workoutClasses;
}
*/


/*
 * using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using InteroAPI.OAuth;
using UnityEngine;
using Intero.Common;
using static InteroAPI.OAuth.InteroCloud;
using Intero.Workouts;
using System;
using Amazon;

public class AuthManager : MonoBehaviour
{
    public InteroCloud interoCloud = null;
    public WorkoutManager workoutManager;
    public int historyId;
    public int classId;
    public PasswordManager passwordManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AuthManager.Start!!" + passwordManager);
        interoCloud = new InteroCloud();
        // if android
#if UNITY_ANDROID
        PluginCognito.InitCognito();
#endif
    }

    public async Task<string> Login()
    {
        print("Trying to Login");
        passwordManager.ReadCredentials();
        string username = passwordManager.username;
        string password = passwordManager.password;
        print("Login leyo correcto "+ username + " dd " + password);


        string s;
#if UNITY_ANDROID
        LoginInfo loginInfo = await PluginCognito.SignIn(username, password);
        s = loginInfo.error == "" ? null: loginInfo.error;
        interoCloud.SetOAuthHeader(loginInfo.accessToken);
#else
        s = await interoCloud.OAuth(username, password);
#endif

        print("error " + s);
        return s;
    }
    public async Task<string> Login(string username, string password)
    {
        print("Trying to Login");
        // passwordManager.ReadCredentials();
        //string username = passwordManager.username;
        //string password = passwordManager.password;
        print("Login leyo correcto " + username + " dd " + password);
        passwordManager.SaveCredentials(username, password);

        string s;
#if UNITY_ANDROID
        LoginInfo loginInfo = await PluginCognito.SignIn(username, password);
        s = loginInfo.error == "" ? null : loginInfo.error;
        interoCloud.SetOAuthHeader(loginInfo.accessToken);
#else
        s = await interoCloud.OAuth(username, password);
#endif

        print("error " + s);

        return s;
    }
    public async Task<string> Signup(string user, string email, string pass)
    {
        print("password man " + passwordManager);
        print("joojo");
        passwordManager.SaveCredentials(user, pass);
        string s = null;
#if UNITY_ANDROID
        LoginInfo loginInfo = await PluginCognito.SignIn(user, pass);
        s = loginInfo.error == "" ? null : loginInfo.error;
        interoCloud.SetOAuthHeader(loginInfo.accessToken);
#else
        s = await interoCloud.Signup(user, email, pass);
#endif
        print("error "+s);
        return s;
    }
    public bool AreCredentialsSaved()
    {
        return passwordManager.AreCredentialsSaved();
    }
    public void SetClassId(int classId)
    {
        this.classId = classId;
        print("Setting SetClassId id " + classId);

    }
    public async Task PostStartWorkout()
    {
        StartWorkoutJSON workoutHistory = await interoCloud.PostStartWorkout(classId);
        historyId = workoutHistory.id;
        print("Setting history id " + historyId);
    }

    public async Task PostMessage(int segmentNum, ErgData e)
    {
        // print("PostMessage" + historyId+ segmentNum+ e);

        await interoCloud.PostMessage(historyId, segmentNum, e);
    }

    public async Task<Dictionary<int, WorkoutJSON>> GetWorkoutDic()
    {
        Dictionary<int, WorkoutJSON> workoutDic = new Dictionary<int, WorkoutJSON>();
        print("fetching workocouts");
        List<WorkoutJSON> workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

        foreach (WorkoutJSON workout in workouts)
        {
            workoutDic.Add(int.Parse(workout.id), workout);
            // UnityEngine.Debug.Log(workout);
        }
        return workoutDic;
    }
    public async Task<List<WorkoutClassJSON>> GetWorkoutClasses()
    {
        List<WorkoutClassJSON> workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
        // ShowWorkouts(workoutClasses, workoutDic);
        return workoutClasses;
    }

}
*/
