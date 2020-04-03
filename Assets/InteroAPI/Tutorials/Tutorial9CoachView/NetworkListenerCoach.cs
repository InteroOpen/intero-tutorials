using Intero.Common;
using Intero.Events;
using Intero.Workouts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkListenerCoach : MonoBehaviour, IListenerOSC
{
    public LeaderboardController leaderboard;
    public Text connectedText;

    void Start()
    {
        InteroEventManager.GetEventManager().AddListener((IListenerOSC)this);
        connectedText.text = "";
    }
    void IListenerOSC.OnOSCClientConnectedEvent(OSCClientConnectedEvent connectedEvent)
    {
        string username = connectedEvent.socketSender.username;
        string s = username + " connectado\n";
        if (username.Equals("coach")==false) ;
        connectedText.text += s;
        print(s);
    }

    void IListenerOSC.OnOSCErgDataEvent(OSCErgDataEvent ergEvent)
    {
        string username = ergEvent.socketSender.username;
        ErgData e = ergEvent.ergData;
        Segment s = ergEvent.segment;
        float d = s.getProgressedDistance(e);
        print(username + " xx OSC " + ergEvent.ergData + "|" + d);
        leaderboard.UpdateRank(username, d, e, s);
        // rivalController.UpdateRival(ergEvent);
    }

    void IListenerOSC.OnOSCMessageEvent(OSCMessageEvent messageEvent)
    {
        print("jj "+ messageEvent.msgReceive.Address);
        throw new System.NotImplementedException();
    }

    void IListenerOSC.OnOSCStartWorkoutDataEvent(OSCStartWorkoutEvent startWorkoutEvent)
    {
        throw new System.NotImplementedException();
    }

    void IListenerOSC.OnOSCStrokeDataEvent(OSCStrokeDataEvent strokeEvent)
    {
        throw new System.NotImplementedException();
    }

    void IListenerOSC.OnOSCClientDisconnectedEvent(OSCClientDisconnectedEvent connectedEvent)
    {
        throw new System.NotImplementedException();
    }
}
