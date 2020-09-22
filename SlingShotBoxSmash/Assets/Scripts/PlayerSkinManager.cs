using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public static bool hasSkin1BeenPurchased = false;
    public static bool hasSkin2BeenPurchased = false;
    public static bool hasSkin3BeenPurchased = false;
    public static bool hasSkin4BeenPurchased = false;
    public static bool hasSkin5BeenPurchased = false;

    // Start is called before the first frame update
    void Start()
    {
        if (hasSkin1BeenPurchased)
        {
            //Wear skin 1
            SpriteRenderer[] eyeBallSprites = GameObject.Find("EyeBallSkin").GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in eyeBallSprites)
            {
                sprite.enabled = true;
            }
        }
        else if (hasSkin2BeenPurchased)
        {
            //Wear skin 2
        }
        else if (hasSkin3BeenPurchased)
        {

        }
        else if (hasSkin4BeenPurchased)
        {

        }
        else if (hasSkin5BeenPurchased)
        {
            SpriteRenderer[] eyeBallSprites = GameObject.Find("EyeBallSkin").GetComponentsInChildren<SpriteRenderer>();

            foreach(SpriteRenderer sprite in eyeBallSprites)
            {
                sprite.enabled = true;
            }

        }
    }

}
