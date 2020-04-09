using Intero.Common;
using Intero.Workouts;
using InteroAPI.Statistics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    Text[] rankNameTexts;
    Text[] rankStatsTexts;
    LeaderBoardManager leaderboard;
    public GameObject rankPrefab;
    public int numberEntries;
    void Start()
    {
        leaderboard = new LeaderBoardManager();
        rankNameTexts = new Text[numberEntries];
        rankStatsTexts = new Text[numberEntries];
        for (int i = 0; i < numberEntries; i++)
        {
            GameObject g = Instantiate(rankPrefab, new Vector3(0, -i * 30.0F, 0), Quaternion.identity);// rankPrefab.transform.position, rankPrefab.transform.rotation);// new Vector3(0, -i * 30.0F, 0), Quaternion.identity);
            g.SetActive(true);                                                                                         //setParent
            g.transform.SetParent(transform, false);
            rankNameTexts[i] = g.transform.Find("RankName").GetComponent<Text>();
            rankStatsTexts[i] = g.transform.Find("RankStats").GetComponent<Text>();
        }
    }
    void SetRank(int i, RankNode rank)
    {
        rankNameTexts[i].text = (i + 1) + " " + rank.username;
        ErgData e = rank.ergData;
        Segment s = rank.segment;
        rankStatsTexts[i].text = SegmentTime.timeToString((int)rank.ergData.pace) + " " + rank.ergData.spm+ " " + rank.ergData.heartrate;
       //  rankStatsTexts[i].text = SegmentTime.timeToString((int)e.pace) + " " + (s.getProgressedDistance(e)*-1);//  + "|" + s.getProgressedDistance(e);
    }
    public void UpdateRankList(LinkedList<RankNode> listRanks) {
        int i = 0;
        for (LinkedListNode<RankNode> it = listRanks.First; it != null; it = it.Next)
        {
            SetRank(i++, it.Value);
        }
        
    }

    public void UpdateRank(string name, float v, ErgData ergData, Segment segment)
    {
        ErgData e = new ErgData();
        Segment s = new SegmentTime(segment);
        e.Copy(ergData);
        leaderboard.UpdateRank(name,v,e, s);
        LinkedList<RankNode> ranks = leaderboard.GetRankings();
        UpdateRankList(ranks);
    }
}
