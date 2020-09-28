using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class BerzerMeter : MonoBehaviour
{

    public Slider slider;
    public static float currentValue;
    public void SetMaxFill(int fill)
    {
        slider.maxValue = fill;
        slider.value = 0;
    }
   
    public void IncreaseFill(int fill)
    {
        slider.value += fill;
        currentValue = slider.value;
    }

    public void SetFill(int fill)
    {
        slider.value = fill;
        currentValue = fill;
    }
}
