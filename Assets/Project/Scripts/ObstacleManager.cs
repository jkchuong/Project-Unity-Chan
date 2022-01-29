using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Project.Scripts.Obstacles;

namespace Assets.Project.Scripts.Obstacles
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject[] allObstacles;
        #endregion

        private void Start()
        {
            // sound
            //_audioSource.Play();
        }

        private void Update()
        {
            ManualSpawn();
        }
        private void ManualSpawn()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var freeObstacle = allObstacles[GetObstacleFromBag()].GetComponent<Obstacle>();
                freeObstacle.ResetObstacle();
                freeObstacle.ActivateThis();                
            }            
        }

        private int GetObstacleFromBag()
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
