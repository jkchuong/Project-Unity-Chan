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

    // Update is called once per frame
    private void Update()
    {
        // enable jumping after certain time or distance
        if (pointSystem.secondsRunning > secondsToEnableJumping)
        {
            EnableAbility(unityChan.JUMP_PARAM);
        }
        if (pointSystem.secondsRunning > secondsToEnableSliding)
        {
            EnableAbility(unityChan.SLIDE_PARAM);
        }
        if (pointSystem.secondsRunning > secondsToEnableAttack)
        {
            EnableAbility(unityChan.ATTACK_PARAM);
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
