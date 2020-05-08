using UnityEngine;
using Intero.Common;
using Intero.Events;

namespace Intero.BLE
{
    public class BLEReceiver : MonoBehaviour
    {
        ErgData ergData = new ErgData();
        StrokeData strokeData = new StrokeData();

        void Awake()
        {
            StartBLE(0);
        }

        public void StartBLE(int channel)
        {
#if UNITY_STANDALONE || UNITY_EDITOR

#elif UNITY_ANDROID
			PluginBLEPM5.InitBLEPM5(0);
#else
            PM5EventHandler.connectToPM5(channel);
#endif
		}
		ErgDataEvent ergDataEvent = null;
		StrokeDataEvent strokeDataEvent = null;
		bool erdataReady, strokeDataReady;
		public void ScanDevices()
		{
#if UNITY_STANDALONE || UNITY_EDITOR
#elif UNITY_ANDROID
			PluginBLEPM5.StartScanning((ErgData a) => {
				ergDataEvent = new ErgDataEvent(a);
				erdataReady = true;
				print("INTERO_BLE ergdata " + a);
			}, (StrokeData a) => {
				strokeDataEvent = new StrokeDataEvent(a);
				strokeDataReady = true;
				print("INTERO_BLE stroke " + a);
			});
#endif
		}
		private void Update()
		{
			if (erdataReady)
			{
				InteroEventManager.GetEventManager().SendEvent(ergDataEvent);
				erdataReady = false;
			}
			if (strokeDataReady)
			{
				InteroEventManager.GetEventManager().SendEvent(strokeDataEvent);
				strokeDataReady = false;
			}

		}

		void onBLEOn(string s)
        {
            InteroEventManager.GetEventManager().SendEvent(new BLEOnEvent());
        }

        public void onBLEConnected(string s)
        {
            InteroEventManager.GetEventManager().SendEvent(new BLEConnectedEvent());
        }

        void onBLEOff(string s)
        {
            InteroEventManager.GetEventManager().SendEvent(new BLEOffEvent());
        }

        public void onBLEDisconnected(string s)
        {
            InteroEventManager.GetEventManager().SendEvent(new BLEDisconnectedEvent());
        }

        public void onErgDataReady(string s)
        {
            ergData.time = PM5EventHandler.getTime();
            ergData.distance = PM5EventHandler.getDistance();
            ergData.avgPower = PM5EventHandler.getPower();
            ergData.pace = PM5EventHandler.getPace();
            ergData.spm = PM5EventHandler.getSPM();
            ergData.splitAvgCalories = PM5EventHandler.getCalhr();
            ergData.calories = PM5EventHandler.getCalories();
            // ErgData ergData = new ErgData(10, 100, 21, 200, 122);
            ErgDataEvent e = new ErgDataEvent(ergData);
            InteroEventManager.GetEventManager().SendEvent(e);
        }

        public void onStrokeDataReady(string s)
        {
            // 35
            strokeData.time = PM5EventHandler.getTime();
            strokeData.distance = PM5EventHandler.getDistance();
            strokeData.driveLength = PM5EventHandler.getDriveLength();
            strokeData.driveTime = PM5EventHandler.getDriveTime();
            strokeData.strokeRecoveryTime = PM5EventHandler.getStrokeRecoveryTime();
            strokeData.strokeRecoveryDistance = PM5EventHandler.getStrokeRecoveryDistance();
            strokeData.peakDriveForce = PM5EventHandler.getPeakDriveForce();
            strokeData.avgDriveForce = PM5EventHandler.getAvgDriveForce();
            strokeData.strokeCount = PM5EventHandler.getStrokeCount();
            // 36
            strokeData.strokePower = PM5EventHandler.getStrokePower();
            strokeData.strokeCalories = PM5EventHandler.getStrokeCalories();
            strokeData.workPerStroke = PM5EventHandler.getWorkPerStroke();
            
            StrokeDataEvent e = new StrokeDataEvent(strokeData);
            InteroEventManager.GetEventManager().SendEvent(e);
        }

		public ErgData onErgDataReady(float i_f,
								float time,
								float distance,
								float flags,
								float totalWOGDistance,
								float totalWOGTime,
								float WOGTimeType,
								float drag,
								// 32
								float speed,
								float SPM,
								float heartrate,
								float pace,
								float avgPace,
								float restDistance,
								float restTime,
								int intervalCount,
								float avgPower,
								float calories,
								float splitAvgPace,
								float splitAvgPower,
								float splitAvgCalories,
								float splitTime,
								float splitDistance)
		{
			// print("onErgDataReady "+i_f);
			int i = (int)i_f;
			ergData.i = i;
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
			// ergData.power = power;
			return ergData;
			// textStatus.text =  ("ErgData Connected "+time);

			// if (playerBLE!=null){
			// 		playerBLE.OnErgData (ergData);
			// }
			// interoServer.SendErgData(ergData);
		}

		public StrokeData onStrokeDataReady(
				float i_f,
				float time,
				float distance,
				float driveLength,
				float driveTime,
				float strokeRecoveryTime,
				float strokeRecoveryDistance,
				float peakDriveForce,
				float avgDriveForce,
				float workPerStroke,
				float strokeCount,
				float strokePower,
				float strokeCalories,
				float projectedWorkTime,
				float projectedWorkDistance
				)
		{
			int i = (int)i_f;
			strokeData.i = i;
			strokeData.time = time;
			strokeData.distance = distance;
			strokeData.driveLength = driveLength;
			strokeData.driveTime = driveTime;
			strokeData.strokeRecoveryTime = strokeRecoveryTime;
			strokeData.strokeRecoveryDistance = strokeRecoveryDistance;
			strokeData.peakDriveForce = peakDriveForce;
			strokeData.avgDriveForce = avgDriveForce;
			strokeData.workPerStroke = workPerStroke;
			strokeData.strokeCount = strokeCount;
			strokeData.strokePower = strokePower;
			strokeData.strokeCalories = strokeCalories;
			// strokeData.projectedWorkTime = projectedWorkTime;
			// strokeData.projectedWorkDistance = projectedWorkDistance;

			return strokeData;
		}
	}
}