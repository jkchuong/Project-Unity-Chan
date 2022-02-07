using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Project.Scripts.Obstacles;
using UnityEngine;

public class ObstacleRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();

        if (obstacle)
        {
            obstacle.gameObject.SetActive(false);
        }
    }
}
