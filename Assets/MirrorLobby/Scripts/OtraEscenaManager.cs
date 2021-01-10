using Mirror;
using Mirror.Examples.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtraEscenaManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player player = NetworkClient.connection.identity.GetComponent<Player>();
        //player.CmdSendErgData(player.playerName + " jojo ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
