using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Assets.Project.Scripts.Obstacles;

namespace Assets.Project.Scripts.Obstacles
{    public class Obstacle : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform obstacleOriginLocation;
        [SerializeField] private float maxLifetime = 5; // in seconds
        [SerializeField] private float obstacleSpeed = 8.0f;

        private float lifetime;
        private Vector3 velocity;
        private Vector3 direction = new Vector3(0, 0, -1);
        #endregion

        public void ActivateThis() => gameObject.SetActive(true);
        public void DeactivateThis() => gameObject.SetActive(false);

        private void Awake()
        {
            DeactivateThis();
        }

        // Update is called once per frame
        void Update()
        {
            // on creation then move
            Move();
            
            // when this obstacle is out of life reset it
            lifetime += Time.deltaTime;
            if (lifetime > maxLifetime)
            {
                DeactivateThis();
                ResetObstacle();
            }
        }
        
        //Move object at specific direction and speed
        public void Move()
        {
            velocity = direction;
            velocity = transform.TransformDirection(velocity);
            velocity *= obstacleSpeed;
            transform.localPosition += velocity * Time.deltaTime;
        }
        
        // set as inactive and reset position
        public void ResetObstacle()
        {
            lifetime = 0;
            var randomPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(0f, 1.0f), Random.Range(0f, 2.0f)); ; //height,sides,towardsYou
            gameObject.transform.position = obstacleOriginLocation.position + randomPosition;
        }        
    }
}
