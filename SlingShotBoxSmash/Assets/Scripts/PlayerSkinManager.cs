using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        if (hasSkin1BeenPurchased && PlayerPrefs.GetInt("WearingSkin1") == 1)
        {
            
        }
        else if (hasSkin2BeenPurchased && PlayerPrefs.GetInt("WearingSkin2") == 1)
        {
            GameObject.Find("PhantomFlowerSkin").GetComponent<ParticleSystem>().Play();
            
        }
        else if (hasSkin3BeenPurchased && PlayerPrefs.GetInt("WearingSkin3") == 1)
        {
            GameObject.Find("PlayerSprite").GetComponent<Animator>().SetBool("IsRainbowSelected", true);
            GameObject.Find("RainbowSkin").GetComponent<ParticleSystem>().Play();
        }
        else if (hasSkin4BeenPurchased && PlayerPrefs.GetInt("WearingSkin4") == 1)
        {
            SpriteRenderer[] redBallSprites = GameObject.Find("RedBall").GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in redBallSprites)
            {
                sprite.enabled = true;
            }

            if (GreenOrbShield.hasGreenShieldBeenBought == true)
            {
                ParticleSystem.MainModule settings = GameObject.Find("GreenOrbShield").GetComponent<ParticleSystem>().main;
                settings.startColor = new ParticleSystem.MinMaxGradient(new Color32(2, 0, 22, 255));
            }

            GameObject.Find("RedBall").GetComponent<ParticleSystem>().Play();
            GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = new Color32(233, 22, 0, 255);
            GameObject.Find("Player").GetComponent<TrailRenderer>().startColor = new Color32(255, 30, 0, 255);
            GameObject.Find("Player").GetComponent<TrailRenderer>().endColor = new Color32(255, 30, 0, 255);
        }
        else if (hasSkin5BeenPurchased && PlayerPrefs.GetInt("WearingSkin5") == 1)
        {
            SpriteRenderer[] eyeBallSprites = GameObject.Find("EyeBallSkin").GetComponentsInChildren<SpriteRenderer>();

            foreach(SpriteRenderer sprite in eyeBallSprites)
            {
                sprite.enabled = true;
            }

        }
    }

}
