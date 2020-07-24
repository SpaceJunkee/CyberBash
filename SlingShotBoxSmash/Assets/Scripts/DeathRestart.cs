using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DeathRestart : MonoBehaviour
{

    public GameObject deathEffect;
    public TimeManager timeManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
            
        if (collision.gameObject.tag.Equals("Player") && SlingShot.isHeldDown == false)
        {
            Destroy(collision.gameObject);
            CameraShake.Instance.ShakeCamera(25f, 0.75f);
            timeManager.StartSlowMotion(0.3f);
            Instantiate(deathEffect, collision.gameObject.transform.position, Quaternion.identity);
            Invoke("RestartGame", 0.5f);
        }
    }

    private void RestartGame()
    {
        ResetScores();
        timeManager.StopSlowMotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetScores()
    {
        ScoreDisplay.score = 0;
        ScoreDisplay.scoreMultiplier = 1;
    }
}
