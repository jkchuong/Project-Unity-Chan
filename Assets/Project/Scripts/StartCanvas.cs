using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        Button[] buttons = { startButton, scoreButton, creditsButton };

        foreach (Button button in buttons)
        {
            button.transform.DOScale(Vector3.one, 0.3f)
                .SetEase(Ease.InBounce);
        }
    }

    private void HideStartButtons()
    {
        Button[] buttons = { startButton, scoreButton, creditsButton };

        foreach (Button button in buttons)
        {
            button.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBounce);
        }
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(delegate { unityChan.isGameRunning = true; });
        startButton.onClick.RemoveListener(unityChan.SetRunningStateTrue);
        startButton.onClick.RemoveListener(cinematicManager.PlayStartSequence);
    }
}
