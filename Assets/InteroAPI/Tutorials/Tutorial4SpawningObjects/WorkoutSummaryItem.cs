using Intero.Common;
using Intero.Workouts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutSummaryItem : MonoBehaviour
{
    string[] strDifficulty = { "Recuperación", "Medio", "Fuerte!" };
    public Text segmentType;
    public Text segmentObjective;
    public Text segmentDuration;
    public Image intensity;

    public Text avgPace;
    public Text distancie;
    public Text time;
    public Text avgSPM;
    public Text avgPower;
    public Text avgHR;

    internal void UpdateSegmentSummary(Segment s, ErgData e)
    {
        segmentType.text = strDifficulty[(int)s.typeIntensity];// .type.ToString();
        segmentObjective.text = s.getTextObjective(); // workout.segments[i].target.spm.ToString();
        segmentDuration.text = s.getTextRemaining(0);// workout.segments[i].duration.ToString();
                                                                 //set background color
                                                                 //no se que valores arroja intensity porque es del tipo SegmentIntensity
                                                                 // pero para cambiar el color del fondo seria
                                                                 //segmentItem.intensity.color = el color
        // s
        
        avgPace.text = SegmentTime.timeToString((int)e.pace);
        distancie.text = (int)Math.Round(e.distance) + " m";
        time.text = SegmentTime.timeToMSString(e.time);
        avgSPM.text = e.spm+""; ;
        avgPower.text = e.avgPower + " W" ;
        avgHR.text = e.heartrate + " BPM";
    }
}
