using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera startingCamera;
    [SerializeField] private CinemachineVirtualCamera playCamera;
    
    private Button startButton;
    
    private void Start()
    {
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
        startButton.onClick.AddListener(PlayStartSequence);
    }

    private void PlayStartSequence()
    {
        GetComponent<PlayableDirector>().Play();
        playCamera.Priority = 10;
        startingCamera.Priority = -10;
    }
}