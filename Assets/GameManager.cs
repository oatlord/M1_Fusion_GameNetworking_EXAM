using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    // [Networked] public int CollectiblesLeft { get; set; }
    [Networked] public bool GameEnded { get; set; }
    [Networked] public bool ballPresent { get; set; }

    public static GameManager Instance;
    // private bool collectablesInitialized;
    // private bool hasCollectibles;

    public override void Spawned()
    {
        if (Instance == null)
            Instance = this;
        if (Object.HasStateAuthority)
        {
            ballPresent = FindObjectsOfType<Ball>().Length > 0;
            Debug.Log("GameManager initialized. Ball present: " + ballPresent);
            // CollectiblesLeft = FindObjectsOfType<Collectible>().Length;
            // hasCollectibles = CollectiblesLeft > 0;
            // collectablesInitialized = true;
            // Debug.Log($"GameManager initialized with collectibles={CollectiblesLeft}, hasCollectibles={hasCollectibles}");
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (ballPresent)
        {
            return;
        }

        if (ballPresent == false)
        {
            GameEnded = true;
            Debug.Log("Ball is not present, going back to Lobby.");
            Runner.Shutdown();
        }
        // if (!collectablesInitialized || !Object.HasStateAuthority || GameEnded || !hasCollectibles)
        //     return;

        // if (CollectiblesLeft == 0)
        // {
        //     GameEnded = true;
        //     Debug.Log("All collectibles gone, going back to Lobby.");
        //     Runner.Shutdown();
        // }
    }
}