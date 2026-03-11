using UnityEngine;

public class GroundDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SliceObject>() != null)
        {
            collision.GetComponent<SliceObject>().DestroyObject();
        }
    }
}
