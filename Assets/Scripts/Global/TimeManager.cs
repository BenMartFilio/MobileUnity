using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private SO_PlayerDatas playerDatas;

    [SerializeField] private float normalSpeed = 1f;
    [SerializeField] private float hardSpeed = 0.5f;

    private float currentSpeed;

    [SerializeField] public float _timeStepDuration = 1.5f;
    Coroutine coroutineTemps = null;
    public event Action OnTimePassed;

    IEnumerator SpendingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeStepDuration*currentSpeed);
            OnTimePassed?.Invoke();
        }
    }

    public void Start()
    {
        StartTime();
    }

    public void StartTime()
    {
        if (playerDatas.selectedDifficulty == Difficulty.Hard)
            currentSpeed = hardSpeed;
        else
            currentSpeed = normalSpeed;
        coroutineTemps = StartCoroutine(SpendingTime());
    }

    public void StopTime()
    {
        StopCoroutine(coroutineTemps);
    }

    public void UpdateSpeedTimer(float newTime)
    {
        _timeStepDuration = newTime;
    }
}

public enum Difficulty
{
    Normal,
    Hard
}
