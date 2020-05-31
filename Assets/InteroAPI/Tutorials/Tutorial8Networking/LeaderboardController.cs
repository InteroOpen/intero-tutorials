using Intero.Common;
using Intero.Workouts;
using InteroAPI.Statistics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    Text[] rankNameTexts;
    Text[] rankStatsTexts;
    LeaderBoardManager leaderboard = null;
    public MapController mapController;
    public GameObject rankPrefab;
    public int numberEntries;
    public RankNode rankLocal = null;
    void Start()
    {
        leaderboard = new LeaderBoardManager();
        rankNameTexts = new Text[numberEntries];
        rankStatsTexts = new Text[numberEntries];
        mapController.Init(numberEntries);
        for (int i = 0; i < numberEntries; i++)
        {
            GameObject g = Instantiate(rankPrefab, new Vector3(0, -i * 90.0F - 90F, 0), Quaternion.identity);// rankPrefab.transform.position, rankPrefab.transform.rotation);// new Vector3(0, -i * 30.0F, 0), Quaternion.identity);
            g.SetActive(true);                                                                                         //setParent
            g.transform.SetParent(transform, false);
            rankNameTexts[i] = g.transform.Find("RankName").GetComponent<Text>();
            rankStatsTexts[i] = g.transform.Find("RankStats").GetComponent<Text>();
        }
        // void UpdateRankLocal(string name, float v, ErgData ergData, Segment segment)
    }
    string GetShortName(string name)
    {
        int n = name.Length;
        if (n < 5)
        {
            return name;
        }
        return name.Substring(0, 2) + "" + name.Substring(n - 2, 2);
    }
    void SetRank(int i, RankNode rank)
    {
        if (rankLocal == null) return;
        // rank.progressDistance 
        Debug.Log("SetRank 1" + rankLocal);
        Debug.Log("SetRank 2" + rank);
        Debug.Log("SetRank 3" + rankNameTexts);
        Debug.Log("SetRank 3" + rankNameTexts.Length);

        rankNameTexts[i].text = (i + 1) + " " + GetShortName(rank.username);
        ErgData e = rank.ergData;
        Segment s = rank.segment;
        
        rankStatsTexts[i].text = SegmentTime.timeToString((int)rank.ergData.pace) + " " + rank.ergData.spm + " " + (int)(rank.progressDistance - rankLocal.progressDistance) + " m"; //  + rank.ergData.heartrate;
        mapController.UpdatePosition(i, rank, rankLocal);
        //  rankStatsTexts[i].text = SegmentTime.timeToString((int)e.pace) + " " + (s.getProgressedDistance(e)*-1);//  + "|" + s.getProgressedDistance(e);
    }
    public void UpdateRankList(LinkedList<RankNode> listRanks) {
        int i = 0;
        // only update the ones of my same index
        // Debug.Log("UpdateRankList" + rankLocal.username);
        // int currentSegment = rankLocal.segment.index;
        for (LinkedListNode<RankNode> it = listRanks.First; it != null; it = it.Next)
        {
            RankNode r = it.Value;
            //if(r.segment.index==currentSegment)
                SetRank(i++, r);
        }
        
    }

    public void UpdateRankLocal(string name, float v, ErgData ergData, Segment segment)
    {
        rankLocal = new RankNode(name, v, ergData, segment);
        UpdateRank(name, v, ergData, segment);
    }
    public void UpdateRank(string name, float v, ErgData ergData, Segment segment)
    {
        if (leaderboard == null) return;
        ErgData e = new ErgData();
        Segment s = Segment.Factory(segment);
        e.Copy(ergData);
        leaderboard.UpdateRank(name,v,e, s);
        LinkedList<RankNode> ranks = leaderboard.GetRankings();
        UpdateRankList(ranks);
    }
}
