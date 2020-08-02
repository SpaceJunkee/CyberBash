
using UnityEngine;

using UnityEngine.SceneManagement;


public class DeathRestart : MonoBehaviour
{

    public GameObject deathEffect;
    public TimeManager timeManager;
    public GameObject playerBig;
    GameObject[] musicObject;
    public AudioSource audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
        if (collision.gameObject.tag.Equals("Player") && SlingShot.isHeldDown == false)
        {
            Destroy(collision.gameObject);
            DestroyPlayer();    
            
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        
    }

    public void DestroyPlayer()
    { 
        Destroy(playerBig);
        Instantiate(deathEffect, playerBig.gameObject.transform.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(25f, 0.75f);
        timeManager.StartSlowMotion(0.3f);
        Invoke("RestartGame", 0.5f);
    }


    void Start()
    {
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
        
        ResetScores();
        timeManager.StopSlowMotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetScores()
    {
        ScoreDisplay.score = 0;
        ScoreDisplay.scoreMultiplier = 1;
        ScoreDisplay.multiplierGoal = 50;
    }
}
