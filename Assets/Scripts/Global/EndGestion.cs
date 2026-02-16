using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EndGestion : MonoBehaviour
{
    [SerializeField] private PlayerCollecting playerCollect;
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private SaveGameSystem save;
    [SerializeField] private TimeManager time;

    public void OnEndGame()
    {
        int score = playerCollect.score;
        if (score > playerDatas.Score1)
        {
            playerDatas.Score1 = score;
        }
        save.SaveGame();
        time.StopTime();
        Debug.Log($"Vous avez fait ce score : {score}");
    }
}
