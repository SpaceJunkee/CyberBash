using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera cmVirtualCam;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cmVirtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                //Time over
                CinemachineBasicMultiChannelPerlin cmMultiPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cmMultiPerlin.m_AmplitudeGain = 0f;
            }
        }
        
        
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmMultiPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmMultiPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
}
