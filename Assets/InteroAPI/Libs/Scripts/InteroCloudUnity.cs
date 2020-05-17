using Intero.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;




namespace InteroAPI.OAuth
{
    public class InteroCloudUnity : InteroCloud
    {

        [Serializable]
        public class WorkoutClassJSONT
        {
            public string dateStart;
            // public DateTime dateStart;
            // public List<string> usersEnrolled { get; set; }
            public string workoutId;
            public string author;
            public string status;
        }
        public override async Task<string> PostStartWorkoutJSON(int classId)
        {
            UnityEngine.Debug.Log("PostStartWorkoutJSON 1");
            StartWorkoutJSONT startW = new StartWorkoutJSONT(classId);
            string payload = UnityEngine.JsonUtility.ToJson(startW);
            UnityEngine.Debug.Log("PostStartWorkoutJSON 2" + payload);
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            UnityEngine.Debug.Log("PostStartWorkoutJSON 3");
            HttpResponseMessage response = await client.PostAsync(backendAPIAddress + "/user/", content);

            UnityEngine.Debug.Log("PostStartWorkoutJSON 4");
            return await response.Content.ReadAsStringAsync();
        }
        public override async Task<string> PostMessage(int id, int segmentIndex, ErgData e)
        {
            PostMessageT postM = new PostMessageT(id, segmentIndex, e);
            string payload = UnityEngine.JsonUtility.ToJson(postM);
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(backendAPIAddress + "/user/", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return (responseString);
        }
        public override async Task<string> PostMessage(int id, int segmentIndex, StrokeData e)
        {
            PostMessageTS postM = new PostMessageTS(id, segmentIndex, e);
            string payload = UnityEngine.JsonUtility.ToJson(postM);
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(backendAPIAddress + "/user/", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return (responseString);
        }
        // override
        public override async Task<string> GetMessage(string user, string action)
        {
            Debug.Log("GetMessage " + user + " " + action +" ");
            // client.he
            var response = await client.GetAsync(backendAPIAddress + "/user?user=" + user + "&action=" + action);
            var responseString = await response.Content.ReadAsStringAsync();
            return (responseString);
        }
        public override async Task<WorkoutClassJSON[]> GetWorkoutClasses(string user)
        {
            string sJSON = null;
            try
            {
                sJSON = await GetWorkoutClassesJSON(user);
                WorkoutClassMessageJSON jojoT = JsonUtility.FromJson<WorkoutClassMessageJSON>(sJSON);
                WorkoutClassJSONW[] classes = jojoT.classes;
                WorkoutClassJSON[] workoutClasses = new WorkoutClassJSON[classes.Length];
                for (int i = 0; i < classes.Length; i++)
                {
                    workoutClasses[i] = new WorkoutClassJSON(classes[i]);
                }
                return workoutClasses;
            }
            catch (System.Exception e)
            {
                Debug.Log("Eroror!!! " + e.Message);
                Debug.Log("but wanted " + sJSON);
            }

            return (null);
        }
        public override async Task<WorkoutJSON[]> GetWorkouts(string user)
        {
            string sJSON = null;
            try
            {
                sJSON = await GetWorkoutsJSON(user);
                WorkoutMessageJSON jojoT = JsonUtility.FromJson<WorkoutMessageJSON>(sJSON);
                WorkoutJSONW[] workoutJSONWs = jojoT.workouts;
                WorkoutJSON[] workouts = new WorkoutJSON[workoutJSONWs.Length];
                for (int i = 0; i < workoutJSONWs.Length; i++)
                {
                    workouts[i] = new WorkoutJSON(workoutJSONWs[i]);
                }
                return workouts;
            }
            catch (System.Exception e)
            {
                Debug.Log("Eroror!!! " + e.Message);
                Debug.Log("but wanted " + sJSON);
            }

            return (null);
        }
        public override async Task<StartWorkoutJSON> PostStartWorkout(int classId)
        {
            Debug.Log("PostStartWorkout!!! " + classId);
            string s = await PostStartWorkoutJSON(classId);
            Debug.Log("PostStartWorkout!!! 2 " + s);
            StartWorkoutMessageJSON msg = JsonUtility.FromJson<StartWorkoutMessageJSON>(s);
            return (msg.w);
        }
    }
}