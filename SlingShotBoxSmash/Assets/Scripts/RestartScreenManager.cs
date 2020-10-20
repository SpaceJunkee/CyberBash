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

    public void Awake()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {

            AdsManager.hasRemoveAdsBeenBought = true;
            GameObject adButton = GameObject.Find("AdButton");

            GameObject.Find("MoneyEarnedText").GetComponent<Text>().fontSize = 36;
            GameObject.Find("MoneyEarnedText").GetComponent<Text>().color = new Color32(0, 255, 133, 255);

            adButton.GetComponent<Image>().enabled = false;
            adButton.GetComponent<Button>().interactable = false;
            adButton.GetComponentInChildren<Text>().enabled = false;
            GameObject.Find("WatchAdText").GetComponent<Text>().enabled = false;

            GameObject removeAdsButton = GameObject.Find("RemoveAds");
            removeAdsButton.GetComponent<Image>().enabled = false;
            removeAdsButton.GetComponent<Button>().interactable = false;
            removeAdsButton.GetComponentInChildren<Text>().enabled = false;
        }
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
            if (PlayerPrefs.GetInt("RemoveAds") == 1)
            {
                GameObject.Find("MoneyEarnedText").GetComponent<Text>().text = "〄" + moneyEarnedBaseScore * 2 + "";
            }
            else
            {
                GameObject.Find("MoneyEarnedText").GetComponent<Text>().text = "〄" + moneyEarnedBaseScore + "";
            }
                
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
