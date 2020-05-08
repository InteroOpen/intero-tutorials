
using Intero.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
// https://64i5elpzke.execute-api.us-east-2.amazonaws.com/dev/vrow?action=classes
namespace InteroAPI.OAuth
{
    public class LambdaWorkoutHistoryNoAWS : OAuthManager
    {
        public int historyId;
        public int classId;
        public void SetClassId(int classId)
        {
            this.classId = classId;
        }
        void Awake()
        {
            Debug.Log("AuthManager.Start!!" + passwordManager);
            interoCloud = new InteroCloudUnity();
            interoCloud.backendAPIAddress = "https://64i5elpzke.execute-api.us-east-2.amazonaws.com/dev";
            passwordManager = new PasswordManager();
        }
        public async Task<StartWorkoutJSON> PostStartWorkout()
        {
            Debug.Log("PostStartWorkout");
            return null;
            /*
            StartWorkoutJSON workoutHistory = await interoCloud.PostStartWorkout(classId);
            historyId = workoutHistory.id;
            return workoutHistory;
            */
        }
        public void Login(string user)
        {
            passwordManager.username = user;
        }
        public new async Task<string> Login()
        {
            return null;
        }
        public new async Task<string> Login(string username, string password, bool saveCreds = true)
        {
            return null;
        }
        public new async Task<string> Signup(string user, string email, string pass)
        {
            return null;
        }
        public async Task PostMessage(int segmentNum, ErgData e)
        {
            Debug.Log("PostMessage");
            // await interoCloud.PostMessage(historyId, segmentNum, e);
        }

        public async Task<Dictionary<int, WorkoutJSON>> GetWorkoutDic()
        {
            Dictionary<int, WorkoutJSON> workoutDic = new Dictionary<int, WorkoutJSON>();
            Debug.Log("fetching workocouts");
            WorkoutJSON[] workouts = await interoCloud.GetWorkouts("rodrigosavage-at-gmail.com");

            foreach (WorkoutJSON workout in workouts)
            {
                workoutDic.Add(int.Parse(workout.id), workout);
                // UnityEngine.Debug.Log(workout);
            }
            return workoutDic;
        }

        public async Task<WorkoutClassJSON[]> GetWorkoutClasses()
        {
            Debug.Log("getting workouts "+accessToken);
            WorkoutClassJSON[] workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
            Debug.Log("Got workouts " + workoutClasses.Length);
            return workoutClasses;
            //        return c.classes;
        }
    }
}