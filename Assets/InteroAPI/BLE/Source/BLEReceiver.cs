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
            PM5EventHandler.connectToPM5(channel);
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
    }
}