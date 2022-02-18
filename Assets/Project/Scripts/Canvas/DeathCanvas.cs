using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Project.Scripts.Obstacles;
using TMPro;
using UnityChan;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button menuButton, restartButton;
    [SerializeField] private ScoresScriptableObject scoresScriptableObject;
    
    private UnityChanControlScript unityChan;
    private ScrollingBackground movingPlane;
    private RollingDonut[] rollingDonuts;
    private ObstacleManager obstacleManager;
    private CinematicManager cinematicManager;
    private EnvironmentSpawner environmentSpawner;

    private void Start()
    {
        rollingDonuts = FindObjectsOfType<RollingDonut>();
        environmentSpawner = FindObjectOfType<EnvironmentSpawner>();
        
        unityChan = FindObjectOfType<UnityChanControlScript>();
        movingPlane = FindObjectOfType<ScrollingBackground>();
        obstacleManager = FindObjectOfType<ObstacleManager>();
        cinematicManager = FindObjectOfType<CinematicManager>();

        if (unityChan)
        {
            unityChan.OnDeath += DisplayDeathCanvas;
        }
        
        menuButton.onClick.AddListener(delegate { unityChan.isGameRunning = false; });
        menuButton.onClick.AddListener(unityChan.SetRunningStateTrue);
        menuButton.onClick.AddListener(StartRollingAndPlane);
        
        restartButton.onClick.AddListener(delegate { unityChan.isGameRunning = true; });
        restartButton.onClick.AddListener(unityChan.SetRunningStateTrue);
        restartButton.onClick.AddListener(StartRollingAndPlane);
        restartButton.onClick.AddListener(obstacleManager.BeginObstacleSpawning);
        restartButton.onClick.AddListener(cinematicManager.PlayStartSequence);
        
        menuButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    private void DisplayDeathCanvas()
    {
        scoreText.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        scoreText.text = "Score: " + scoresScriptableObject.playerScore.LastOrDefault();
    }

    private void StartRollingAndPlane()
    {
        foreach (RollingDonut donut in rollingDonuts)
        {
            donut.isMoving = true;
        }

        movingPlane.isMoving = true;
        environmentSpawner.isMoving = true;
    }
    
    private void OnDisable()
    {
        unityChan.OnDeath -= DisplayDeathCanvas;
        
        menuButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }
}
