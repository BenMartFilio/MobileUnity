using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    public float minDelay = 1f;
    public float maxDelay = 3f;
    public bool isSpawning = false;
    [SerializeField] private TimeManager _timeManager;

    private Coroutine spawning;

    private List<GameObject> poolObject = new List<GameObject>();
    private List<int> shuffleBag = new List<int>();

    private float generalSpeed = 10;


    private void OnEnable()
    {
        _timeManager.OnTimePassed += SpawnRate;
    }

    private void OnDisable()
    {
        _timeManager.OnTimePassed -= SpawnRate;
    }

    private void SpawnRate()
    {
        generalSpeed = Mathf.Clamp(generalSpeed + 1, 5, 30);
    }


    IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            Spawn();
        }
    }

    void Spawn()
    {
        int index = GetNextObstacleIndex();

        GameObject obj = GetFromPool(obstacles[index]);

        obj.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        obj.SetActive(true);
        obj.GetComponent<ScrollingElement>().speed = generalSpeed;
    }

    int GetNextObstacleIndex()
    {
        if (shuffleBag.Count == 0)
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                shuffleBag.Add(i);
            }

            for (int i = 0; i < shuffleBag.Count; i++)
            {
                int aleatoire = Random.Range(i, shuffleBag.Count);
                (shuffleBag[i], shuffleBag[aleatoire]) = (shuffleBag[aleatoire], shuffleBag[i]);
            }
        }

        int index = shuffleBag[0];
        shuffleBag.RemoveAt(0);

        return index;
    }

    GameObject GetFromPool(GameObject prefab)
    {
        foreach (GameObject obj in poolObject)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(prefab.name))
            {
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab);
        poolObject.Add(newObj);

        return newObj;
    }

    public void StartSpawning()
    {
        isSpawning = true;

        if (spawning == null)
            spawning = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        isSpawning = false;

        if (spawning != null)
        {
            StopCoroutine(spawning);
            spawning = null;
        }
        foreach (GameObject b in poolObject)
        {
            b.GetComponent<ScrollingElement>().StopMoving();
        }
    }
}