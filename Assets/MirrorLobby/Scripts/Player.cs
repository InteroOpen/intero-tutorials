using Intero.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror.Examples.Chat
{
    public class Player : NetworkBehaviour
    {
        [SyncVar]
        public string playerName;
        [SyncVar]
        public bool listo;
        [SyncVar]
        public bool lider = false;
        [SyncVar(hook = nameof(UpdateParent))]
        public NetworkIdentity myParent = null;

        public GameObject salaPrefab;
        // public GameObject ownSala;
        public static event Action<Player, string> OnMessage;
        public static event Action<Player, bool> OnReady;
        // public static event Action<Player, bool> OnLider;
        public static event Action<Player, string> OnCreateSala;
        
        public static event Action<Player, string> OnPlayerJoinSala;
        public static event Action<Player> OnPlayerExitSala;
        public static event Action<Player> OnPlayerJoinGame;
        public static event Action<Player> OnPlayerExitGame;
        public static Dictionary<string, GameObject> salas = new Dictionary<string, GameObject>();


        private void Awake()
        {
            DontDestroyOnLoad(this);
            this.transform.parent = null;
        }
        

        public void Indestructible()
        {
            DontDestroyOnLoad(this);

        }

        public void Start()
        {
            UnityEngine.Debug.Log("Noew player " + playerName);
            if(myParent != null)
                transform.parent = myParent.transform;
            OnPlayerJoinGame?.Invoke(this);
            this.transform.parent = null;
        }

        private void OnDestroy()
        {
            OnPlayerExitGame?.Invoke(this);
//            NetworkServer.Destroy(ownSala);
            
            if (hasAuthority)
            {
                // CmdExitSala();
                Debug.Log("Server OnDestroy");
            }
            else
            {
                Debug.Log("client OnDestroy");
            }
        }

        [Command]
        public void CmdCreateSala(string salaName)
        {
            Debug.Log("CmdCreateSala");
            GameObject ownSala = Instantiate(salaPrefab);
            ownSala.GetComponent<Sala>().salaName = salaName;
            // salas.Add(salaName, ownSala);
            salas[salaName] = ownSala;
            lider = true;
            NetworkServer.Spawn(ownSala);
            if (salaName.Trim() != "")
               RcpCreateSala(salaName.Trim(), ownSala);
        }
        [Command]
        public void CmdSend(string message)
        {
            Debug.Log("El mensaje llego al server");
            if (message.Trim() != "")
                RpcReceive(message.Trim());
        }
        [Command]
        public void CmdReady(bool ready)
        {
            listo = ready;
            // RcpReady(ready);
        }
        [Command]
        public void CmdUneteSala(string salaName)
        {
            GameObject sala = salas[salaName];
            myParent = sala.GetComponent<NetworkIdentity>();
            gameObject.transform.parent = myParent.transform;
           //  RpcUneteSala(salaName);
        }
        [Command]
        public void CmdIniciar()
        {
            gameObject.transform.parent = null;
            //  RpcUneteSala(salaName);
        }
        [Command]
        public void CmdExitSala()
        {
            ExitSala();
        }
        [Command]
        public void CmdCambioEscena(GameObject sala)
        {
            Debug.Log("Cambio de escena");
            RpcCambioEscena(sala);
        }
        public void ExitSala() {
            GameObject sala = myParent.gameObject;
            Debug.Log("Antes de null");
            myParent = null;
            transform.parent = null;
            if(sala != null)
            {
                Player[] players = sala.GetComponentsInChildren<Player>();
                Debug.Log("Despues de null");
                lider = false;
                Debug.Log("# SALAS " + players.Length);
                if (players.Length == 0)
                {
                    sala.SetActive(false);
                    sala.transform.parent = null;
                    RpcDestroySala(sala);
                }
                else
                {
                    players[0].lider = true;
                }
            }
        }


        [Command]
        public void CmdDestroySala(GameObject sala)
        {
            /*
            GameObject []players;
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject entry in players)
            {
                if (entry.transform.parent == sala.transform)
                {
                    entry.GetComponent<Player>().myParent = null;
                    entry.transform.parent = null;
                }
            }*/
            lider = false;
            //RpcDestroySala(sala);
            // NetworkServer.UnSpawn(sala);
            NetworkServer.Destroy(sala);
        }
        [ClientRpc]
        public void RpcDestroySala(GameObject sala)
        {
            sala.transform.parent = null;
            sala.SetActive(false);
           
           // if (transform.parent == sala.transform)
             //   myParent = null;
                //transform.parent = null;
        }
        [ClientRpc]
        public void RpcCambioEscena(GameObject sala)
        {
            Debug.Log("Cambio de escena");
            GameObject salaH = sala.transform.GetChild(0).gameObject;
            salaH.SetActive(false);
            sala.transform.parent = null;
            DontDestroyOnLoad(sala);
            SceneManager.LoadScene("Tutorial5SpawningObjects");
            // CmdSendErgData("Jjojo");
            // if (transform.parent == sala.transform)
            //   myParent = null;
            //transform.parent = null;
        }
        // corre en los clientes
        void UpdateParent(NetworkIdentity salaVieja,NetworkIdentity salaNueva)
        {
            
            Debug.Log("Se llamo elñ hook" + salaNueva);
            if(salaNueva != null)
            {
                Debug.Log("Nuevo no es null");

                transform.parent = salaNueva.transform;
                Debug.Log("Se cambio el parent a " + salaNueva);

                Sala sala = salaNueva.GetComponent<Sala>();
                if (isLocalPlayer)
                    sala.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
                OnPlayerJoinSala?.Invoke(this, sala.salaName);
            }
            else
            {

                Debug.Log("Nuevo es null y old es" + salaVieja);
                if(salaVieja != null)
                {
                    transform.parent = null;
                    Debug.Log("Se salio: " + playerName);
                    if (hasAuthority)
                    {
                        salaVieja.transform.parent = null;
                            /*
                        if(lider == true)
                        {
                            GameObject[] players;
                            players = GameObject.FindGameObjectsWithTag("Player");
                            foreach (GameObject entry in players)
                            {
                                if (entry.transform.parent == salaVieja.transform)
                                {
                                    entry.GetComponent<Player>().myParent = null;
                                    entry.transform.parent = null;

                                }
                            }
                            CmdDestroySala(salaVieja.gameObject);
                        }
                            */
                    }
                    // lider = false;
                    OnPlayerExitSala?.Invoke(this);
                }
                
            }

        }
        
            /*
        [ClientRpc]
        public void RpcUneteSala(string salaName)
        {
            UnityEngine.Debug.Log("RpcUneteSala");
            Debug.Log(salaName + " " + isLocalPlayer);
            GameObject sala = salas[salaName];
            if (isLocalPlayer) 
                sala.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            // transform.parent = sala.transform;
            
        }
                */

        [ClientRpc]
        public void RcpCreateSala(string salaName, GameObject sala)
        {
            Debug.Log("RcpCreateSala " );
            // salas.Add(salaName, sala);
            Debug.Log(salaName + "Se agrego sala ");

            Debug.Log("" + isLocalPlayer);
            if (isLocalPlayer)
                CmdUneteSala(salaName);
            // sala.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            OnCreateSala?.Invoke(this, salaName);
        }
        [ClientRpc]
        public void RcpReady(bool ready)
        {
            OnReady?.Invoke(this, ready);
        }

        [ClientRpc]
        public void RpcReceive(string message)
        {
            OnMessage?.Invoke(this, message);
        }
        [ClientRpc]
        public void RpcReceiveErgData(ErgData ergData)
        {
            // OnMessage?.Invoke(this, ergData);
            Debug.Log(playerName + " " +ergData.distance + "Jojojojo RpcReceiveErgData");
        }
        [Command]
        public void CmdSendErgData(ErgData ergData)
        {
            // if (message.Trim() != "")
                RpcReceiveErgData(ergData);
        }
    }
}
