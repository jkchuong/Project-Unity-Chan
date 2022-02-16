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

    public void PlayStartSequence()
    {
        GetComponent<PlayableDirector>().Play();
        playCamera.Priority = 10;
        startingCamera.Priority = -10;
    }
}