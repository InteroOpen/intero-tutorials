using UnityEngine.UI;
using UnityEngine;
using Intero.Events;
using Intero.Common;

public class UICanvasErgController : MonoBehaviour, IListenerErg
{
    public Text textTime;
    public Text textPace;
    public Text textDistance;
    public Text textWatts;
    public Text textStrokeCount;

    void IListenerErg.OnErgDataEvent(ErgDataEvent ergDataEvent)
    {
        ErgData erg = ergDataEvent.ergData;
        textTime.text = erg.time+"";
        textPace.text = erg.pace+"";
        textDistance.text = erg.distance+ "";
    }

    void IListenerErg.OnStrokeDataEvent(StrokeDataEvent strokeDataEvent)
    {
        StrokeData stroke = strokeDataEvent.strokeData;
        textWatts.text = stroke.strokePower + "";
        textStrokeCount.text = stroke.strokeCount + "";

    }

    // Update is called once per frame
    void Start()
    {
        InteroEventManager.GetEventManager().AddListener(this);
    }
}
