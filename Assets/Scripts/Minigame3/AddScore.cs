using UnityEngine;

public class AddScore : MonoBehaviour
{
    [SerializeField] private PlayerCollecting playerCollect;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private GameObject SpawnPoint;
    float speed = 7;
    private bool isMoving = false;

    private void OnEnable()
    {
        _timeManager.OnTimePassed += SpawnAndMove;
    }

    private void OnDisable()
    {
        _timeManager.OnTimePassed -= SpawnAndMove;
    }


    void Update()
    {
        if (!isMoving)
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -20)
        {
            isMoving = false;
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCollecting player = collision.GetComponent<PlayerCollecting>();
        if (player == null)
        {
            return;
        }
        playerCollect.AddScoreAndLife();
    }


    public void SpawnAndMove()
    {
        transform.position = SpawnPoint.transform.position;
        isMoving = true;
        speed = Mathf.Clamp(speed + 1, 5, 27);
    }
}
