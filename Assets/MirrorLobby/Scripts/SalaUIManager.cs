using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Mirror.Examples.Chat
{
    public class SalaUIManager : MonoBehaviour
    {

        public Text chatHistory;
        /*
        public InputField chatMessage;
        public Text textUsers;
        public Scrollbar scrollbar;
        public Toggle toggleListo;
        public GameObject btnInicar;
        public Player playerLocal;
        public int connectionId;
        */
        //Dictionary<string, Player> players = new Dictionary<string, Player>();

        public void Awake()
        {
            
            //Player.OnReady += OnReady;
            
            // Player.OnPlayerJoinLobby += OnPlayerJoinLobby;
            // Player.OnPlayerExitLobby += OnPlayerExitLobby;
            Player.OnCreateSala += OnCreateSala;


        }
        // esto debe ir en ListaSalasController
        public void CreateSala()
        {
            Player player = NetworkClient.connection.identity.GetComponent<Player>();
            //player.listo = toggleListo.isOn;
            player.CmdCreateSala(player.playerName);
            //player.listo = true;
        }
        public void OnCreateSala(Player player, string message)
        {
            Debug.Log("Se cre la sala " + message);
            UpdateTextSalas();
            // chatHistory.text += message + "\n";
        }


        void UpdateTextSalas()
        {
            GameObject[] salasA;

            chatHistory.text = "";
            salasA = GameObject.FindGameObjectsWithTag("Sala");
            //            if (playerLocal != null)
            //              btnInicar.SetActive(playerLocal.lider);
            foreach (GameObject entry in salasA)
            {
                Sala player = entry.GetComponent<Sala>();
                // do something with entry.Value or entry.Key

                chatHistory.text += $"<color=green> {player.salaName} </color> \n";
                /*
                if (player.listo == true)
                {


                }
                else
                {
                    // btnInicar.SetActive(false);
                    textUsers.text += $"<color=red> {player.playerName} </color> \n";
                    btnInicar.SetActive(false);
                }*/


            }
        }
        void OnPlayerJoinLobby(Player player)
        {
            UpdateTextSalas();
        }
        private void OnPlayerExitLobby(Player player)
        {
            UpdateTextSalas();
        }
        /*
        public void OnClickToggle()
        {
            Player player = NetworkClient.connection.identity.GetComponent<Player>();
            //player.listo = toggleListo.isOn;
            player.CmdReady(toggleListo.isOn);
            //player.listo = true;
        }

        void UpdateTextPlayers()
        {
            GameObject[] playersA;

            textUsers.text = "";
            playersA = GameObject.FindGameObjectsWithTag("Player");
            if(playerLocal != null)
                btnInicar.SetActive(playerLocal.lider);
            foreach (GameObject entry in playersA)
            {
                Player player = entry.GetComponent<Player>();
                // do something with entry.Value or entry.Key
                
                if(player.listo == true)
                {
                    
                    textUsers.text += $"<color=green> {player.playerName} </color> \n";

                }
                else
                {
                   // btnInicar.SetActive(false);
                    textUsers.text += $"<color=red> {player.playerName} </color> \n";
                    btnInicar.SetActive(false);
                }


            }
        }
        void OnPlayerJoinLobby(Player player)
        {

            //  players[player.playerName] = player;
            if (player.isLocalPlayer)
                playerLocal = player;
            
           // btnInicar.SetActive(playerLocal.isServer);
            UpdateTextPlayers();
           // Debug.Log("ID" + player.netId);

            // UnityEngine.Debug.Log("Noew player koko " + player.playerName);
            // textUsers.text += player.playerName + "\n";
            // logger.Log(player.playerName);
        }
        private void OnPlayerExitLobby(Player player)
        {
            UpdateTextPlayers();
        }
        void OnPlayerMessage(Player player, string message)
        {
            string prettyMessage = player.isLocalPlayer ?
                $"<color=red>{player.playerName}: </color> {message}" :
                $"<color=blue>{player.playerName}: </color> {message}";
            AppendMessage(prettyMessage);

            logger.Log(message);
        }

        void OnReady(Player player, bool ready)
        {
            player.listo = ready;
            UpdateTextPlayers();
        }

        public void OnSend()
        {
            if (chatMessage.text.Trim() == "")
                return;

            // get our player
            Player player = NetworkClient.connection.identity.GetComponent<Player>();

            // send a message
            player.CmdSend(chatMessage.text.Trim());

            chatMessage.text = "";
        }

        internal void AppendMessage(string message)
        {
            StartCoroutine(AppendAndScroll(message));
        }

        IEnumerator AppendAndScroll(string message)
        {
            chatHistory.text += message + "\n";

            // it takes 2 frames for the UI to update ?!?!
            yield return null;
            yield return null;

            // slam the scrollbar down
            scrollbar.value = 0;
        }
    */
    }
}
