using Intero.Common;
using Intero.Events;
using Intero.Statistics;
using Intero.Workouts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentSummary : MonoBehaviour
{
    public Text textPace;
    public Text textSPM;
    public Text textObjective;
    public Text textPower;
    public GameObject layout;
    // Start is called before the first frame update

    // Update is called once per frame
    public void ShowSegmentSummary(SegmentEndEvent segmentEndEvent,StatisticManager statisticManager)
    {
        ErgData e = statisticManager.GetSegmentErgDataStats();
        textPace.text = SegmentTime.timeToString((int)e.pace);
        textSPM.text = e.spm+"";
        textObjective.text = segmentEndEvent.currentSegment.getTextObjective();
        textPower.text = e.avgPower+" watts";

        // ErgData[] ergs = (ErgData[]).ToArray();
        string ret = "";
        Stack s = statisticManager.ergSegmentStack;
        for (int i = 0; i < s.Count; ++i)
        {
            e = (ErgData)s.Pop();
            ret += "[" + e.pace + ", " + e.spm + ", " + e.avgPower + "] ";
        }
        print("jojo " + ret);
        layout.SetActive(true);
        Invoke("HideSegmentSummary", 5);
    }
    public void HideSegmentSummary()
    {
        layout.SetActive(false);
    }
}
