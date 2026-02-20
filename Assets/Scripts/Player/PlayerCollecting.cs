using TMPro;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    [SerializeField] private SO_PlayerDatas playerDatas;
    [SerializeField] private TMP_Text scoreInputField;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private LifeSystem life;
    private int scoreToReachForNewSpeed = 10;
    [SerializeField] private AudioEventDispatcher _AudioEventDispatcher;
    [SerializeField] private AudioType _SpecialSoundWhenNewSpeed;
    [SerializeField] private AudioType _NormalGettingSound;

    public int score = 0;
    public float bonus = 1f;

    private void Start()
    {
        scoreInputField.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectsWhichFall objectCollected = collision.gameObject.GetComponent<ObjectsWhichFall>();
        if (objectCollected != null)
        {
            CollectingElement(objectCollected);
        }
    } 

    public void CollectingElement(ObjectsWhichFall toDestroy)
    {
        toDestroy.WhenGetted();
        score += (int) Mathf.Round(1*bonus);
        scoreInputField.text = score.ToString();
        if (score > scoreToReachForNewSpeed)
        {
            timeManager.UpdateSpeedTimer(Mathf.Clamp(timeManager._timeStepDuration * 0.9f, 0.3f, 1.5f)); // 15 étapes avant la vitesse finale
            scoreToReachForNewSpeed += 10;
            _AudioEventDispatcher.PlayAudio(_SpecialSoundWhenNewSpeed); //METTRE SON QUAND NEW SPEED
            life.BonusLife();
        }
        else
        {
            _AudioEventDispatcher.PlayAudio(_NormalGettingSound); // METTRE SON NORMAL GETTING
        }
    }
}
