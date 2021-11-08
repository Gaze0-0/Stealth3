using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby

public class NetworkManagerLobby : NetworkBehaviour
{
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private RoomPlayerLobby roomPlayerPrefab = null;
    

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;



    public override void OnStartSever() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }


    public override void OnClientConnect(NetworkConnection conn)
    {
        if (!clientLoadedScene)
        {
            if (!ClientScene.ready)
            {
                ClientScene.Ready(conn);
            }
        }
    }








































}
