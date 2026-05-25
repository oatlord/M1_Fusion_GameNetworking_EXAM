using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void StartHost()
    {
        NetworkModeSelector.Mode = FusionSessionMode.Host;
        SceneManager.LoadScene("Game");
    }

    public void StartClient()
    {
        NetworkModeSelector.Mode = FusionSessionMode.Client;
        SceneManager.LoadScene("Game");
    }
}