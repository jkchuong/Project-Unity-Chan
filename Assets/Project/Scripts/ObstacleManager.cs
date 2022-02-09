using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        
        private Button startButton;

        private void Start()
        {
            // init random spawning coroutine
            fastestSpawnRate = 0.2f;
            lowestSpawnRate = 1f;
            
            startButton = GameObject.Find("Start Button").GetComponent<Button>();
            startButton.onClick.AddListener(BeginObstacleSpawning);
        }


        private void BeginObstacleSpawning()
        {
            StartCoroutine(RandomSpawning(1));
        }

        // random spawn coroutine function that changes the spawn rate every time
        private IEnumerator RandomSpawning(float spawnInterval)
        {
            yield return new WaitForSeconds(initialSpawnDelay);
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
