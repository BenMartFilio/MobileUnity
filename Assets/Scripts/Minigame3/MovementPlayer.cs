using System.Collections;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private LastInputSystem3 input;
    private bool firstTime = true;
    private Coroutine deplace;
    private Coroutine jump;

    private void OnEnable()
    {
        input.OnTapScreen += WhenTaped;
    }

    private void OnDisable()
    {
        input.OnTapScreen -= WhenTaped;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void WhenTaped()
    {
        if (firstTime)
        {
            firstTime = false;
            deplace = StartCoroutine(Deplacement());
        }
        else if (deplace == null)
        {
            if (jump  == null)
            {
                jump = StartCoroutine(Jump());
            }
        }
        
    }

    IEnumerator Deplacement()
    {
        float t = 0f;
        float duration = 1f;

        Vector2 startPos = rb.position;
        Vector2 endPos = startPos + Vector2.right * 2f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            rb.MovePosition(Vector2.Lerp(startPos, endPos, t));

            yield return null;
        }

        StopCoroutine(deplace);
        deplace = null;
        //Déclencher mouvement terrain
    }

    IEnumerator Jump()
    {
        float jumpHeight = 2.5f;
        float duration = 0.25f;

        Vector2 startPos = rb.position;
        Vector2 topPos = startPos + Vector2.up * jumpHeight;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            rb.MovePosition(Vector2.Lerp(startPos, topPos, t));
            yield return null;
        }

        t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            rb.MovePosition(Vector2.Lerp(topPos, startPos, t));
            yield return null;
        }

        jump = null;
    }
}
