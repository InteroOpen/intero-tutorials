using Intero.Events;
using Intero.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalController : MonoBehaviour
{
    private PhysicsManager physicsManager = new PhysicsManager();
    GameObject[] rivals;
    void Start()
    {
        StartWorkout(3);
    }
    public void StartWorkout(int numRivals)
    {
        GameObject rivalTemplate = transform.Find("RivalTemplate").gameObject;

        rivals = new GameObject[numRivals];
        for (int i = 0; i < numRivals; i++)
        {
            rivals[i] = Instantiate(rivalTemplate, new Vector3(0,0, (i + 1) * 2.0F), rivalTemplate.transform.rotation);// rankPrefab.transform.position, rankPrefab.transform.rotation);// new Vector3(0, -i * 30.0F, 0), Quaternion.identity);
            rivals[i].transform.parent = transform.parent;
            rivals[i].SetActive(true);
        }
    }

    public void UpdateRival(OSCErgDataEvent ergEvent)
    {
        GameObject rival = rivals[ergEvent.senderId];
        Rigidbody rigidBody = rival.GetComponent<Rigidbody>();
        float v = rigidBody.velocity.x;
        float x = rigidBody.position.x;
        float z = ergEvent.senderId * 2.0f;

        InteroBody1D body = physicsManager.UpdateLocation(x, v, ergEvent.ergData);
        rigidBody.velocity = new Vector3(body.velocity, rigidBody.velocity.y, rigidBody.velocity.z);
        rigidBody.position = new Vector3(body.distance, rigidBody.position.y, z);
    }
}
