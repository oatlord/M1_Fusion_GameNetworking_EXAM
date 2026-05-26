using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BallTrigger : NetworkBehaviour
{
    [Networked] public Ball ballParent { get; set; }
    // [Networked] public bool PickedUp { get; set; } = false;
    // private bool playerInRange;

    public override void Spawned()
    {
        ballParent = GetComponentInParent<Ball>();
    }

    void Awake()
    {
    }

    void Update()
    {
        // Debug.Log("Player holding ball/last held by: " + (ballParent.playerHoldingBall != null ? ballParent.playerHoldingBall.PlayerName.ToString() : "None"));
    }

    public override void FixedUpdateNetwork()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            if (p.Object == null || !p.Object.IsValid) continue;

            if (p.HasBall) 
            {
                ballParent.playerHoldingBall = p;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.Object.HasStateAuthority)
            {
                player.CanPickUpBall = true;
                // playerInRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.Object.HasStateAuthority)
            {
                player.CanPickUpBall = false;
                // playerInRange = false;
            }
        }
    }
}
