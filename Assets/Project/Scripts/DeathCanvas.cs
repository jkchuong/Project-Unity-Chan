using System;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private Button menuButton, restartButton;

    private UnityChanControlScript unityChan;

    private void Start()
    {
        unityChan = FindObjectOfType<UnityChanControlScript>();

        if (unityChan)
        {
            unityChan.OnDeath += DisplayDeathButtons;
        }
        
        menuButton.onClick.AddListener(delegate { unityChan.isGameRunning = false; });
        restartButton.onClick.AddListener(delegate { unityChan.isGameRunning = true; });
    }

    private void DisplayDeathButtons()
    {
        menuButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        unityChan.OnDeath -= DisplayDeathButtons;
    }
}
