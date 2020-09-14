
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
        StartCoroutine(LoadLevel());       
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

}
