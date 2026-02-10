using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private ObjectsMovement[] _fallingLines;
    [SerializeField] private GameObject _ObjectToSpawn;

    [SerializeField] private int _spawnTimer = 0;
    [SerializeField] private int _spawnDelayDuration = 3;
    private int randomNumber;

    private void OnEnable()
    {
        _timeManager.OnTimePassed += TimeGestion;
    }
    private void OnDisable()
    {
        _timeManager.OnTimePassed -= TimeGestion;
    }

    private void TimeGestion()
    {
        _spawnTimer++;
        if(_spawnTimer >= _spawnDelayDuration)
        {
            randomNumber = Random.Range(0, _fallingLines.Length);
            _spawnTimer = 0;
            _fallingLines[randomNumber].Init((Instantiate(_ObjectToSpawn)));
        }
    }
}
