using Intero.Common;
using Intero.Events;
using UnityEngine;
namespace Intero
{
    public class Tutorial1EventGenerator : MonoBehaviour
    {
        // Start is called before the first frame update
        ErgData ergData;
        void Start()
        {
            ergData = new ErgData();
            ErgDataEvent e = new ErgDataEvent(ergData);
            InteroEventManager.GetEventManager().SendEvent(e);
        }

        // Update is called once per frame
        void Update()
        {
            //ergData.distance++;
            //ErgDataEvent e = new ErgDataEvent(ergData);
            //InteroEventManager.GetEventManager().SendEvent(e);
        }
    }
}