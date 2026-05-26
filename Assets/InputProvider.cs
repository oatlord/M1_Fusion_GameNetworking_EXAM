using Fusion;
using UnityEngine;
public class InputProvider : MonoBehaviour
{
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();
        data.direction = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;
        data.jumpPressed = Input.GetKey(KeyCode.Space);
        data.ballActionPressed = Input.GetMouseButton(0); // Left click for ball action
        data.ballReleasedPressed = Input.GetMouseButton(1); // Right click for ball release
        input.Set(data);
    }
}