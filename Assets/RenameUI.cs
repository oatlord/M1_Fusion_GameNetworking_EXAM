using UnityEngine;
using TMPro;
using Fusion;

public class RenameUI : MonoBehaviour
{
    public TMP_InputField nameInput;
    public void RenamePlayer()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text)) return;
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            var playerObj = runner.GetPlayerObject(runner.LocalPlayer);
            if (playerObj != null)
            {
                var player = playerObj.GetComponent<Player>();
                if (player != null)
                {
                    player.SetName(nameInput.text.Trim());
                    Debug.Log("Renamed to " + nameInput.text.Trim());
                    gameObject.SetActive(false); // Hide UI after rename
                }
            }
        }
    }
}