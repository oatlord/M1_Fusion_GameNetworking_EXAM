using UnityEngine;
public class ReadyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null && p.Object.HasStateAuthority)
        {
            p.IsReady = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null && p.Object.HasStateAuthority)
        {
            p.IsReady = false;
        }
    }
}