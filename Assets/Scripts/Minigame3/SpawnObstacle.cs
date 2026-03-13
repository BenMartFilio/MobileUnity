using System.Collections;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    public float minDelay = 1f;
    public float maxDelay = 3f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);

            yield return new WaitForSeconds(delay);

            Spawn();
        }
    }

    void Spawn()
    {
        int index = Random.Range(0, obstacles.Length);

        Instantiate(obstacles[index], transform.position, Quaternion.identity);
    }
}
