using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityChan;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CinematicEndManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera startingCamera;
    [SerializeField] private CinemachineVirtualCamera playCamera;

    private UnityChanControlScript unityChan;
    private void Start()
    {
        unityChan = FindObjectOfType<UnityChanControlScript>();
        unityChan.OnDeath += PlayEndSequence;
    }

    private void PlayEndSequence()
    {
        GetComponent<PlayableDirector>().Play();
        playCamera.Priority = -10;
        startingCamera.Priority = 10;
    }
}
