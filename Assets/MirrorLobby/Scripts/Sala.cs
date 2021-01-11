using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mirror.Examples.Chat
{
    //[System.Serializable]
    //public class SyncListItem : SyncList<Player> { }
    public class Sala : NetworkBehaviour
    {
        [SyncVar]
        public string salaName;

        public Text listaPlayersText;
        public Text chatHistory;
        public Text mensaje;
        public Toggle toggleReady;
        public GameObject listaSalas;
      //  public GameObject salaActual;
        public GameObject miniMenu;
        public NetworkIdentity myParent = null;
        private Transform Canvas;

        public void Awake()
        {
            Debug.Log("Se creo la sala");
            Player.OnPlayerJoinSala += OnJoinSala;
            Player.OnPlayerExitSala += OnExitSala;
            Player.OnMessage += OnPlayerMessage;
            Player.salas[salaName] = gameObject;
            if(isServer==false)
                InvokeRepeating("UpdateTextUsersSalas", 2.0f, 0.3f);
        }
        private void Start()
        {
            listaSalas = GameObject.Find("ListaSalas");
            listaSalas.SetActive(false);
        }
        public void ExitSala()
        {
            listaSalas.SetActive(true);
            Player player = NetworkClient.connection.identity.GetComponent<Player>();
            player.CmdExitSala();
           
        }
        public void PlayerReady()
        {
            Player player = NetworkClient.connection.identity.GetComponent<Player>();
            print("Setting player to ready ");
            player.CmdReady(toggleReady.isOn);
        }
        public void PlayerMessage()
        {
            Debug.Log("Se llamo a la funcion del boton");
            Player player = NetworkClient.connection.identity.GetComponent<Player>();
            player.CmdSend(mensaje.text.ToString());
        }
        void OnJoinSala(Player player, string salaName)
        {
            // UpdateTextUsersSalas();
        }
        void OnExitSala(Player player)
        {
            // UpdateTextUsersSalas();
        }
        public void OnPlayerMessage(Player player, string message)
        {
            Debug.Log("Se llamo la funcion de actualizar texto");
            chatHistory.text += player.playerName + " " +  message + "\n";

        }

        public void Iniciar()
        {
            miniMenu.SetActive(true);
            //  ExitSala();
            // Player player = NetworkClient.connection.identity.GetComponent<Player>();
            //  player.transform.parent = null;
            Canvas = this.transform.parent;
            this.transform.parent = null;
            DontDestroyOnLoad(this);
            //  player.Indestructible();
        }

        public void CambiaEscen()
        {
            //  Player player = NetworkClient.connection.identity.GetComponent<Player>();
            //  player.CmdExitSala();
            //  myParent = salaActual.GetComponent<NetworkIdentity>();
              Player player = NetworkClient.connection.identity.GetComponent<Player>();
            /* player.transform.parent = null;
             this.transform.parent = null;
             DontDestroyOnLoad(this);
             player.CmdIniciar();*/
            player.CmdCambioEscena(this.gameObject);
            miniMenu.SetActive(false);
          //  SceneManager.LoadScene("otraEscena");
         //   SceneManager.LoadScene("otraEscena");
        }
        public void MantenerEscena()
        {
          //  Player player = NetworkClient.connection.identity.GetComponent<Player>();
          //  player.transform.parent = salaActual.transform;
            miniMenu.SetActive(false);
            this.transform.parent = Canvas;
        }

        void UpdateTextUsersSalas()
        {
            // GameObject[] players;
            int i;
            if (listaPlayersText != null)
            {
                listaPlayersText.text = "";
                Player [] players = gameObject.GetComponentsInChildren<Player>();
                i = 0;
                foreach (Player entry in players)
                {
                    // Player sala = entry.GetComponent<Player>();
                    if(entry.lider)
                        listaPlayersText.text += $"<color=yellow> {entry.playerName} </color> \n";

                    if (entry.listo)
                        listaPlayersText.text += $"<color=green> {entry.playerName} </color> \n";
                    else
                        listaPlayersText.text += $"<color=red> {entry.playerName} </color> \n";
                    ++i;
                }
            }
        }
    }
}