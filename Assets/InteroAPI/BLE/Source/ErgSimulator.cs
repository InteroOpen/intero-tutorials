using UnityEngine;
using Intero.Common;
using Intero.Events;
using System.Collections;

    public class ErgSimulator : MonoBehaviour
    {
        ErgData erg = new ErgData();
        public float pace;
    float d = 0;
        int i = 0;

        void Update()
        {
            float time = Time.timeSinceLevelLoad;
            float dt = Time.deltaTime;
            // float distance = 500 * (time / pace); // Distance in meters
            d = d + 500 * (dt/pace);
            if ((++i % 10) == 0)
            {
                i = 0;
                erg.time = time;
                erg.pace = pace;
                erg.distance = d;
                erg.spm = 34;
                // SendErgData();
            }
        }
        void Start()
        {
         
            InvokeRepeating("SendErgData", 0.0f, 0.5f);
        }
        public void SetPace(string pace)
    {
        this.pace = float.Parse( pace);
    }
        void SendErgData()
        {
            // Debug.Log("SendErgData");
            ErgDataEvent e = new ErgDataEvent(erg);
            InteroEventManager.GetEventManager().SendEvent(e);
        }
    }
