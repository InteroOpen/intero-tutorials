
using Intero.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InteroAPI.OAuth
{
    public class LambdaWorkoutHistory : OAuthManager
    {
        public int historyId;
        public int classId;
        public void SetClassId(int classId)
        {
            this.classId = classId;
        }
        public async Task<StartWorkoutJSON> PostStartWorkout()
        {
            Debug.Log("PostStartWorkout");

            StartWorkoutJSON workoutHistory = await interoCloud.PostStartWorkout(classId);
            historyId = workoutHistory.id;
            return workoutHistory;
        }

        public async Task PostMessage(int segmentNum, ErgData e)
        {
            // print("PostMessage" + historyId+ segmentNum+ e);
            Debug.Log("PostMessage");
            await interoCloud.PostMessage(historyId, segmentNum, e);
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
            Debug.Log("getting workouts");
            WorkoutClassJSON[] workoutClasses = await interoCloud.GetWorkoutClasses("rodrigosavage-at-gmail.com");
            Debug.Log("Got workouts " + workoutClasses.Length);
            return workoutClasses;
            //        return c.classes;
        }
    }
}