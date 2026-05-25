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
        );
        data.jumpPressed = Input.GetKey(KeyCode.Space);
        input.Set(data);
    }
}