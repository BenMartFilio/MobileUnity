using UnityEngine;

public class ScrollingElement : MonoBehaviour
{
    void Update()
    {
        float speed = 1;

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
