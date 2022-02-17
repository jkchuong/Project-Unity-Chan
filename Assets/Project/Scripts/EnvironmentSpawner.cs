using DG.Tweening;
using UnityChan;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform[] endPositions;
    [SerializeField] private GameObject[] environmentPrefabs;
    [SerializeField] private float timeToMoveFullPath = 4f;
    [SerializeField] private float minTimeBetweenSpawns = 0.5f;
    [SerializeField] private float maxTimeBetweenSpawns = 2f;
    public bool isMoving = true;
    private int NumberOfPrefabs => environmentPrefabs.Length;
    private float nextSpawnTime;
    private UnityChanControlScript unityChan;

    private void Start()
    {
        unityChan = FindObjectOfType<UnityChanControlScript>();

        if (unityChan)
        {
            unityChan.OnDeath += delegate { isMoving = false; };
        }
        
        nextSpawnTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns) + Time.time;
    }

    private void Update()
    {
        if (isMoving && Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns) + Time.time;
        }
    }

    private void SpawnObstacle()
    {
        int randomSpawnPosition = Random.Range(0, 2);
        int randomPrefab = Random.Range(0, NumberOfPrefabs);
        GameObject spawnedEnv = Instantiate(environmentPrefabs[randomPrefab], spawnPositions[randomSpawnPosition]);
        spawnedEnv.transform.DOMoveZ(endPositions[randomSpawnPosition].position.z, timeToMoveFullPath).SetEase(Ease.Linear)
            .OnComplete(delegate { Destroy(spawnedEnv); });
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(spawnPositions[0].position, endPositions[0].position);
        Gizmos.DrawLine(spawnPositions[1].position, endPositions[1].position);
    }
}
