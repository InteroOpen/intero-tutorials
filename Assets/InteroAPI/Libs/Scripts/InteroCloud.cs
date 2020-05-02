        /*
using InteroAPI.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


public class InteroCloud
    {
        protected static readonly HttpClient client = new HttpClient();
        protected const string backendAPIAddress = "https://pni2tv862g.execute-api.us-east-2.amazonaws.com/dev/";

    public void SetOAuthHeader(string key)
    {
        client.DefaultRequestHeaders.Add("authorization", key);
        client.DefaultRequestHeaders.Add("X-Amz-Date", DateTime.UtcNow.ToLongDateString());
    }

    public async Task<string> PostMessage(Dictionary<string, string> d)
    {
        var content = new FormUrlEncodedContent(d);
        var response = await client.PostAsync(backendAPIAddress + "/user/", content);
        var responseString = await response.Content.ReadAsStringAsync();
        return (responseString);
    }
    public async Task<string> GetMessage(string action)
    {
        var response = await client.GetAsync(backendAPIAddress + "/user?action=" + action);
        var responseString = await response.Content.ReadAsStringAsync();
        return (responseString);
    }

    public virtual Task<WorkoutJSON[]> GetWorkouts(string user);

        public async Task<WorkoutJSON[]> GetWorkouts(string user)
        {
            var response = await client.GetAsync(backendAPIAddress + "/user?action=workout&user=" + user);
            
            // var responseString = await response.Content.ReadAsA();
            // Contributor contributors;
            // contributors = response.Content.ReadAsAsync<Contributor>().Result;
            // var responseString = contributors.ToString();// await response.Content.ReadAsStringAsync();
            // return (contributors.workouts);

        }

        public new async Task<WorkoutClassJSON[]> GetWorkoutClasses(string user)
        {
           
            HttpResponseMessage response = await client.GetAsync(backendAPIAddress + "/user?action=workoutclass&user=" + user);
            // Debug.Log("GetWorkoutClasses " + response.ToString());
            // Debug.Log("GetWorkoutClasses " + response.StatusCode);

            // ContributorClass contributors = null;
            try
            {
            // contributors = response.Content.ReadAsAsync<ContributorClass>().Result;

            // contributors.code = response.StatusCode;
            // contributors.response = response.ToString();
                string s = response.Content.ReadAsStringAsync().Result;
            }
            catch (System.Exception e)
            {

                // Debug.Log("Eroror!!! " + e.Message);
            }

            return (contributors.classes);
        }
    public async Task<List<WorkoutClassJSON>> GetWorkoutClasses(string user)
        {
            var response = await client.GetAsync(backendAPIAddress + "/user?action=workoutclass&user=" + user);
            ContributorClass contributors;
            contributors = response.Content.ReadAsAsync<ContributorClass>().Result;
            return (contributors.classes);
        }
        public class StartWorkoutJSON
        {
            public string dateStart { get; set; }
            // public List<string> segmentStatistic { get; set; }
            public int status { get; set; }
            public int id { get; set; }
            public string username { get; set; }
            public string workoutClassId { get; set; }
        }
        internal class ContributorStartWorkoutJSON
        {
            public StartWorkoutJSON w { get; set; }
        }
        public async Task<StartWorkoutJSON> PostStartWorkout(int classId)
        {
            string payload = JsonConvert.SerializeObject(new
            {
                action = "workouthistory",

                actionBody = new
                {
                    action = "classStart",
                    classId,
                }
            });

            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(backendAPIAddress + "/user/", content);
            // var response = await client.GetAsync(backendAPIAddress + "/user?action=workoutclass&user=" + user);
            ContributorStartWorkoutJSON contributors;
            contributors = response.Content.ReadAsAsync<ContributorStartWorkoutJSON>().Result;

            // contributors = await response.Content.ReadAsAsync<ContributorClass>().Result;

            return (contributors.w);
        }
        public async Task<string> PostMessage(int id, int segmentIndex, ErgData e)
        {
            string payload = JsonConvert.SerializeObject(new
            {
                action = "workouthistory",

                actionBody = new
                {
                    action = "segmentTrack",
                    id,
                    segmentIndex,
                    ergData = new
                    {
                        e.time,
                        e.distance,
                        e.flags,
                        e.totalWOGDistance,
                        e.totalWOGTime,
                        e.WOGTimeType,
                        e.drag,
                        e.speed,
                        e.spm,
                        e.heartrate,
                        e.pace,
                        e.avgPace,
                        e.restDistance,
                        e.restTime,
                        e.intervalCount,
                        e.avgPower,
                        e.calories,
                        e.splitAvgPace,
                        e.splitAvgPower,
                        e.splitAvgCalories,
                        e.splitTime,
                        e.splitDistance
                    }
                },
            });
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(backendAPIAddress + "/user/", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return (responseString);
        }
    public async Task<string> SendMessage(Segment e)
    {
        var ergDataDic = new Dictionary<string, string>
        {
            { "time", e. + ""},
            // 31
            { "distance", e.distance + ""},
            { "flags", e.flags + ""},
            { "totalWOGDistance", e.totalWOGDistance + ""},
            { "totalWOGTime", e.totalWOGTime + ""},
            { "WOGTimeType", e.WOGTimeType + ""},
            { "drag", e.drag + ""},
            // 32
            { "speed", e.speed + ""},               		// Speed
            { "spm", e.spm + ""},                 		// Stroke rate
            { "heartrate", e.heartrate + ""},           	// Heartrate ToDo in emulator and BLE
            { "pace", e.pace + ""},                		// Current Pace 
            { "avgPace", e.avgPace + ""},             		// Average Pace ToDo in emulator and BLE
            { "restDistance", e.restDistance + ""},        		// Rest Distance ToDo in emulator and BLE
            { "restTime", e.restTime + ""},            		// Rest Time ToDo in emulator and BLE
                                                    // 33
            { "intervalCount", e.intervalCount + ""},         		// Interval Count ToDo in emulator and BLE
            { "avgPower", e.avgPower + ""},            		// Average Power ToDo in emulator and BLE
            { "calories", e.calories + ""},            		// Total calories
            { "splitAvgPace", e.splitAvgPace + ""},       		// Split Average Place ToDo in emulator and BLE
            { "splitAvgPower", e.splitAvgPower + ""},       		// split Average Power ToDo in emulator and BLE
            { "splitAvgCalories", e.splitAvgCalories + ""},    		// Split Average Calories ToDo in emulator and BLE
            { "splitTime", e.splitTime + ""},           		// Split Time ToDo in emulator and BLE
            { "splitDistance", e.splitDistance + ""},
        };
        var content = new FormUrlEncodedContent(ergDataDic);
        var response = await client.PostAsync(backendAPIAddress + "/user/", content);
        // Console.WriteLine(response);
        var responseString = await response.Content.ReadAsStringAsync();

        return (responseString);
    }
    }
}
    */
