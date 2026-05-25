using UnityEngine;
using Fusion;

public class ReadyZone : NetworkBehaviour
{
    [SerializeField] private GameObject lobbyUI;

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null && p.Object.HasStateAuthority)
        {
            p.IsReady = true;
            RPC_CheckAllPlayersReady();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null && p.Object.HasStateAuthority)
        {
            p.IsReady = false;
            RPC_CheckAllPlayersReady();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_CheckAllPlayersReady()
    {
        // Find all player scripts currently active in the game
        Player[] allPlayers = FindObjectsByType<Player>(FindObjectsSortMode.None);

        if (allPlayers.Length == 0) return;

        bool everyoneReady = true;

        foreach (Player p in allPlayers)
        {
            if (!p.IsReady)
            {
                everyoneReady = false;
                break;
            }
        }

        // If everyone is locked in, trigger the start for all clients
        if (everyoneReady)
        {
            RPC_StartGamePerspective();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_StartGamePerspective()
    {
        // 1. Goodbye ugly UI!
        if (lobbyUI != null)
        {
            lobbyUI.SetActive(false);
        }

        // 2. Find OUR local player and turn on their overhead camera
        Player[] allPlayers = FindObjectsByType<Player>(FindObjectsSortMode.None);
        foreach (Player p in allPlayers)
        {
            // We only want to activate the camera on the player WE control
            if (p.Object.HasStateAuthority && p.overheadCamera != null)
            {
                p.overheadCamera.SetActive(true);
                break;
            }
        }
    }
}