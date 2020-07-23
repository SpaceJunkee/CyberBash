using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;

    public VolumeProfile myProfile;

    public void StartSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        if (myProfile)
        {
            ChromaticAberration ca;

            if(myProfile.TryGet(out ca))
            {
                ca.intensity.SetValue(new NoInterpMinFloatParameter(0.5f, 0, true));
            }
        }
    }

    public void StopSlowMotion()
    {
        Time.timeScale = 1f;

        if (myProfile)
        {
            ChromaticAberration ca;

            if (myProfile.TryGet(out ca))
            {
                ca.intensity.SetValue(new NoInterpMinFloatParameter(0f, 0, true));
            }
        }
    }
}
