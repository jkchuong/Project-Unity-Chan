using System.Collections;
using UnityEngine;

namespace Assets.Project.Scripts.Obstacles
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject[] allObstacles; // list of prefabs outside
        [SerializeField] private float fastestSpawnRate;
        [SerializeField] private float lowestSpawnRate;
        #endregion

        private void Start()
        {
            // init random spawning coroutine
            fastestSpawnRate = 0.2f;
            lowestSpawnRate = 1f;
            StartCoroutine(RandomSpawning(1));            
        }

        private void Update()
        {

        }

        // random spawn coroutine function that changes the spawn rate every time
        private IEnumerator RandomSpawning(float spawnInterval)
        {         
            yield return new WaitForSeconds(spawnInterval);
            while (true)
            {
                SpawnObstacle();

                #if UNITY_EDITOR
                    Debug.Log($"Spawning every {spawnInterval}s");
                #endif                
                
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

                #if UNITY_EDITOR
                    Debug.Log("Spawning");
                #endif

                var freeObstacle = allObstacles[refFreeObstacle].GetComponent<Obstacle>();
                freeObstacle.ResetObstacle();
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
            #if UNITY_EDITOR
                Debug.LogWarning("No available obstacle to get");
            #endif

            return false;
        }
    }
}
