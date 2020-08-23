
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour

{

    public Text scoreText;
    public Text scoreMultiplierText;
    public static int score = 0;
    public static int scoreMultiplier = 1;
    public static int multiplierGoal = 50;

    private void Update()
    {
        scoreText.text = $"{score}";
        scoreMultiplierText.text = $"x{scoreMultiplier}";

        if(scoreText.fontSize != 20)
        {
            scoreText.fontSize--;
        }
        else if (scoreText.fontSize == 20)
        {
            scoreText.fontSize = 20;
        }

        if(scoreMultiplierText.fontSize != 15)
        {
            scoreMultiplierText.fontSize--;
        }else if(scoreMultiplierText.fontSize == 15)
        {
            scoreMultiplierText.fontSize = 15;
        }
 
    }

   
}
