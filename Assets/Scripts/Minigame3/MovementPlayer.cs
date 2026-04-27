using System;
using System.Collections;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer m_SpriteRenderer;

    [SerializeField] private LastInputSystem3 input;
    private bool firstTime = true;
    private Coroutine deplace;
    private Coroutine jump;
    private Coroutine lanceurCoroutine;

    [SerializeField] private SpawnObstacle spawner;
    [SerializeField] private GoundMouvement[] groundMove;
    private bool isPlaying = true;
    private bool isWalking = false;

    [SerializeField] private DeathPlayer death;
    [SerializeField] private AddScore maison;
    [SerializeField] private TimeManager time;

    [SerializeField] private AudioEventDispatcher _AudioEventDispatcher;
    [SerializeField] private AudioType _WalkAudioType;
    [SerializeField] private AudioType _DeathAudioType;
    [SerializeField] private AudioType _JumpAudioType;

    [Header("Footsteps")]
    [SerializeField] private AudioSource _FootstepsAudioSource;

    [Header("Lanceur")]
    [SerializeField] private Sprite _LanceurSprite;
    private const float LanceurDuration = 0.5f;

    [Header("FlyPizza")]
    [SerializeField] private FlyPizzaLauncher _FlyPizzaLauncher;

    private Animator m_Animator;

    /// <summary>Raised when the player scores via the house, passes the house transform.</summary>
    public event Action<Transform> OnScored;

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
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        time.StopTime();
    }

    private void Update()
    {
        HandleFootsteps();
    }

    /// <summary>Plays footstep audio only when walking and grounded (not jumping).</summary>
    private void HandleFootsteps()
    {
        bool shouldPlay = isWalking && jump == null && isPlaying;

        if (shouldPlay && !_FootstepsAudioSource.isPlaying)
        {
            _FootstepsAudioSource.Play();
        }
        else if (!shouldPlay && _FootstepsAudioSource.isPlaying)
        {
            _FootstepsAudioSource.Stop();
        }
    }

    private void WhenTaped()
    {
        if (!isPlaying)
        {
            return;
        }
        if (deplace == null)
        {
            if (jump == null)
            {
                jump = StartCoroutine(Jump());
            }
        }
    }

    IEnumerator Deplacement()
    {
        isWalking = true;
        m_Animator.SetBool("IsWalking?", true);
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
    }

    IEnumerator Jump()
    {
        float jumpHeight = 7f;
        float duration = 0.8f;
        _AudioEventDispatcher.PlayAudio(_JumpAudioType);

        Vector2 startPos = rb.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            float height = 4 * jumpHeight * smoothT * (1 - smoothT);

            rb.MovePosition(new Vector2(startPos.x, startPos.y + height));
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

    /// <summary>Called by AddScore when the player successfully scores via the house.</summary>
    public void OnHouseScored(Transform houseTransform)
    {
        if (lanceurCoroutine != null)
        {
            StopCoroutine(lanceurCoroutine);
        }
        lanceurCoroutine = StartCoroutine(PlayLanceurSprite());

        if (_FlyPizzaLauncher != null)
        {
            _FlyPizzaLauncher.Launch(houseTransform);
        }
    }

    private IEnumerator PlayLanceurSprite()
    {
        Sprite originalSprite = m_SpriteRenderer.sprite;
        bool wasAnimatorEnabled = m_Animator.enabled;

        // Compensate for pivot difference between the two sprites so the character
        // stays visually at the same world position.
     /*   Vector3 positionCorrection = Vector3.Scale(
            originalSprite.bounds.center - _LanceurSprite.bounds.center,
            transform.localScale
        );*/

        m_Animator.enabled = false;
        m_SpriteRenderer.sprite = _LanceurSprite;
   //     transform.position += positionCorrection;

        yield return new WaitForSeconds(LanceurDuration);

        m_SpriteRenderer.sprite = originalSprite;
 //       transform.position -= positionCorrection;
        m_Animator.enabled = wasAnimatorEnabled;
        lanceurCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ScrollingElement>() == null)
        {
            return;
        }

        isWalking = false;
        if (_FootstepsAudioSource.isPlaying)
        {
            _FootstepsAudioSource.Stop();
        }

        spawner.StopSpawning();
        foreach (GoundMouvement a in groundMove)
        {
            a.StopMove();
        }
        StartCoroutine(Camera.main.GetComponent<ScreenShake>().Shake(0.2f, 0.15f));
        isPlaying = false;
        _AudioEventDispatcher.PlayAudio(_DeathAudioType);
        m_Animator.SetBool("IsWalking?", false);
        death.Death();
        maison.StopMoving();
    }
}
