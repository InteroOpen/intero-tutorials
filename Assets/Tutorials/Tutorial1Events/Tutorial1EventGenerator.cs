using Intero.Common;
using Intero.Events;
using UnityEngine;

public class Tutorial1EventGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    ErgData ergData;
    void Start()
    {
        ergData = new ErgData();
    }

    // Update is called once per frame
    void Update()
    {
        ergData.distance++;
        ErgDataEvent e = new ErgDataEvent(ergData);
        InteroEventManager.GetEventManager().SendEvent(e);
    }
}
