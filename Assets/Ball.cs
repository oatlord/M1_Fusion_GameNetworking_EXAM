using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Ball : NetworkBehaviour
{
    [Networked] public Player playerHoldingBall { get; set; }

    public override void Spawned()
    {
        playerHoldingBall = null;
    }
}
