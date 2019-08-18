using UnityEngine;
using Intero.Common;
using Intero.Events;

namespace Intero.BLE
{
    public class BLEReceiver : MonoBehaviour
    {
        ErgData ergData = new ErgData();
        StrokeData strokeData = new StrokeData();

        public void StartBLE(int channel)
        {
            PM5EventHandler.connectToPM5(channel);
        }

        void onBLEOn(string s)
        {
            InteroEventManager.GetEventManager().SendEvent(new BLEOnEvent());
        }


        public void onPM5Connected(string s)
        {
            InteroEventManager.GetEventManager().SendEvent(new BLEConnectedEvent());
        }

        public void onErgDataReady(string s)
        {
            print(PM5EventHandler.getDistance());
            ergData.distance = PM5EventHandler.getDistance();
            ergData.avgPower = PM5EventHandler.getPower();
            ergData.pace = PM5EventHandler.getPace();
            ergData.spm = PM5EventHandler.getSPM();
            ergData.time = PM5EventHandler.getTime();
            ergData.splitAvgCalories = PM5EventHandler.getCalhr();
            ergData.calories = PM5EventHandler.getCalories();
            ErgDataEvent e = new ErgDataEvent(ergData);
            InteroEventManager.GetEventManager().SendEvent(e);
        }

        public void onStrokeDataReady(string s)
        {
            strokeData.time = PM5EventHandler.getTime();
            strokeData.distance = PM5EventHandler.getDistance();
            strokeData.driveLength = PM5EventHandler.getDriveLength();
            strokeData.driveTime = PM5EventHandler.getDriveTime();
            strokeData.strokeRecoveryTime = PM5EventHandler.getStrokeRecoveryTime();
            StrokeDataEvent e = new StrokeDataEvent(strokeData);
            InteroEventManager.GetEventManager().SendEvent(e);
        }
    }
}