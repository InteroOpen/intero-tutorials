using Intero.Events;
using UnityEngine;

public class Tutorial1EventListener : MonoBehaviour, IListenerErg
{
    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        UnityEngine.Debug.Log("got ergData " + ergDataEvent.ergData);
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
