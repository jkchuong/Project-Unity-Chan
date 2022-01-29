using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Assets.Project.Scripts;

public class MoveObstacle : MonoBehaviour
{
    #region OutsideComponents
    private Rigidbody obstacle;
    #endregion

    #region Fields
    private Vector3 velocity;
    private Vector3 direction;
    public float objectSpeed = 8.0f;
    #endregion

    #region Object Pooling fields
    [SerializeField] private Transform obstacleOriginLocation;
    [SerializeField] private float maxLifetime = 5; //  5s 
    private float lifetime; //  5s 
    private ObjectPool _objectPoolingService;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<Rigidbody>();        
        direction = new Vector3(0, 0, -1); // default

        // object pooling
        //_objectPoolingService = new ObjectPool();
        //var x = _objectPoolingService.Bag["Cube"];
        //x.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction, objectSpeed);
        CheckObstacleLifeTime();
    }

    //Move object at specific direction and speed
    private void Move(Vector3 direction_, float speed)
    {
        velocity = direction_;
        velocity = transform.TransformDirection(velocity);
        velocity *= speed;

        // move object
        transform.localPosition += velocity * Time.deltaTime;
    }

    // object pooling driver code
    private void CheckObstacleLifeTime()
    {
        lifetime += Time.deltaTime;
        if (lifetime > maxLifetime) 
        {
            gameObject.SetActive(false);
            ResetObstacle();
        }

    }
    // set as inactive and reset position
    private void ResetObstacle()
    {        
        lifetime = 0;
        gameObject.SetActive(true);
        gameObject.transform.position = obstacleOriginLocation.position;
    }
}
