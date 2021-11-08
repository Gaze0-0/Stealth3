using Lobby;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class NetworkManagerLobby : NetworkManager
    {
        [Scene] [SerializeField] private string menuScene = string.Empty;

        [Header("Room")]
        [SerializeField] private RoomPlayerLobby roomPlayerPrefab = null;

        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;

        public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");
            base.OnStartClient();
            Debug.Log("1");
            foreach (var prefab in spawnablePrefabs)
            {
                NetworkClient.RegisterPrefab(prefab);
            }
        }

        public override void OnClientConnect(NetworkConnection conn) //when you connect to a server
        {
            base.OnClientConnect(conn);
            Debug.Log("2");
            OnClientConnected?.Invoke();
        }

        public override void OnClientDisconnect(NetworkConnection conn)//when you disconnect from a server
        {
            base.OnClientDisconnect(conn);
            Debug.Log("3");
            OnClientDisconnected?.Invoke();
        }

        public override void OnServerConnect(NetworkConnection conn)// when client connects to you
        {
            Debug.Log("4");
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                Debug.Log("4,1");
                return;
                
            }

            if (SceneManager.GetActiveScene().name != menuScene)
            {
                conn.Disconnect();
                Debug.Log("4,2");
                return;
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Debug.Log("5");
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                RoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
            }
        }


        
































    }
}