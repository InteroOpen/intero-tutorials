using Intero.Common;
using Intero.Workouts;
using InteroAPI.Statistics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject positionPrefab;
    public GameObject[] boats;
    // Start is called before the first frame update
    public void Init(int numberEntries)
    {
        boats = new GameObject[numberEntries];
        for (int i = 0; i < numberEntries; i++)
        {
            int sing = i % 2 == 0 ? 1 : -1;
            GameObject g = Instantiate(positionPrefab, new Vector3(-i * 20 * sing,0F, 0), Quaternion.identity);// rankPrefab.transform.position, rankPrefab.transform.rotation);// new Vector3(0, -i * 30.0F, 0), Quaternion.identity);
            g.SetActive(true);                                                                                         //setParent
            g.transform.SetParent(transform, false);
            boats[i] = g;
        }
    }

    public void UpdatePosition(int i, float percentage)
    {
        print("pre " + percentage);
        // boats[i].transform.position = ;//  new Vector3(0,0);
        // boats[i].transform.localPosition =  new Vector3(0, percentage * 400);
        boats[i].transform.position =  new Vector3(boats[i].transform.position.x,transform.position.y+220 - percentage * 400);
        // positio
        // 
    }
    public void UpdatePosition(int i, RankNode rank)
    {
        // rank.ergData
        ErgData e = rank.ergData;
        Segment s = rank.segment;
        // e.distance = s.getProgressedDistance(e);
        // print(s.type+" s\t" + s.start  + "\t" + s.end + "\t" + s.Progress(e) + "\t" + s.getTextRemaining(e)); 
        UpdatePosition(i,s.Progress(e));
    }
}


// empieza con algo positivo
// siempre vas a tener que retrabajar
// es algo normal que sucede y va suceder, 
//
// 