using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
using System.Collections;

public class EndGestion : MonoBehaviour
{
    [SerializeField] private PlayerCollecting playerCollect;
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private SaveGameSystem save;
    [SerializeField] private TimeManager time;
    [SerializeField] private TMP_Text scoresToChange;
    [SerializeField] private GameObject endCanvas;
    private CanvasGroup canvaGroup = null;

    public void OnEndGame()
    {
        int score = playerCollect.score;
        if (score > playerDatas.Score1)
        {
            playerDatas.Score1 = score;
        }
        save.SaveGame();
        time.StopTime();
        scoresToChange.text = $"Score : {score} \n High score : {playerDatas.Score1}";
        ScoringScreenApparition();
        Debug.Log($"Vous avez fait ce score : {score}");
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
