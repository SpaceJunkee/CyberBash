using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour

{

    public Text scoreText;
    public Text scoreMultiplierText;
    public static int score = 0;
    public static int scoreMultiplier = 1;
    private bool isMultiplied = false;


    private void Update()
    {
        scoreText.text = $"{score}";
        scoreMultiplierText.text = $"x{scoreMultiplier}";

        if(scoreText.fontSize != 50)
        {
            scoreText.fontSize--;
        }
        else if (scoreText.fontSize == 50)
        {
            scoreText.fontSize = 50;
        }

        if(scoreMultiplierText.fontSize != 33)
        {
            scoreMultiplierText.fontSize--;
        }else if(scoreMultiplierText.fontSize == 33)
        {
            scoreMultiplierText.fontSize = 33;
        }
        
       
    }

   
}
