using System;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] private Button startButton, scoreButton, creditsButton;

    private CinematicManager cinematicManager;
    private UnityChanControlScript unityChan;

    private void Start()
    {
        cinematicManager = FindObjectOfType<CinematicManager>();
        unityChan = FindObjectOfType<UnityChanControlScript>();
        
        startButton.onClick.AddListener(delegate { unityChan.isGameRunning = true; });
        startButton.onClick.AddListener(unityChan.SetRunningStateTrue);
        startButton.onClick.AddListener(cinematicManager.PlayStartSequence);
        startButton.onClick.AddListener(HideStartButtons);
        
        scoreButton.onClick.AddListener(HideStartButtons);
        
        creditsButton.onClick.AddListener(HideStartButtons);
    }

    public void DisplayStartButtons()
    {
        startButton.gameObject.SetActive(true);
        scoreButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
    }

    private void HideStartButtons()
    {
        startButton.gameObject.SetActive(false);
        scoreButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(delegate { unityChan.isGameRunning = true; });
        startButton.onClick.RemoveListener(unityChan.SetRunningStateTrue);
        startButton.onClick.RemoveListener(cinematicManager.PlayStartSequence);
    }
}
