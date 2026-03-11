using UnityEngine;

public class SliceCollision : MonoBehaviour
{
    [SerializeField] private SliceObject _slicedObject;
    [SerializeField] private GameObject sliced;
    public void OnExitCollision(Vector2 swipeDirection)
    {
        if (_slicedObject.getSlicedObject() == null)
        {
            _slicedObject.setSlicedObject(sliced);
            _slicedObject.Slice(swipeDirection);
        }
    }
}
