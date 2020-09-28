
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public VolumeProfile myProfile;
    private ChromaticAberration ca;
    public AudioSource audio;
    public static int menuMusicPlaying = 0;
    public Animator transition;

    private void Start()
    {
        menuMusicPlaying++;

        if (menuMusicPlaying > 2 && audio!= null)
        {
            audio.Stop();
        }

    }
    public void PlayGame()
    {
        HandleAbilityTiles();

        HandleCustomizationTiles();

        StartCoroutine(LoadLevel());

        RestartScreenManager.hasPlayerClickedAd = false;
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FastGraphics()
    {
        
        if (myProfile.TryGet(out ca))
        {
            ca.active = false;      
        }

    }

    public void FancyGraphics()
    {

        if (myProfile.TryGet(out ca))
        {
            ca.active = true;
        }

    }

    public void SetShopCurrency()
    {
        GameObject.Find("CurrencyText").GetComponent<Text>().text = $"〄{PlayerPrefs.GetInt("NormalCurrency")}";
    }
    public void HandleAbilityTiles()
    {
        if (PlayerPrefs.GetInt("AbilityTile1") == 1)
        {
            ShieldWallBounce.isShieldActive = true;
        }

        if (PlayerPrefs.GetInt("AbilityTile2") == 1)
        {
            SpawnObjects.isMoneyBagUnlocked = true;
        }

        if (PlayerPrefs.GetInt("AbilityTile3") == 1)
        {
            ScoreDisplay.scoreMultiplier = 3;
            ScoreDisplay.scoreMultiplierIncreaser = 1;
        }

        /*if (PlayerPrefs.GetInt("AbilityTile4") == 1)
        {

        }

        if (PlayerPrefs.GetInt("AbilityTile5") == 1)
        {

        }*/

        if (PlayerPrefs.GetInt("AbilityTile6") == 1)
        {
            GreenOrbShield.hasGreenShieldBeenBought = true;
        }

        if (PlayerPrefs.GetInt("AbilityTile7") == 1)
        {
            LaunchPlayerProjectiles.hasPlayerProjectileBeenBought = true;
        }

        if (PlayerPrefs.GetInt("AbilityTile8") == 1)
        {
            SlingShot.hasBezerkBeenBought = true;
        }

       /* if (PlayerPrefs.GetInt("AbilityTile9") == 1)
        {

        }

        if (PlayerPrefs.GetInt("AbilityTile10") == 1)
        {

        }*/
    }

    public void HandleCustomizationTiles()
    {
        if (PlayerPrefs.GetInt("SkinTile1") == 1)
        {
            PlayerSkinManager.hasSkin1BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile2") == 1)
        {
            PlayerSkinManager.hasSkin2BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile3") == 1)
        {
            PlayerSkinManager.hasSkin3BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile4") == 1)
        {
            PlayerSkinManager.hasSkin4BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile5") == 1)
        {
            PlayerSkinManager.hasSkin5BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile6") == 1)
        {
            PlaylistManager.hasSong1BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile7") == 1)
        {
            PlaylistManager.hasSong2BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile8") == 1)
        {
            PlaylistManager.hasSong3BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile9") == 1)
        {
            PlaylistManager.hasSong4BeenPurchased = true;
        }

        if (PlayerPrefs.GetInt("SkinTile10") == 1)
        {
            PlaylistManager.hasSong5BeenPurchased = true;
        }
    }

}
