using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string placement = "rewardedVideo";
    int moneyEarned;
    int normalCurrency;

    IEnumerator Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3815483", true);

        while (!Advertisement.IsReady(placement))
        {
            yield return null;
        }

        GameObject.Find("AdButton").SetActive(true);


    }

    public void ShowRewardAd()
    {
        moneyEarned = PlayerPrefs.GetInt("MoneyEarned");
        normalCurrency = PlayerPrefs.GetInt("NormalCurrency");
        Advertisement.Show(placement);
        RestartScreenManager.hasPlayerClickedAd = true;
    }

   
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {          
            GameObject.Find("MoneyEarnedText").GetComponent<Text>().text = "〄" + PlayerPrefs.GetInt("MoneyEarned") * 2 + "";
            PlayerPrefs.SetInt("NormalCurrency", normalCurrency + moneyEarned);
            GameObject.Find("AdButton").GetComponent<Button>().enabled = false;
            GameObject.Find("AdButton").GetComponent<Image>().enabled = false;
            GameObject.Find("AdButton").GetComponentInChildren<Text>().enabled = false;
            GameObject.Find("WatchAdText").GetComponent<Text>().enabled = false;
            GameObject.Find("MoneyEarnedText").GetComponent<Text>().fontSize = 38;
            GameObject.Find("MoneyEarnedText").GetComponent<Text>().color = new Color32(0,255,133,255);
        }
        else if (showResult == ShowResult.Failed)
        {
            // :(
        }
        
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {

    }
}
