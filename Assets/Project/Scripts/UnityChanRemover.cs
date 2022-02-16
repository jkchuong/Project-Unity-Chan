using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Project.Scripts.Obstacles;
using UnityChan;
using UnityEngine;

public class UnityChanRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnityChanControlScript unityChan = other.GetComponent<UnityChanControlScript>();
        
        if (unityChan)
        {
            unityChan.DeathControls();
        }
    }
}
