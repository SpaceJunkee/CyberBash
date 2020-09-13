using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public bool hasBeenBought = false;
    string prefUpgrade = "";

    private void Start()
    {
        if(PlayerPrefs.GetInt("AbilityTile1") == 1)
        {
            DisableTile("AbilityTile1");
        }

        if(PlayerPrefs.GetInt("AbilityTile2") == 1)
        {
            DisableTile("AbilityTile2");
        }

        if (PlayerPrefs.GetInt("AbilityTile3") == 1)
        {
            DisableTile("AbilityTile3");
        }

        if (PlayerPrefs.GetInt("AbilityTile4") == 1)
        {
            DisableTile("AbilityTile4");
        }

        if (PlayerPrefs.GetInt("AbilityTile5") == 1)
        {
            DisableTile("AbilityTile5");
        }
    }

    public void BuyUpgrade(int price)
    {
        if(PlayerPrefs.GetInt("NormalCurrency") >= price && hasBeenBought != true)
        {
            hasBeenBought = true;
            PlayerPrefs.SetInt("NormalCurrency", PlayerPrefs.GetInt("NormalCurrency") - price);
            GameObject.Find("CurrencyText").GetComponent<Text>().text = $"〄{PlayerPrefs.GetInt("NormalCurrency")}";
            SaveBuyingStatus(prefUpgrade);
        }
    }

    public void SaveBuyingStatus(string prefUpgrade)
    {
        if (hasBeenBought)
        {
            GameObject.Find(prefUpgrade).GetComponent<Image>().enabled = false;
            GameObject.Find(prefUpgrade).GetComponentInChildren<Text>().enabled = false;
            GameObject.Find(prefUpgrade).GetComponent<Button>().enabled = false;
            PlayerPrefs.SetInt(prefUpgrade, 1);
        }
        
        hasBeenBought = false;
    }

    public void setPrefUpgradeString(string prefUpgradeParam)
    {
        prefUpgrade = prefUpgradeParam;
    }

    public void DisableTile(string tileName)
    {
        GameObject.Find(tileName).GetComponent<Image>().enabled = false;
        GameObject.Find(tileName).GetComponentInChildren<Text>().enabled = false;
        GameObject.Find(tileName).GetComponent<Button>().enabled = false;
    }

}
