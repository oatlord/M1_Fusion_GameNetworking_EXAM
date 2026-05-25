using UnityEngine;
using Fusion;
using TMPro;
using ExitGames.Client.Photon.StructWrapping;
public class Player : NetworkBehaviour
{
    CharacterController controller;
    public float speed = 5f;
    public float jumpForce = 5f;
    float gravity = -9.81f;
    Vector3 velocity;
    [Networked] public int Score { get; set; }
    [Networked] public bool IsReady { get; set; }
    [Networked] public NetworkString<_16> PlayerName { get; set; }
    [Networked] public bool CanPickUpBall {get; set;}
    [Networked] private bool HasBall { get; set; } = false;
    public TextMeshPro nameText;
    public override void Spawned()
    {
        controller = GetComponent<CharacterController>();
        if (HasStateAuthority)
            PlayerName = "Player " + Object.InputAuthority.PlayerId;
    }
    public void SetName(string name)
    {
        if (HasInputAuthority)
        {
            SetNameRPC(name);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void SetNameRPC(string name)
    {
        if (HasStateAuthority)
        {
            PlayerName = name;
            Debug.Log("Player name set to: " + name);
        }
    }
    void Update()
    {
        if (nameText != null)
            nameText.text = PlayerName.ToString();

        Debug.Log("Can pick up ball: " + CanPickUpBall);
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Vector3 move = new Vector3(data.direction.x, 0, data.direction.y);
            controller.Move(move * speed * Runner.DeltaTime);

            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Runner.DeltaTime;

            if (data.jumpPressed && controller.isGrounded)
            {
                velocity.y = jumpForce;
            }

            if (data.ballActionPressed && CanPickUpBall && !HasBall)
            {
                Debug.Log("Ball action triggered!");

                GameObject ball = GameObject.FindWithTag("Ball");
                // BallTrigger ballTrigger = GetComponentInChildren<BallTrigger>();
                if (ball != null)
                {
                    ball.transform.SetParent(transform);
                    ball.transform.localPosition = new Vector3(0, 1, 0);
                    // ball.GetComponent<Rigidbody>().isKinematic = true;
                    // ballTrigger.PickedUp = true;
                    CanPickUpBall = false;
                    HasBall = true;
                }
            }

            if (data.ballReleasedPressed && HasBall)
            {
                Debug.Log("Dropping the ball!");

                Transform ball = transform.Find("Ball");
                // GameObject ball = GameObject.FindWithTag("Ball");
                // BallTrigger ballTrigger = GetComponentInChildren<BallTrigger>();

                if (ball != null)
                {
                    ball.transform.SetParent(null);
                    // ball.GetComponent<Rigidbody>().isKinematic = false;
                    // ball.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
                    // ball.transform.position = transform.position + transform.forward * 2;
                    CanPickUpBall = true;
                    HasBall = false;
                    // ballTrigger.PickedUp = false;
                }
            }

            // controller.Move(velocity * Runner.DeltaTime);
        }
    }
}