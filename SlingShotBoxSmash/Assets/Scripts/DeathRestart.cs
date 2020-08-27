
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathRestart : MonoBehaviour
{

    public GameObject deathEffect;
    public TimeManager timeManager;
    public GameObject playerBig;
    GameObject[] musicObject;
    public AudioSource audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag.Equals("Lava") && SlingShot.isHeldDown)
        {

        }
        else
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                GameObject.Find("PlayerDeath").GetComponent<AudioSource>().Play();              
                Destroy(collision.gameObject);
                DestroyPlayer();
                SlingShot.isHeldDown = false;
            }
        }                     
        
    }

    public void DestroyPlayer()
    {
        SlingShot.isDead = true;
        playerBig.GetComponent<TrailRenderer>().enabled = false;
        Instantiate(deathEffect, playerBig.gameObject.transform.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(25f, 0.75f);
        timeManager.StartSlowMotion(0.3f);
        Invoke("RestartGame", 0.5f);
    }


    void Start()
    {
        playerBig = GameObject.FindGameObjectWithTag("PlayerBig");
        musicObject = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObject.Length == 1)
        {
            audio.Play();
        }
        else
        {
            for (int i = 1; i < musicObject.Length; i++)
            {
                Destroy(musicObject[i]);
            }

        }
    }

    void Awake()
    {
        DontDestroyOnLoad(audio);
    }

    public void RestartGame()
    {
        GameObject music = GameObject.Find("Music");
        SlingShot.isDead = false;
        playerBig.GetComponent<TrailRenderer>().enabled = true;

        if (music.GetComponent<AudioSource>().isPlaying == false && music.GetComponent<AudioSource>()!= null)
        {
            music.GetComponent<AudioSource>().Play();
        }

        ResetScores();
        timeManager.StopSlowMotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetScores()
    {
        SpawnObjects.hasFirstBombGoneOff = false;
        ScoreDisplay.score = 0;
        ScoreDisplay.scoreMultiplier = 1;
        ScoreDisplay.multiplierGoal = 50;
        Destroy(GameObject.FindGameObjectWithTag("Projectile"));

        ComboHandler.ResetValues();
    }
}
