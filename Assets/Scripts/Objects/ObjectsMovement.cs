using UnityEngine;
using System;

public class ObjectsMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    private int _index = -1;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private GameObject _ObjectFalling;
    [SerializeField] private LifeSystem life;

    [SerializeField] private AudioEventDispatcher _AudioEventDispatcher;
    [SerializeField] private AudioType _ObjectMovementAudioType;
    [SerializeField] private AudioType _DestructionAudioType;
    public event Action OnGround;


    public void Init(GameObject NewObject)
    {
        if (_ObjectFalling == null)
        {
            _ObjectFalling = NewObject;
            _index = -1;
          //  MoveObject();
        }
        else 
        {
            Destroy(NewObject);  // Supprime si un deuxième tente d'être sur la même ligne
        }
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
            _AudioEventDispatcher.PlayAudio(_ObjectMovementAudioType);
        }
        else if (_index == _transforms.Length-2)
        {
            _ObjectFalling.transform.position = _transforms[_index].position;
            //Mettre code où le joueur doit être à l'emplacement
        }
        else
        {
            // SI joueur pas là Détruire et enlever une vie
            Destroy(_ObjectFalling);
            _AudioEventDispatcher.PlayAudio(_DestructionAudioType);
            _index = -1;
            life.MinusLife();
        }
        
    }
}
