using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef playerPrefab;
    
    [Header("Spawn Settings")]
    [Tooltip("Drag your Transform spawnpoints here from the hierarchy")]
    public List<Transform> spawnPoints = new List<Transform>();
    
    private int _spawnIndex = 0;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            // Default backup position in case the list is empty
            Vector3 spawnPos = new Vector3(0f, 1f, 0f); 
            Quaternion spawnRot = Quaternion.identity;

            // If we have assigned spawn points, use them!
            if (spawnPoints != null && spawnPoints.Count > 0)
            {
                Transform targetSpawn = spawnPoints[_spawnIndex];
                if (targetSpawn != null)
                {
                    spawnPos = targetSpawn.position;
                    spawnRot = targetSpawn.rotation;
                }

                _spawnIndex = (_spawnIndex + 1) % spawnPoints.Count;
            }
            else
            {
                Debug.LogWarning("No spawn points assigned to PlayerSpawner! Defaulting to Vector3.zero.");
            }

            // Spawn the player cleanly at the designated spot
            var playerObj = runner.Spawn(
                playerPrefab,
                spawnPos,
                spawnRot,
                player
            );
            
            runner.SetPlayerObject(player, playerObj);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            var playerObj = runner.GetPlayerObject(player);
            if (playerObj != null)
            {
                runner.Despawn(playerObj);
            }
        }
    }

    // --- Rest of your original interface callbacks remain identical ---
    public void OnInput(NetworkRunner runner, NetworkInput input) {}
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { SceneManager.LoadScene("Lobby"); }
    public void OnConnectedToServer(NetworkRunner runner) {}
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { SceneManager.LoadScene("Lobby"); }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {}
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {}
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {}
    public void OnSceneLoadDone(NetworkRunner runner) {}
    public void OnSceneLoadStart(NetworkRunner runner) {}
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {}
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) {}
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) {}
}