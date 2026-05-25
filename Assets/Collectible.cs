using UnityEngine;
using Fusion;
public class Collectible : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            // Request the server to collect this
            CollectRPC(p.Object);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void CollectRPC(NetworkObject playerObj)
    {
        if (playerObj == null) return;
        
        Player p = playerObj.GetComponent<Player>();
        if (p != null && Object.HasStateAuthority)
        {
            p.Score += 1;
            if (GameManager.Instance != null)
                GameManager.Instance.CollectiblesLeft--;
            Runner.Despawn(Object);
        }
    }
}