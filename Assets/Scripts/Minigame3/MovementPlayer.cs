using System.Collections;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Deplacement());
    }

    IEnumerator Deplacement()
    {
        float t = 0f;
        float duration = 2f;

        Vector2 startPos = rb.position;
        Vector2 endPos = startPos + Vector2.right * 2f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            rb.MovePosition(Vector2.Lerp(startPos, endPos, t));

            yield return null;
        }
    }
}
