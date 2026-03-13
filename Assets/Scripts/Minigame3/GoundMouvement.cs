using UnityEngine;

public class GoundMouvement : MonoBehaviour
{
    public float speed = 5f;
    public float width;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }
}
