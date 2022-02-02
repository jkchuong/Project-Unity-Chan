using UnityEngine;

namespace Assets.Project.Scripts.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform obstacleOriginLocation; // parent position
        [SerializeField] private float maxLifetime = 5; // in seconds
        [SerializeField] private float obstacleSpeed = 10.0f;
        [SerializeField] private float minObjSpeed = 10f;
        [SerializeField] private float maxObjSpeed = 30f;

        private float lifetime;
        private Vector3 velocity;
        // private Vector3 direction = new Vector3(0, 0, -1);
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
            velocity = Vector3.back;
            velocity = transform.TransformDirection(velocity);
            velocity *= obstacleSpeed;
            transform.localPosition += velocity * Time.deltaTime;
        }
        
        // resets this obstacle from a random delta from origin position
        public void ResetObstacle()
        {            
            var randomDeltaPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(0f, 1.0f), Random.Range(0f, 2.0f)); ; //height,sides,towardsYou
            var randomObjSpeed = Random.Range(minObjSpeed, maxObjSpeed);

            lifetime = 0;
            gameObject.transform.position = obstacleOriginLocation.position + randomDeltaPosition;
            obstacleSpeed = randomObjSpeed;
        }        
    }
}
