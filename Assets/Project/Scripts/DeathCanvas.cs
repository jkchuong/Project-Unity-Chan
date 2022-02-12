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
    private ObstacleManager obstacleManager;
    private CinematicManager cinematicManager;

    private void Start()
    {
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
        menuButton.onClick.AddListener(delegate { movingPlane.isMoving = true; });
        
        restartButton.onClick.AddListener(delegate { unityChan.isGameRunning = true; });
        restartButton.onClick.AddListener(unityChan.SetRunningStateTrue);
        restartButton.onClick.AddListener(delegate { movingPlane.isMoving = true; });
        restartButton.onClick.AddListener(obstacleManager.BeginObstacleSpawning);
        restartButton.onClick.AddListener(cinematicManager.PlayStartSequence);
    }

    private void DisplayDeathButtons()
    {
        menuButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        unityChan.OnDeath -= DisplayDeathButtons;
        menuButton.onClick.RemoveListener(delegate { unityChan.isGameRunning = false; });
        menuButton.onClick.RemoveListener(unityChan.SetRunningStateTrue);
        restartButton.onClick.RemoveListener(delegate { unityChan.isGameRunning = true; });
        restartButton.onClick.RemoveListener(unityChan.SetRunningStateTrue);
    }
}
