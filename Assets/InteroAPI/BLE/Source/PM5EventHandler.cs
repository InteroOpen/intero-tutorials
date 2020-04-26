using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intero.Common;
using System.Runtime.InteropServices;

namespace Intero.BLE
{
    public class PM5EventHandler
    {
        public static float Time;
        public static float Distance;
        public static float Power;
        public static float Pace;
        public static float SPM;
        public static float Calhr;
        public static float Calories;
        public static float DriveLength;
        public static float DriveTime;
        public static float StrokeRecoveryTime;
        public static float StrokeRecoveryDistance;
        public static float PeakDriveForce;
        public static float AvgDriveForce;
        public static float StrokeCount;
        // 36
        public static float StrokePower;
        public static float StrokeCalories;
        public static float WorkPerStroke;

#if UNITY_STANDALONE || UNITY_EDITOR
        /*
        public static void SetControllerPM5(BLEReceiver controllerPM5)
        {
            // _controllerPM5 = controllerPM5;
        }*/

        public static void connectToPM5(int channel)
        {
            // PluginInstance.Call("connectToPM5", new object[] {mShareImageCallback, channel});
        }
#elif UNITY_ANDROID
    /*

	const string pluginName = "com.cwgtech.unity.MyPlugin";
	public static BLEReceiver _controllerPM5 = null;
	static ShareImageCallback mShareImageCallback = new ShareImageCallback();

	public static void SetControllerPM5(BLEReceiver controllerPM5){
		_controllerPM5 = controllerPM5;
	}

	public static void connectToPM5(int channel){
		PluginInstance.Call("connectToPM5", new object[] {mShareImageCallback, channel});
	}
	//////

	class ShareImageCallback: AndroidJavaProxy
	{
		public ShareImageCallback() : base (pluginName + "$ShareImageCallback")
		{ }

		public void onShareComplete(int result)
		{
			_controllerPM5.onBLEConnected(" "+result);
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
		){ 
			PM5EventHandler._controllerPM5.onErgDataReady(
				0,
                time,
                distance,
                flags,
                totalWOGDistance,
                totalWOGTime,
                WOGTimeType,
                drag,
                speed,
                SPM,
                heartrate,
                pace,
                avgPace,
                restDistance,
                restTime,
                intervalCount,
                avgPower,
                calories,
                splitAvgPace,
                splitAvgPower,
                splitAvgCalories,
                splitTime,
                splitDistance
			);
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
			){
			PM5EventHandler._controllerPM5.onStrokeDataReady(
			 0,
			 time,    
             distance,
             driveLength,                
             driveTime,                  
             strokeRecoveryTime,         
             strokeDistance,             
             peakDriveForce,             
             avgDriveForce,              
             workPerStroke, 
             strokeCount,             
             strokePower,                
             strokeCalories,             
             projectedWorkTime,          
             projectedWorkDistance
			);
		}

	}

	static AndroidJavaClass _pluginClass;
	static AndroidJavaObject _pluginInstance;

	

	public static AndroidJavaClass PluginClass
	{
		get {
			if (_pluginClass==null)
			{
				_pluginClass = new AndroidJavaClass(pluginName);
				AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
				_pluginClass.SetStatic<AndroidJavaObject>("mainActivity",activity);
			}
			return _pluginClass;
		}
	}

	public static AndroidJavaObject PluginInstance
	{
		get {
			if (_pluginInstance==null)
			{
				_pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
			}
			return _pluginInstance;
		}
	}
    */
#endif
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_ANDROID

        /////
        public static void changeChannel(int ch)
        {

        }
        public static void scanForPM5()
        {

        }
        // ERGDATA
        public static float getTime()
        {
            return Time;
        }

        public static float getDistance()
        {
            return Distance;
        }

        public static float getPower()
        {
            return Power;
        }

        public static float getPace()
        {
            return Pace;
        }

        public static float getSPM()
        {
            return SPM;
        }

        public static float getCalhr()
        {
            return Calhr;
        }

        public static float getCalories()
        {
            return Calories;
        }

        // STROKE DATA
        public static float getDriveLength()
        {
            return DriveLength;
        }

        public static float getDriveTime()
        {
            return DriveTime;
        }

        public static float getStrokeRecoveryTime()
        {
            return StrokeRecoveryTime;
        }

        public static float getStrokeRecoveryDistance()
        {
            return StrokeRecoveryDistance;
        }

        public static float getPeakDriveForce()
        {
            return PeakDriveForce;
        }

        public static float getAvgDriveForce()
        {
            return AvgDriveForce;
        }

        public static float getStrokeCount()
        {
            return StrokeCount;
        }
        public static float getStrokePower()
        {
            return StrokePower;
        }
        public static float getStrokeCalories()
        {
            return StrokeCalories;
        }
        public static float getWorkPerStroke()
        {
            return WorkPerStroke;
        }
        
#elif UNITY_IOS
	public static void SetControllerPM5(BLEReceiver controllerPM5){
		// _controllerPM5 = controllerPM5;
	}

	[DllImport ("__Internal")]
	public static extern void connectToPM5 (int channel);

	[DllImport ("__Internal")]
	public static extern void changeChannel(int ch);

	[DllImport ("__Internal")]
	public static extern void scanForPM5();

	[DllImport ("__Internal")]
	public static extern StrokeData readStrokeData ();

	[DllImport ("__Internal")]
	public static extern ErgData readErgData ();
	// ERGDATA
	[DllImport ("__Internal")]
	public static extern float getTime();

	[DllImport ("__Internal")]
	public static extern float getDistance();

	[DllImport ("__Internal")]
	public static extern float getPower();

	[DllImport ("__Internal")]
	public static extern float getPace();

	[DllImport ("__Internal")]
	public static extern float getSPM();

	[DllImport ("__Internal")]
	public static extern float getCalhr();

	[DllImport ("__Internal")]
	public static extern float getCalories();

	// STROKE DATA
	[DllImport ("__Internal")]
	public static extern float getDriveLength();

	[DllImport ("__Internal")]
	public static extern float getDriveTime();

	[DllImport ("__Internal")]
	public static extern float getStrokeRecoveryTime();

	[DllImport ("__Internal")]
	public static extern float getStrokeRecoveryDistance();

	[DllImport ("__Internal")]
	public static extern float getPeakDriveForce();

	[DllImport ("__Internal")]
	public static extern float getAvgDriveForce();

	[DllImport ("__Internal")]
	public static extern float getStrokeCount();

    [DllImport ("__Internal")]
	public static extern float getStrokePower();
    
    [DllImport ("__Internal")]
	public static extern float getStrokeCalories();

    [DllImport ("__Internal")]
	public static extern float getWorkPerStroke();
#endif

    }
}