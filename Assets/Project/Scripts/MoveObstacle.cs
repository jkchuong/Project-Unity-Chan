using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    #region Components
    private Rigidbody boxComponent;
    #endregion

    #region Fields
    private Vector3 velocity;
    private Vector3 direction;
    public float objectSpeed = 8.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxComponent = GetComponent<Rigidbody>();

        // default
        direction = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {        
        MoveObject(direction, objectSpeed);
    }

    //Move object at specific direction and speed
    private void MoveObject(Vector3 direction_, float speed)
    {
        velocity = direction_;
        velocity = transform.TransformDirection(velocity);
        velocity *= speed;

        // move object
        transform.localPosition += velocity * Time.deltaTime;
    }
}
