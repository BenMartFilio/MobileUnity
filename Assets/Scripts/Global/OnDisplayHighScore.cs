using UnityEngine;
using TMPro;

public class OnDisplayHighScore : MonoBehaviour
{
    public SO_PlayerDatas playerDatas;

    public TMP_Text[] miniGame1Texts;
    public TMP_Text[] miniGame2Texts;
    public TMP_Text[] miniGame3Texts;

    public GameObject highScoreCanvas;

    public void ShowHighScores()
    {
        DisplayMiniGameScores(MiniGameType.GameAndWatch, miniGame1Texts);
        DisplayMiniGameScores(MiniGameType.PizzaTime, miniGame2Texts);
        DisplayMiniGameScores(MiniGameType.DeliveryTime, miniGame3Texts);

        highScoreCanvas.SetActive(true);
    }

    void DisplayMiniGameScores(MiniGameType type, TMP_Text[] texts)
    {
        MiniGameHighScores gameScores = playerDatas.allHighScores.Find(g => g.gameType == type);

        if (gameScores == null) return;

        for (int i = 0; i < texts.Length && i < gameScores.highScores.Count; i++)
        {
            var entry = gameScores.highScores[i];
            texts[i].text = (i + 1) + ". " + entry.playerName + " - " + entry.score;
        }
    }
}
