using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GoalZone : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // BallTrigger ballTrigger = other.GetComponentInChildren<BallTrigger>();
            Ball ball = other.GetComponent<Ball>();
            if (ball != null)
            {
                Debug.Log("Ball entered the goal zone! Last held by: " + (ball.playerHoldingBall != null ? ball.playerHoldingBall.PlayerName.ToString() : "None"));
                ball.playerHoldingBall.Score += 1; // Increment the player's score
                Runner.Despawn(ball.Object); // Despawn the ball
            }

            Debug.Log("Ball entered the goal zone!");
        }
    }
}
