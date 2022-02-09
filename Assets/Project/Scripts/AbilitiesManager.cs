using System;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{

    [SerializeField] private PointSystem pointSystem;
    [SerializeField] private UnityChanControlScript unityChan;

    private string jumpParam = "Jump";
    private string slideParam = "Slide";
    private string attackParam = "Attack";
    private string deathParam = "Collision";

    // Start is called before the first frame update
    void Start()
    {
        //pointSystem = GetComponent<PointSystem>();
        //unityChan = GetComponent<UnityChanControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointSystem.secondsRunning > 3.0f)
        {
            //unityChan.animationStates[jumpParam].IsEnabled = true; ;
            
        }
    }
}
