using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BouyeCreator : MonoBehaviour
{
    private GameObject[,] bouyesLeft, bouyesRight;
    private GameObject[] distRight, distLeft;
    private int nBouyes = 30;
    private float bouyeDistance = 10.0f;
    private int nLanes = 4;
    private int distancia;

    public Rigidbody player;
    public int metros = 250;
    public GameObject boyaPrefab, distanciaPrefab;

    public Text textoDistacia;
    // Use this for initialization
    void Start()
    {
        print("Start Boyas");
        InvokeRepeating("Bark", 0, 1.0f);
        float scale = 0.55f;
        bouyesLeft = new GameObject[nBouyes, nLanes / 2];
        bouyesRight = new GameObject[nBouyes, nLanes / 2];
        distLeft = new GameObject[nLanes / 2];
        distRight = new GameObject[nLanes / 2];
        for (int lane = 1; lane < nLanes; lane += 2)
        {
            for (int i = 0; i < nBouyes; i++)
            {

                GameObject sphereRight = Instantiate(boyaPrefab);
                GameObject sphereLeft = Instantiate(boyaPrefab);

                // sphereRight.transform.position = new Vector3 (3.0f*lane, 0F, 20.0f * i);
                sphereRight.transform.position = new Vector3(bouyeDistance * i - bouyeDistance * (nBouyes / 2.0f), 0F, 3.5f * lane);
                sphereLeft.transform.position = new Vector3(bouyeDistance * i - bouyeDistance * (nBouyes / 2.0f), 0F, -3.5f * lane);
                //				sphereRight.transform.position = new Vector3 (0, 0, 0);
                //				sphereLeft.transform.position = new Vector3 (0, 0, 0);
                // sphereLeft.transform.position = new Vector3 (-3.0f*lane, 0F, 20.0f * i);
                sphereLeft.transform.localScale -= new Vector3(scale, scale, scale);
                sphereRight.transform.localScale -= new Vector3(scale, scale, scale);
                bouyesLeft[i, lane / 2] = sphereLeft;
                bouyesRight[i, lane / 2] = sphereRight;

                //				print (i);
                //				bouyesRight [i, lane / 2].transform.position = new Vector3 (0, 0F, 3.5f*lane);
                //				bouyesLeft [i, lane / 2].transform.position = new Vector3 (0, 0F, -3.5f*lane);
            }

            distRight[lane / 2] = Instantiate(distanciaPrefab);
            distLeft[lane / 2] = Instantiate(distanciaPrefab);

            distRight[lane / 2].transform.position = new Vector3(distancia, 0.5f, 3.5f * lane);
            distLeft[lane / 2].transform.position = new Vector3(distancia, 0.5f, -3.5f * lane);

            distRight[lane / 2].transform.rotation = Quaternion.Euler(0f, 180.0f, 0f);
            distLeft[lane / 2].transform.rotation = Quaternion.Euler(0f, 180.0f, 0f);

        }

       // textoDistacia.text = distancia + "m";

    }
    public void SetPlayer(Rigidbody player)
    {
        this.player = player;
    }
    void Bark()
    {
        if (player == null)
        {
            return;
        }
        //print( "Bark");
        int x = (int)player.transform.position.x;

        float z = player.transform.position.z;

        if (x > distancia + metros / 2)
        {
            distancia += metros;
            textoDistacia.text = distancia + "m";
            for (int lane = 1; lane < nLanes; lane += 2)
            {
                distRight[lane / 2].transform.position = new Vector3(distancia, 0.5f, 3.5f * lane + z);
                distLeft[lane / 2].transform.position = new Vector3(distancia, 0.5f, -3.5f * lane + z);
            }
        }
       
        for (int lane = 1; lane < nLanes; lane += 2)
        {
            for (int i = 0; i < nBouyes; i++)
            {
                bouyesRight[i, lane / 2].transform.position = new Vector3(i * bouyeDistance + bouyeDistance * (x / (int)bouyeDistance + 1) - bouyeDistance * (nBouyes / 2.0f), 0F, 3.5f * lane + z);
                bouyesLeft[i, lane / 2].transform.position = new Vector3(i * bouyeDistance + bouyeDistance * (x / (int)bouyeDistance + 1) - bouyeDistance * (nBouyes / 2.0f), 0F, -3.5f * lane + z);
            }

            distRight[lane / 2].transform.position = new Vector3(distancia, 0.5f, 3.5f * lane + z);
            distLeft[lane / 2].transform.position = new Vector3(distancia, 0.5f, -3.5f * lane + z);

        }
    }
}
