using System;
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
        // override
        public override async Task<WorkoutClassJSON[]> GetWorkoutClasses(string user)
        {
            string sJSON = null;
            try
            {
                Debug.Log("meow " + user);
                Debug.Log("meow2 " + backendAPIAddress);
                sJSON = await GetWorkoutClassesJSON(user);
                Debug.Log("Eroror!!!691 1 "+ sJSON);

                WorkoutClassMessageJSON jojoT = JsonUtility.FromJson<WorkoutClassMessageJSON>(sJSON);
                Debug.Log("Eroror!!!691 2");
                WorkoutClassJSONW[] classes = jojoT.classes;
                Debug.Log("Eroror!!!691 3"); 
                WorkoutClassJSON[] workoutClasses = new WorkoutClassJSON[classes.Length];
                Debug.Log("Eroror!!!691 4");
                for (int i = 0; i < classes.Length; i++)
                {
                    Debug.Log("Eroror!!!691 tt"+i);
                    workoutClasses[i] = new WorkoutClassJSON(classes[i]);
                }
                return workoutClasses;
            }
            catch (System.Exception e)
            {
                Debug.Log("Eroror!!!691 " + e.Message);
                Debug.Log("but wanted " + sJSON);
            }

            return (null);
        }
        public new async Task<string> GetMessage(string user, string action)
        {

            Debug.Log("Sending!!!691 tt" + user + "  " +action );
            var response = await client.GetAsync(backendAPIAddress + "/user?user=" + user + "&action=" + action);
            Debug.Log("Sent!!!!691 tt"  + "  " + response.Content);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.Log("got!!!!691 tt" + "  " + responseString);

            return (responseString);
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
                Debug.Log("Eroror!!!692 " + e.Message);
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