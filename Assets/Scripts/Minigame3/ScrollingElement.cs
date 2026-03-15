using UnityEngine;

public class ScrollingElement : MonoBehaviour
{
    public float speed = 10;
    private bool isMoving = true;

    void Update()
    {
        if (!isMoving)
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }
    }


    public void StopMoving()
    {
        isMoving = false;
    }

}