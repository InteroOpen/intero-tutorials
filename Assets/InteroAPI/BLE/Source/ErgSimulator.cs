using UnityEngine;
using Intero.Common;
using Intero.Events;
using System.Collections;

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
            erg.spm = 34;
                // SendErgData();
            }
        }
        void Start()
        {
         
            InvokeRepeating("SendErgData", 0.0f, 0.4f);
        }
        public void SetPace(string pace)
    {
        this.pace = float.Parse( pace);
    }
        void SendErgData()
        {
            ErgDataEvent e = new ErgDataEvent(erg);
            InteroEventManager.GetEventManager().SendEvent(e);
        }
    }
