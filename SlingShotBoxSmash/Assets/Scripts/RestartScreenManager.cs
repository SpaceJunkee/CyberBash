using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScreenManager : MonoBehaviour
{

    public float moneyEarnedBaseScore = 0;
    public float delay = 0.005f;
    public int moneyBaseMultiplier = 2;
    public AdsManager adManager;
    public static bool playerRewarded = false;
    public static bool hasPlayerClickedAd = false;

    private void Start()
    {
        StartCoroutine(CountUpToTarget());
        GameObject.Find("HighScore").GetComponent<Text>().text = $"High Score\n{PlayerPrefs.GetInt("HighScore")}";
    }

    IEnumerator CountUpToTarget()
    {
        while (moneyEarnedBaseScore < PlayerPrefs.GetInt("MoneyEarned") && hasPlayerClickedAd == false)
        {
            if(moneyEarnedBaseScore > 25 && moneyEarnedBaseScore < 500)
            {
                delay = 0.005f;
            }
            else if(moneyEarnedBaseScore > 500)
            {
                moneyBaseMultiplier += 1;
            }

            moneyEarnedBaseScore += moneyBaseMultiplier; 
            moneyEarnedBaseScore = Mathf.Clamp(moneyEarnedBaseScore, 0f, PlayerPrefs.GetInt("MoneyEarned"));
            GameObject.Find("MoneyEarnedText").GetComponent<Text>().text = "〄" + moneyEarnedBaseScore + "";
            yield return new WaitForSeconds(delay);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        playerRewarded = false;
        hasPlayerClickedAd = false;
    }

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void ShowRewardAd()
    {
        adManager.ShowRewardAd();
        
    }

    public void DisableAdButton()
    {
        

    }

}
