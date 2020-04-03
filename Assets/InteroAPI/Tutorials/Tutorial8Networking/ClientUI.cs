using System;
using System.Collections;
using System.Collections.Generic;
using Intero.Common;
using Intero.TCP;
using Intero.Workouts;
using UnityEngine;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour
{
    public InputField nameText;
    public InputField speedText;
    public ErgSimulator ergSimulator;
    InteroClientTCP client = null;
    // Start is called before the first frame update
    public void StartClient()
    {
        client = new InteroClientTCP();
        client.username = nameText.text;
        ergSimulator.SetPace(speedText.text);
        client.Connect(8080);
    }
    public string GetName()
    {
        return nameText.text;
    }
    // Update is called once per frame
    void Update()
    {
        // neded for event handling to run on the unity thread
        if (client != null) client.unityThread.Update();
    }
    void OnDestroy()
    {
        client.OnDestroy();
        Debug.Log("OnDestroy1");
    }

    public void SendMessage(ErgData ergData, Segment currentSegment)
    {
        if (client != null) client.SendMessage(ergData, currentSegment);
    }
}
