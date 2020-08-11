using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    public static int normalScoreValue = 5;
    public static int doubleScoreValue = 10;
    public static int greenGuyScore = 15;
    public static int hitCount = 0;



    public static void ResetValues()
    {
        normalScoreValue = 5;
        doubleScoreValue = 10;
        hitCount = 0;
    }
}
