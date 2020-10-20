using UnityEngine;

public class RemoveAdsButton : MonoBehaviour
{
    public enum PurchaseType { removeAds };

    public PurchaseType purchaseType;

    public void ClickPurchaseButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                IAPManager.instance.BuyRemoveAds();
                break;
        }
    }
}
