using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using static SO_PlayerDatas;

public class EndGestion : MonoBehaviour
{
    [SerializeField] private PlayerCollecting playerCollect;
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private SaveGameSystem save;
    [SerializeField] private TimeManager time;
    [SerializeField] private TMP_Text scoresToChange;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private MiniGameType currentMiniGame;
    [SerializeField] private GameObject returnToMenuButton;
    [SerializeField] private GameObject nameButton;
    private CanvasGroup canvaGroup = null;

    [SerializeField] private AudioEventDispatcher _AudioEventDispatcher;
    [SerializeField] private AudioType _DeathAudioType;

    public void OnEndGame()
    {
        _AudioEventDispatcher.PlayAudio(_DeathAudioType);
        int score = playerCollect.score;
        if (score > playerDatas.Score1)
        {
            playerDatas.Score1 = score;
        }
        save.SaveGame();
        time.StopTime();
        scoresToChange.text = $"Score : {score}";
        ScoringScreenApparition();
    }

    private void AddNewScore(string playerName, int score)
    {
        MiniGameHighScores gameHighScores = playerDatas.allHighScores
            .Find(g => g.gameType == currentMiniGame);

        if (gameHighScores == null)
        {
            gameHighScores = new MiniGameHighScores();
            gameHighScores.gameType = currentMiniGame;
            playerDatas.allHighScores.Add(gameHighScores);
        }

        gameHighScores.highScores.Add(new HighScoreEntry(playerName, score));

        gameHighScores.highScores = gameHighScores.highScores
            .OrderByDescending(x => x.score)
            .ToList();

        if (gameHighScores.highScores.Count > 5)
            gameHighScores.highScores.RemoveRange(5, gameHighScores.highScores.Count - 5);

        save.SaveGame();
    }

    public void OnValidateScore()
    {
        int score = playerCollect.score;
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
            playerName = "AAA";

        AddNewScore(playerName, score);
        DisplayHighScores();
        nameButton.SetActive(false);
        returnToMenuButton.SetActive(true);
    }

    private void DisplayHighScores()
    {
        MiniGameHighScores gameHighScores = playerDatas.allHighScores
            .Find(g => g.gameType == currentMiniGame);

        if (gameHighScores == null)
        {
            scoresToChange.text = "Aucun score enregistré";
            return;
        }

        string display = "HIGH SCORES\n";

        for (int i = 0; i < gameHighScores.highScores.Count; i++)
        {
            display += $"{i + 1}. {gameHighScores.highScores[i].playerName} - {gameHighScores.highScores[i].score}\n";
        }

        scoresToChange.text = display;
    }







    public void ScoringScreenApparition()
    {
        canvaGroup = endCanvas.GetComponent<CanvasGroup>();
        StartCoroutine(UnhideEnding());
    }

    public IEnumerator UnhideEnding()
    {
        var aValue = 0f;
        canvaGroup.blocksRaycasts = true;
        while (aValue < 1f)
        {
            if (canvaGroup != null)
            {
                canvaGroup.alpha = Mathf.Lerp(aValue, 1f, 0.6f);
                aValue += Time.deltaTime;
            }
            else
            {
                break;
            }

            yield return null;
        }
        canvaGroup.alpha = 1.0f;
        
        yield return null;
        
    }
}
