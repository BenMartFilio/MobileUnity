using UnityEngine;

public class GoundMouvement : MonoBehaviour
{
    public float speed = 5f;
    public float width;
    public bool started = false;

    void Update()
    {
        if (!started)
        {
            return;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= -width)
        {
            transform.position += new Vector3(width * 2f, 0, 0);
        }
    }

    public void StartMove()
    {
        started = true;
    }

    public void StopMove()
    {
        started = false;
    }
}
