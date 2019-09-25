using Intero.Events;
using UnityEngine;

public class DebugListener : MonoBehaviour, IListenerErg
{
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        UnityEngine.Debug.Log("got ergData " + ergDataEvent.ergData);
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent)
    {
        UnityEngine.Debug.Log("got strokeData " + strokeDataEvent.strokeData);
    }


    // Start is called before the first frame update
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener(this);
    }
}
