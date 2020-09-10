using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int firstTierPrice = 1000;
    public int secondTierPrice = 5000;
    public int thirdTierPrice = 10000;
    public void BuyFirstTierUpgrade()
    {
        if(PlayerPrefs.GetInt("NormalCurrency") > firstTierPrice)
        {
            //Apply upgrade and remove money
        }
    }

    public void BuySecondTierUpgrade()
    {
        if (PlayerPrefs.GetInt("NormalCurrency") > secondTierPrice)
        {
            //Apply upgrade and remove money
        }
    }

    public void BuyThirdTierUpgrade()
    {
        if (PlayerPrefs.GetInt("NormalCurrency") > thirdTierPrice)
        {
            //Apply upgrade and remove money
        }
    }
}
