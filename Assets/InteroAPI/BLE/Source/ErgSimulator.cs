using UnityEngine;
using Intero.Common;
using Intero.Events;
using System.Collections;

namespace Intero.BLE
{
    public class ErgSimulator : MonoBehaviour
    {
        ErgData erg = new ErgData();
        public float pace;
        int i = 0;

        void Update()
        {
            float time = Time.timeSinceLevelLoad;
            float distance = 500 * (time / pace); // Distance in meters

            if ((++i % 10) == 0)
            {
                i = 0;
                erg.time = time;
                erg.pace = pace;
                erg.distance = distance;
                SendErgData();
            }
        }

        void SendErgData()
        {
            ErgDataEvent e = new ErgDataEvent(erg);
            InteroEventManager.GetEventManager().SendEvent(e);
        }
    }
}
