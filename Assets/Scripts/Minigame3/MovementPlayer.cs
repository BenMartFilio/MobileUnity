using System.Collections;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private LastInputSystem3 input;
    private bool firstTime = true;
    private Coroutine deplace;
    private Coroutine jump;
    [SerializeField] private SpawnObstacle spawner;
    [SerializeField] private GoundMouvement[] groundMove;
    private bool isPlaying = true;
    [SerializeField] private DeathPlayer death;
    [SerializeField] private AddScore maison;
    [SerializeField] private TimeManager time;

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
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        time.StopTime();
    }

    private void WhenTaped()
    {
        if (!isPlaying)
        {
            return;
        }
        if (deplace == null)
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
        float duration = 0.4f;

        Vector2 startPos = rb.position;
        Vector2 endPos = startPos + Vector2.right * 2f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            rb.MovePosition(Vector2.Lerp(startPos, endPos, t));

            yield return null;
        }

        spawner.StartSpawning();
        foreach (GoundMouvement a in groundMove)
        {
            a.StartMove();
        }
        StopCoroutine(deplace);
        deplace = null;
        //Déclencher mouvement terrain
    }

    IEnumerator Jump()
    {
        float jumpHeight = 7f;
        float duration = 0.8f;

        Vector2 startPos = rb.position;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            float height = 4 * jumpHeight * smoothT * (1 - smoothT);

            Vector2 pos = new Vector2(startPos.x, startPos.y + height);

            rb.MovePosition(pos);

            yield return null;
        }
        if (firstTime)
        {
            deplace = StartCoroutine(Deplacement());
            firstTime = false;
            time.StartTime();
        }

        rb.MovePosition(startPos);
        jump = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ScrollingElement>() == null)
        {
            return;
        }
        spawner.StopSpawning();
        foreach (GoundMouvement a in groundMove)
        {
            a.StopMove();
        }
        StartCoroutine(Camera.main.GetComponent<ScreenShake>().Shake(0.2f, 0.15f));
        isPlaying = false;
        death.Death();
        maison.StopMoving();
    }
}
