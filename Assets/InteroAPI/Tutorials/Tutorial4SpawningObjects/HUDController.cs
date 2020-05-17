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
        if (progressValue == null) return;
        textObjective.text = $"{progressValue.spm}/"+segment.objective.targetValue + " spm";
        //.time - 0.6f
        textDuration.text = segment.getTextRemaining(progressValue);// segment.getTextRemaining();
        segRecuperacion.SetActive(SegmentIntensity.EASY == segment.typeIntensity);
        segFuerte.SetActive(SegmentIntensity.FAST == segment.typeIntensity);
        segMedio.SetActive(SegmentIntensity.MEDIUM == segment.typeIntensity);
        string[] strDifficulty = { "recuperación", "medio", "Fuerte!"};
        textDifficulty.text = strDifficulty[(int)segment.typeIntensity];// SegmentTime.timeToString((int)segment.getRemaining(0));
    }

}