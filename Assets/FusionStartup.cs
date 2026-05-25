using UnityEngine;
using Fusion;

public class FusionStartup : MonoBehaviour
{
    public FusionBootstrap fusionBootstrap;

    void Start() // Changed from Awake
    {
        if (fusionBootstrap == null)
            fusionBootstrap = FindObjectOfType<FusionBootstrap>();

        if (fusionBootstrap == null)
        {
            Debug.LogError("FusionStartup requires a FusionBootstrap in the scene.");
            return;
        }

        switch (NetworkModeSelector.Mode)
        {
            case FusionSessionMode.Host:
                Debug.Log("Starting Fusion as Host.");
                fusionBootstrap.StartHost();
                break;
            case FusionSessionMode.Client:
                Debug.Log("Starting Fusion as Client.");
                fusionBootstrap.StartClient();
                break;
            case FusionSessionMode.AutoHostOrClient:
                Debug.Log("Starting Fusion as AutoHostOrClient.");
                fusionBootstrap.StartHost();
                break;
            case FusionSessionMode.Single:
                Debug.Log("Starting Fusion as Single.");
                fusionBootstrap.StartSinglePlayer();
                break;
        }
    }
}