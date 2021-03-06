using System;
using System.Collections;
using UnityChan;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Project.Scripts.Obstacles
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject[] allObstacles; // list of prefabs outside
        [SerializeField] private float fastestSpawnRate;
        [SerializeField] private float lowestSpawnRate;
        [SerializeField] private float initialSpawnDelay = 5f;
        #endregion

        private float timeGameBegan;
        private Button startButton;
        private UnityChanControlScript unityChan;
        private Coroutine randomSpawningCoroutine;

        private void Start()
        {
            // init random spawning coroutine
            fastestSpawnRate = 0.2f;
            lowestSpawnRate = 1f;

            unityChan = FindObjectOfType<UnityChanControlScript>();
            unityChan.OnDeath += StopObstacleSpawning;
        }


        public void BeginObstacleSpawning()
        {
            timeGameBegan = Time.time;
            randomSpawningCoroutine = StartCoroutine(RandomSpawning(1));
        }

        private void StopObstacleSpawning()
        {
            StopCoroutine(randomSpawningCoroutine);
        }

        // random spawn coroutine function that changes the spawn rate every time
        private IEnumerator RandomSpawning(float spawnInterval)
        {
            yield return new WaitForSeconds(initialSpawnDelay);
            while (true)
            {
                SpawnObstacle();
                spawnInterval = Random.Range(fastestSpawnRate, lowestSpawnRate);
                yield return new WaitForSeconds(spawnInterval);
            }            
        }
        
        // spawn obstacle
        private void SpawnObstacle()
        {
            int refFreeObstacle = -1;
            if (TryGetAvailableObstacle(out refFreeObstacle))
            {
                var freeObstacle = allObstacles[refFreeObstacle].GetComponent<Obstacle>();
                freeObstacle.ResetObstacle();
                freeObstacle.timeGameBegan = timeGameBegan;
                freeObstacle.ActivateThis();
            }             
        }

        // TODO: refactor into dictionary to reduce the look up from O(n) to O(1)
        private bool TryGetAvailableObstacle(out int refAvailableObstacle)
        {
            refAvailableObstacle = 0;
            for (int i = 0; i < allObstacles.Length; i++)
            {
                if (!allObstacles[i].activeInHierarchy)
                {
                    refAvailableObstacle = i;
                    return true;
                }                    
            }

            return false;
        }

        private void OnDisable()
        {
            unityChan.OnDeath -= StopObstacleSpawning;
        }
    }
}
