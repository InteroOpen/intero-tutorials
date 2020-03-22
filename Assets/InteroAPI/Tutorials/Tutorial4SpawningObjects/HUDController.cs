using Intero.Workouts;
using Intero.Common;

using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text textDuration;
    public Text textDifficulty;
    public Text textObjective;

    public GameObject segRecuperacion;
    public GameObject segFuerte;
    public GameObject segMedio;
    
    public void DisplayCurrentSegment(Segment segment, ErgData progressValue)
    {
        textObjective.text = segment.getTextObjective();
        //.time - 0.6f
        textDuration.text = segment.getTextRemaining(progressValue);// segment.getTextRemaining();
        segRecuperacion.SetActive(SegmentType.EASY == segment.type);
        segFuerte.SetActive(SegmentType.FAST == segment.type);
        segMedio.SetActive(SegmentType.MEDIUM == segment.type);
        string[] strDifficulty = { "recuperación", "medio", "intenso" };
        textDifficulty.text = strDifficulty[(int)segment.type];// SegmentTime.timeToString((int)segment.getRemaining(0));
    }

}