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
    public void StartClient(string username)
    {
        if (client != null)
            client.OnDestroy();
        client = new InteroClientTCP();
        client.username = username;
        ergSimulator.SetPace("120");
        try
        {
            client.Connect(8080);
        } catch(Exception e)
        {
            print(" fv " + e.Message);
        }
    }
    public void StartClient()
    {
        client = new InteroClientTCP();
        client.username = nameText.text;
        ergSimulator.SetPace(speedText.text);
        try
        {
            print(" Starting client " );
            client.Connect(8080);
            print(" End client ");

        }
        catch (Exception e)
        {
            print(" fv " + e.Message);
        }
    }
    public string GetName()
    {
        if (client == null)
            return "missigno";
        return client.username;//nameText.text;
    }
    // Update is called once per frame
    void Update()
    {
        // neded for event handling to run on the unity thread
        if (client != null) client.unityThread.Update();
    }
    void OnDestroy()
    {
        if (client != null)
            client.OnDestroy();
        Debug.Log("OnDestroy1");
    }

    public void SendMessage(ErgData ergData, Segment currentSegment)
    {
        if (client != null) client.SendMessage(ergData, currentSegment);
    }
}
