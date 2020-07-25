
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour

{

    public Text scoreText;
    public Text scoreMultiplierText;
    public static int score = 0;
    public static int scoreMultiplier = 1;


    private void Update()
    {
        scoreText.text = $"{score}";
        scoreMultiplierText.text = $"x{scoreMultiplier}";

        if(scoreText.fontSize != 80)
        {
            scoreText.fontSize--;
        }
        else if (scoreText.fontSize == 80)
        {
            scoreText.fontSize = 80;
        }

        if(scoreMultiplierText.fontSize != 55)
        {
            scoreMultiplierText.fontSize--;
        }else if(scoreMultiplierText.fontSize == 55)
        {
            scoreMultiplierText.fontSize = 55;
        }
        
       
    }

   
}
