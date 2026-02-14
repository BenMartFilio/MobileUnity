using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TOUCHEEEE");
    }

    public void CollectingElement()
    {

    }
}
