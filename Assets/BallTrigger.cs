using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BallTrigger : MonoBehaviour
{
    [Networked] public bool PickedUp { get; set; } = false;
    void Awake()
    {
    }
    void Update()
    {
        if (PickedUp)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
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
            }
        }
    }
}
