
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
    private void Start()
    {
        menuMusicPlaying++;

        if (menuMusicPlaying > 2)
        {
            audio.Stop();
        }

    }
    public void PlayGame()
    {
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
        GameObject.Find("CurrencyText").GetComponent<Text>().text = $"${PlayerPrefs.GetInt("NormalCurrency")}";
    }

}
