using UnityEngine;
using Fusion;
using TMPro;
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
            controller.Move(velocity * Runner.DeltaTime);
        }
    }
}