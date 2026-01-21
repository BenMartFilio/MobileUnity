using UnityEngine;
using System;

public class ObjectsMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    private int _index = -1;
    [SerializeField] private TimeManager _timeManager;

    private void OnEnable()
    {
        _timeManager.OnTimePassed += MoveObject;
    }

    private void OnDisable()
    {
        _timeManager.OnTimePassed -= MoveObject;
    }

    private void MoveObject()
    {
        _index++;
        if (_index < _transforms.Length)
        {
            transform.position = _transforms[_index].position;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
