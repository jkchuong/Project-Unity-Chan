using UnityEngine;

namespace Assets.Project.Scripts.Obstacles
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject[] allObstacles;
        #endregion

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) SpawnObstacle();
        }
        
        // spawn obstacle
        private void SpawnObstacle()
        {            
            var freeObstacle = allObstacles[GetAvailableObstacle()].GetComponent<Obstacle>();
            freeObstacle.ResetObstacle();
            freeObstacle.ActivateThis(); 
        }

        // TODO: refactor into dictionary to reduce the look up from O(n) to O(1)
        private int GetAvailableObstacle()
        {
            for (int i = 0; i < allObstacles.Length; i++)
            {
                if (!allObstacles[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }
    }
}
