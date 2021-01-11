using Intero.Common;
using Intero.Events;
using Intero.Physics;
using Mirror.Examples.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalController : MonoBehaviour
{
    PhysicsManager[] physicsManagers;// = new PhysicsManager();
    GameObject[] rivals;
    /*
    void Start()
    {
        StartWorkout(3);
    }*/

     Dictionary<uint, uint> mapNetId = new Dictionary<uint, uint>();
    public void StartWorkout(GameObject[] players)
    {
        uint i = 0;
        foreach (GameObject entry in players)
        {
            Player p = entry.GetComponent<Player>();
            mapNetId.Add(p.netId, i++);
        }
        StartWorkout(players.Length);
    }
    public void StartWorkout(int numRivals)
    {
        Debug.Log("RIvalControler.Startworkout" + numRivals);
        GameObject rivalTemplate = transform.Find("RivalTemplate").gameObject;

        rivals = new GameObject[numRivals];
        physicsManagers = new PhysicsManager[numRivals];
        for (int i = 0; i < numRivals; i++)
        {
            rivals[i] = Instantiate(rivalTemplate, new Vector3(0,0, (i + 1) * .6F), rivalTemplate.transform.rotation);// rankPrefab.transform.position, rankPrefab.transform.rotation);// new Vector3(0, -i * 30.0F, 0), Quaternion.identity);
            rivals[i].transform.parent = transform.parent;
            rivals[i].SetActive(true);
            physicsManagers[i] = new PhysicsManager();
        }
    }

    public void UpdateRival(OSCErgDataEvent ergEvent)
    {
        if (rivals == null) return;
        print("UpdateRival index1 " + ergEvent.senderId);
        print("UpdateRival index2 " + rivals);
        GameObject rival = rivals[ergEvent.senderId];
        PhysicsManager physicsManager = physicsManagers[ergEvent.senderId];
        Rigidbody rigidBody = rival.GetComponent<Rigidbody>();
        float v = rigidBody.velocity.x;
        float x = rigidBody.position.x;
        // float z = ergEvent.senderId * 2.0f;
        
        ErgData e = new ErgData();
        e.Copy(ergEvent.ergData);
        e.distance = ergEvent.segment.getProgressedDistance(ergEvent.ergData);
        print("UpdateRival p Distance " + e.distance);
        if (e.distance < 0.1 && e.distance> -0.1) physicsManager.ResetLocation();

        InteroBody1D body = physicsManager.UpdateLocation(x, v, e);
        rigidBody.velocity = new Vector3(body.velocity, rigidBody.velocity.y, rigidBody.velocity.z);
        rigidBody.position = new Vector3(body.distance, rigidBody.position.y, rigidBody.position.z);
    }
}
