using System;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{

    [SerializeField] private PointSystem pointSystem;
    [SerializeField] private UnityChanControlScript unityChan;

    [SerializeField] private float secondsToEnableJumping = 10;
    [SerializeField] private float secondsToEnableSliding = 20;
    [SerializeField] private float secondsToEnableAttack = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // enable jumping after certain time or distance
        if (pointSystem.secondsRunning > secondsToEnableJumping)
        {
            EnableAbility(unityChan.jumpParam);
        }
        if (pointSystem.secondsRunning > secondsToEnableSliding)
        {
            EnableAbility(unityChan.slideParam);
        }
        if (pointSystem.secondsRunning > secondsToEnableAttack)
        {
            EnableAbility(unityChan.attackParam);
        }
    }

    private void EnableAbility(string abilityToEnable)
    {        
        if (!unityChan.animationStates[abilityToEnable].IsEnabled)
        {
            unityChan.animationStates[abilityToEnable].IsEnabled = true;
        }        
    }
}
