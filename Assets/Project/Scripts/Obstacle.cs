using System;
using UnityChan;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Project.Scripts.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform obstacleOriginLocation; // parent position
        [SerializeField] private float maxLifetime = 30; // in seconds
        [SerializeField] private float obstacleSpeed = 10.0f;
        [SerializeField] private float minObjSpeed = 5f;
        [SerializeField] private float maxObjSpeed = 10f;
        [SerializeField] private float speedIncreaseFactor = 0.003f;

        public float timeGameBegan;
        private float lifetime;
        private Vector3 velocity;
        private Vector3 direction;
        #endregion

        public void ActivateThis() => gameObject.SetActive(true);
        public void DeactivateThis() => gameObject.SetActive(false);

        private void Awake()
        {
            DeactivateThis();
        }

        private void Start()
        {
            direction = transform.forward * -1;
        }

        // Update is called once per frame
        void Update()
        {
            // obstacle always moving
            Move();

            // when out of life reset it
            lifetime += Time.deltaTime;
            if (lifetime > maxLifetime)
            {
                DeactivateThis();
                ResetObstacle();
            }
        }
        
        //  move object at specific direction and speed
        public void Move()
        {
            velocity = direction;
            velocity = transform.TransformDirection(velocity);
            velocity *= obstacleSpeed;
            transform.localPosition += velocity * Time.deltaTime;
        }
        
        // resets this obstacle from a random delta from origin position
        public void ResetObstacle()
        {            
            var randomDeltaPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 0f), Random.Range(0f, 2.0f)); ; //height,sides,towardsYou
            var randomObjSpeed = Random.Range(minObjSpeed, maxObjSpeed)  * (1 + ((Time.time - timeGameBegan) * speedIncreaseFactor));

            lifetime = 0;
            gameObject.transform.position = obstacleOriginLocation.position + randomDeltaPosition;
            obstacleSpeed = randomObjSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.GetComponent<UnityChanControlScript>()) return;
            FindObjectOfType<CinemachineShake>().ShakeCamera();
        }
    }
}
