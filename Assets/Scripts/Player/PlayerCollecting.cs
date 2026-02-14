using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
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
    }
}
