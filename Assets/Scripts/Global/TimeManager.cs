using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _timeStepDuration = 3.0f;

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
        StartCoroutine(SpendingTime());
    }

    public void StopTime()
    {
        StopCoroutine(SpendingTime());
    }
}
