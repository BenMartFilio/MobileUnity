using System.Collections;
using UnityEngine;

public class IndicationDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] _skin;
    [SerializeField] private SpriteRenderer Affichage;
    [SerializeField] private SpawnerManager spawner;
    private float timeToWaitSpawn = 0.8f;
    private int lenghtOfList;

    [SerializeField] private TimeManager timeManager;

    private int _ID;

    private void OnEnable()
    {
        timeManager.OnTimePassed += NewWave;
    }
    private void OnDisable()
    {
        timeManager.OnTimePassed -= NewWave;
    }

    private void Start()
    {
        lenghtOfList = _skin.Length;
        spawner.DefineLenghtList(lenghtOfList);
        NewWave();
    }

    private void NewWave()
    {
        _ID = Random.Range(0, lenghtOfList);
        Affichage.sprite = _skin[_ID];
        StartCoroutine(HideEnding());
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(timeToWaitSpawn);
        spawner.OnSpawning(_ID);
    }

    public IEnumerator HideEnding()
    {
        Color c = Affichage.color;
        c.a = 0f;
        Affichage.color = c;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t);
            Affichage.color = c;

            yield return null;
        }
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0.6f, t);
            Affichage.color = c;

            yield return null;
        }

        c.a = 0.6f;
        Affichage.color = c;
        StartCoroutine(WaitBeforeSpawn());
    }
}
