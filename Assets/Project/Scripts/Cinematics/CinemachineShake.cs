using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private float maxShakeIntensity, shakeDuration;

    private float startingShakeIntensity;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    public void ShakeCamera()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = maxShakeIntensity;
        startingShakeIntensity = maxShakeIntensity;
        shakeTimer = shakeDuration;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingShakeIntensity, 0f, 1 -(shakeTimer / shakeDuration));
            }
        }
    }
}
