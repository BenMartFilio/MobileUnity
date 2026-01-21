using UnityEngine;
using System;

public class ObjectsMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    private int _index = -1;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private GameObject _ObjectFalling;


    public void Init(GameObject NewObject)
    {
        _ObjectFalling = NewObject;
        MoveObject();
    }

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
        if (_ObjectFalling == null) 
        {
            return;
        }
        _index++;
        if (_index < _transforms.Length)
        {
            _ObjectFalling.transform.position = _transforms[_index].position;
        }
        else
        {
            Destroy(_ObjectFalling);
            _index = -1;
        }
        
    }
}
