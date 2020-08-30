
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public VolumeProfile myProfile;
    private ChromaticAberration ca;

    public void PlayGame()
    {
        Invoke("LoadLevel", 0.5f);
    }

    private void LoadLevel()
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

}
