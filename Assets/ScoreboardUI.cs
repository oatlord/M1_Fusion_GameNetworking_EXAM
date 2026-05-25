using UnityEngine;
using TMPro;
public class ScoreboardUI : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText;
    void Update()
    {
        Player[] players = FindObjectsOfType<Player>();
        scoreboardText.text = "";
        foreach (Player p in players)
        {
            if (p.Object == null || !p.Object.IsValid) continue;
            scoreboardText.text += 
                p.PlayerName + " | " +
                (p.IsReady ? "READY" : "NOT READY") + " | Score: " +
                p.Score + "\n";
        }
    }
}