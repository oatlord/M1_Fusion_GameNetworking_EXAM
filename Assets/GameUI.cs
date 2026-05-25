using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class GameUI : MonoBehaviour
{
    public Button leaveButton;

    void Start()
    {
        if (leaveButton != null)
        {
            leaveButton.onClick.AddListener(LeaveGame);
        }
    }

    public void LeaveGame()
    {
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner != null)
        {
            runner.Shutdown();
        }
    }
}