using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Project.Scripts.Obstacles;
using UnityChan;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private Button menuButton, restartButton;

    private UnityChanControlScript unityChan;
    private ScrollingBackground movingPlane;
    private RollingDonut[] rollingDonuts;
    private ObstacleManager obstacleManager;
    private CinematicManager cinematicManager;

    private void Start()
    {
        rollingDonuts = FindObjectsOfType<RollingDonut>();
        
        unityChan = FindObjectOfType<UnityChanControlScript>();
        movingPlane = FindObjectOfType<ScrollingBackground>();
        obstacleManager = FindObjectOfType<ObstacleManager>();
        cinematicManager = FindObjectOfType<CinematicManager>();

        if (unityChan)
        {
            unityChan.OnDeath += DisplayDeathButtons;
        }
        
        menuButton.onClick.AddListener(delegate { unityChan.isGameRunning = false; });
        menuButton.onClick.AddListener(unityChan.SetRunningStateTrue);
        menuButton.onClick.AddListener(StartRollingAndPlane);
        
        restartButton.onClick.AddListener(delegate { unityChan.isGameRunning = true; });
        restartButton.onClick.AddListener(unityChan.SetRunningStateTrue);
        restartButton.onClick.AddListener(StartRollingAndPlane);
        restartButton.onClick.AddListener(obstacleManager.BeginObstacleSpawning);
        restartButton.onClick.AddListener(cinematicManager.PlayStartSequence);
    }

    private void DisplayDeathButtons()
    {
        menuButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void StartRollingAndPlane()
    {
        foreach (RollingDonut donut in rollingDonuts)
        {
            donut.isMoving = true;
        }

        movingPlane.isMoving = true;
    }
    
    private void OnDisable()
    {
        unityChan.OnDeath -= DisplayDeathButtons;
        
        menuButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }
}
