using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawner;
    private GameObject _ObjectSpawning;
    [SerializeField] private GameObject _ObjectToSpawn;
    private int GoodID;
    private int LenghtFruit;
    private int numberToSpawn = 1;
    private float accumulatorToNumber = 2;
    [SerializeField] private PlayerCollecting playerCollect;
    [SerializeField] private LifeSystem life;
    [SerializeField] private FlashScreen flash;

    public float forceX = 4f;
    public float travelTime = 1.2f;
    public Transform targetPoint;

    public float minDistance = 1.5f;
    private List<Vector2> spawnPositions = new List<Vector2>();

    public void DefineLenghtList(int lenghtList)
    {
        LenghtFruit = lenghtList;
    }
    public void OnSpawning(int ID)
    {
        GoodID = ID;

        SpawnElement(numberToSpawn);

        accumulatorToNumber = Mathf.Clamp(accumulatorToNumber + 1 * 0.1f, 1, 9);
        numberToSpawn = Mathf.FloorToInt(accumulatorToNumber);
        
    }

    public void SpawnElement(int howMuchSpawn)
    {
        SpawnFruit(GoodID);
        for (int i = 0; i < numberToSpawn-1; i++)
        {
            SpawnFruit(Random.Range(0,LenghtFruit));
        }
    }

    void SpawnFruit(int ID)
    {
        Vector2 spawnPos = GetSpawnPosition();

        _ObjectSpawning = Instantiate(_ObjectToSpawn);
        _ObjectSpawning.transform.position = spawnPos;
        SliceObject slice = _ObjectSpawning.gameObject.GetComponent<SliceObject>();
        ChangeSkin skin = _ObjectSpawning.gameObject.GetComponent<ChangeSkin>();
        if (slice != null)
        {
            slice.playerCollect = playerCollect;
            slice.flash = flash;
            slice.life = life;
            slice.ID = ID;
            slice.WhichIsGoodID = GoodID;
            skin.skinIndex = ID;
        }

        Rigidbody2D rb = _ObjectSpawning.GetComponent<Rigidbody2D>();

        Vector2 target = targetPoint.position;

        Vector2 gravity = Physics2D.gravity * rb.gravityScale;

        Vector2 velocity =
            (target - spawnPos) / travelTime -
            0.5f * gravity * travelTime;

        rb.linearVelocity = velocity;

        rb.angularVelocity = Random.Range(-200f, 200f);
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 pos;
        int attempts = 0;

        do
        {
            float x = Random.Range(spawner[0].transform.position.x, spawner[1].transform.position.x);
            pos = new Vector2(x, spawner[0].transform.position.y);

            attempts++;
            if (attempts > 50) break;

        } while (TooClose(pos));

        spawnPositions.Add(pos);
        return pos;
    }

    bool TooClose(Vector2 pos)
    {
        foreach (Vector2 p in spawnPositions)
        {
            if (Vector2.Distance(p, pos) < minDistance)
                return true;
        }

        return false;
    }
}
