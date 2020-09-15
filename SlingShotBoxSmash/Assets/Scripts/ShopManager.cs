using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public bool hasBeenBought = false;
    string prefUpgrade = "";

    private void Start()
    {
        PlayerPrefs.SetInt("NormalCurrency", 999999);

        if(this.gameObject.name == "ShopMenuDisplay1")
        {
            HandleAbilityTiles();         
        }

        if (this.gameObject.name == "ShopMenuDisplay2")
        {
            HandleCustomiseTiles();
        }

    }

    public void BuyUpgrade(int price)
    {
        if (PlayerPrefs.GetInt("NormalCurrency") >= price && hasBeenBought != true)
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

    public void HandleAbilityTiles()
    {
        if (PlayerPrefs.GetInt("AbilityTile1") == 1)
        {
            ShieldWallBounce.isShieldActive = true;
            DisableTile("AbilityTile1");
        }

        if (PlayerPrefs.GetInt("AbilityTile2") == 1)
        {
            SpawnObjects.isMoneyBagUnlocked = true;
            DisableTile("AbilityTile2");
        }

        if (PlayerPrefs.GetInt("AbilityTile3") == 1)
        {
            ScoreDisplay.scoreMultiplier = 4;
            ScoreDisplay.scoreMultiplierIncreaser = 1;
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

        if (PlayerPrefs.GetInt("AbilityTile6") == 1)
        {
            DisableTile("AbilityTile6");
        }

        if (PlayerPrefs.GetInt("AbilityTile7") == 1)
        {
            DisableTile("AbilityTile7");
        }

        if (PlayerPrefs.GetInt("AbilityTile8") == 1)
        {
            DisableTile("AbilityTile8");
        }

        if (PlayerPrefs.GetInt("AbilityTile9") == 1)
        {
            DisableTile("AbilityTile9");
        }

        if (PlayerPrefs.GetInt("AbilityTile10") == 1)
        {
            DisableTile("AbilityTile10");
        }
    }

    public void HandleCustomiseTiles()
    {
        if (PlayerPrefs.GetInt("SkinTile1") == 1)
        {
            DisableTile("SkinTile1");
        }

        if (PlayerPrefs.GetInt("SkinTile2") == 1)
        {
            DisableTile("SkinTile2");
        }

        if (PlayerPrefs.GetInt("SkinTile3") == 1)
        {
            DisableTile("SkinTile3");
        }

        if (PlayerPrefs.GetInt("SkinTile4") == 1)
        {
            DisableTile("SkinTile4");
        }

        if (PlayerPrefs.GetInt("SkinTile5") == 1)
        {
            DisableTile("SkinTile5");
        }

        if (PlayerPrefs.GetInt("SkinTile6") == 1)
        {
            DisableTile("SkinTile6");
        }

        if (PlayerPrefs.GetInt("SkinTile7") == 1)
        {
            DisableTile("SkinTile7");
        }

        if (PlayerPrefs.GetInt("SkinTile8") == 1)
        {
            DisableTile("SkinTile8");
        }

        if (PlayerPrefs.GetInt("SkinTile9") == 1)
        {
            DisableTile("SkinTile9");
        }

        if (PlayerPrefs.GetInt("SkinTile10") == 1)
        {
            DisableTile("SkinTile10");
        }
    }


}
