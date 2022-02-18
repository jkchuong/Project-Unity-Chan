using System;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class RollingDonut : MonoBehaviour
{
    [SerializeField] private float rollSpeed = 0.5f;
    public bool isMoving = true;
    
    private UnityChanControlScript unityChan;

    private void Start()
    {
        unityChan = FindObjectOfType<UnityChanControlScript>();

        if (unityChan)
        {
            unityChan.OnDeath += delegate { isMoving = false; };
        }
    }

    private void Update()
    {
        if (!isMoving) return;
        transform.Rotate(0, 0, rollSpeed * Time.deltaTime);
    }
}
