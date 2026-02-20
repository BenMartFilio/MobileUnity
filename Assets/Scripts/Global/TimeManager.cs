using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] public float _timeStepDuration = 1.5f;
    Coroutine coroutineTemps = null;
    public event Action OnTimePassed;

    IEnumerator SpendingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeStepDuration);
            OnTimePassed?.Invoke();
        }
    }

    public void Start()
    {
        StartTime();
    }

    public void StartTime()
    {
        coroutineTemps = StartCoroutine(SpendingTime());
    }

    public void StopTime()
    {
        StopCoroutine(coroutineTemps);
    }
}
