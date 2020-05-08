using Intero.Common;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class PluginBLEPM5
{
    public const string pluginName = "com.intero.unity.InteroAndroidPlugin";
    static AndroidJavaClass pluginClass = null;
    static AndroidJavaObject pluginInstance = null;
    static AndroidJavaObject activity = null;
    static BLECallback bleCallback = null;


    public static void InitBLEPM5(int channel)
    {
        Debug.Log("Start Intero InitBLEPM5 Unity");
        pluginClass = new AndroidJavaClass(pluginName);
        pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
        Debug.Log("Start Intero Instance Unity" + pluginInstance);
        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        pluginClass.SetStatic<AndroidJavaObject>("mainActivity", activity);
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            bleCallback = new BLECallback();
            pluginInstance.Call("initBLEPM5", new object[] { bleCallback, channel });
        }));
    }
    public static void StartScanning(Action<int> onCompleted, Action<ErgData> onCompletedErgData, Action<StrokeData> onCompletedStrokeData)
    {
        Debug.Log("Unity Intero StartScanning");
        pluginInstance.Call("startScanning");
        bleCallback.onConnected = onCompleted;
        bleCallback.onCompletedErgData = onCompletedErgData;
        bleCallback.onCompletedStrokeData = onCompletedStrokeData;

    }
    public static async Task<int> StartScanning(Action<ErgData> onCompletedErgData, Action<StrokeData> onCompletedStrokeData)
    {
        TaskCompletionSource<int> t = new TaskCompletionSource<int>();
        StartScanning( (s) => t.TrySetResult(s), onCompletedErgData, onCompletedStrokeData);
        return await t.Task;
    }

    public class BLECallback : AndroidJavaProxy
    {
        public Action<int> onConnected;
        public Action<ErgData> onCompletedErgData;
        public Action<StrokeData> onCompletedStrokeData;

        ErgData ergData = new ErgData();
        StrokeData strokeData = new StrokeData();
        public BLECallback() : base(pluginName + "$BLEListener")
        { }
        public void onConnectedToPM5(int result)
        {
            Debug.Log("onConnectedToPM5 From unity "+result);
            onConnected(result);
        }
        public void onErgDataReady(
            float time,
            float distance,
            float flags,
            float totalWOGDistance,
            float totalWOGTime,
            float WOGTimeType,
            float drag,
            // 32
            float speed,               // Speed
            float SPM,                 // Stroke rate
            float heartrate,           // Heartrate ToDo in emulator and BLE
            float pace,                // Current Pace
            float avgPace,             // Average Pace ToDo in emulator and BLE
            float restDistance,        // Rest Distance ToDo in emulator and BLE
            float restTime,            // Rest Time ToDo in emulator and BLE
                                       // 33
            int intervalCount,         // Interval Count ToDo in emulator and BLE
            float avgPower,            // Average Power ToDo in emulator and BLE
            float calories,            // Total calories
            float splitAvgPace,       // Split Average Place ToDo in emulator and BLE
            float splitAvgPower,       // split Average Power ToDo in emulator and BLE
            float splitAvgCalories,    // Split Average Calories ToDo in emulator and BLE
            float splitTime,           // Split Time ToDo in emulator and BLE
            float splitDistance       // Split Distance ToDo in emulator and BLE
        )
        {
            Debug.Log("Intero onErgDataReady From unity " + time + " " + distance);
            ergData.i = 0;
            ergData.time = time;
            ergData.distance = distance;
            ergData.flags = flags;
            ergData.totalWOGDistance = totalWOGDistance;
            ergData.totalWOGTime = totalWOGTime;
            ergData.WOGTimeType = WOGTimeType;
            ergData.drag = drag;
            // 32
            ergData.speed = speed;
            ergData.spm = SPM;
            ergData.heartrate = heartrate;
            ergData.pace = pace;
            ergData.avgPace = avgPace;
            ergData.restDistance = restDistance;
            ergData.restTime = restTime;
            ergData.intervalCount = intervalCount;
            ergData.avgPower = avgPower;
            ergData.calories = calories;
            ergData.splitAvgPace = splitAvgPace;
            ergData.splitAvgPower = splitAvgPower;
            ergData.splitAvgCalories = splitAvgCalories;
            ergData.splitTime = splitTime;
            ergData.splitDistance = splitDistance;
            onCompletedErgData(ergData);
        }
        public void onStrokeDataReady(
            float time,
            float strokeCount,                 // Stroke Count
                                               // Stroke Data 35
            float distance,
            float driveLength,                 // Drive Lentgth
            float driveTime,                   // Drive Time
                                               // Recovery
            float strokeRecoveryTime,          // Stroke Recovery Time
            float strokeDistance,              // Stroke Distance ToDo in emulator and BLE
                                               //
            float peakDriveForce,              // Peak Drive Force
            float avgDriveForce,               // Average Drive Force
            float workPerStroke,               // Work per stroke ToDo in emulator and BLE
                                               // 36
            float strokePower,                 // Stroke Power
            float strokeCalories,              // Stroke Calories ToDo in emulator and BLE
            float projectedWorkTime,           // Projected Work Time ToDo in emulator and BLE
            float projectedWorkDistance       // Projected Work Distance ToDo in emulator and BLE
            )
        {
            Debug.Log("Intero onStrokeDataReady From unity " + time + " " + distance);
            strokeData.i = 0;
            strokeData.time = time;
            strokeData.distance = distance;
            strokeData.driveLength = driveLength;
            strokeData.driveTime = driveTime;
            strokeData.strokeRecoveryTime = strokeRecoveryTime;
            strokeData.strokeRecoveryDistance = strokeDistance;
            strokeData.peakDriveForce = peakDriveForce;
            strokeData.avgDriveForce = avgDriveForce;
            strokeData.workPerStroke = workPerStroke;
            strokeData.strokeCount = strokeCount;
            strokeData.strokePower = strokePower;
            strokeData.strokeCalories = strokeCalories;
            onCompletedStrokeData(strokeData);
        }
    }
}