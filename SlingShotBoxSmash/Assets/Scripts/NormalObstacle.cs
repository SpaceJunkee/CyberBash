using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObstacle : MonoBehaviour
{
    public GameObject normalObjectDeathEffect;

    public ScoreDisplay scoreTextPop;
    public GameObject floatingTextPrefab;
    public AudioSource boxBreakAudio;
    public AudioClip normalBoxBreakClip;
    public GameObject spawnConfiner;
    

    private void Start()
    {
        spawnConfiner = GameObject.FindGameObjectWithTag("Confiner");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (ScoreDisplay.score >= ScoreDisplay.multiplierGoal)
        {
            ScoreDisplay.scoreMultiplier++;
            scoreTextPop.scoreMultiplierText.fontSize = 70;
            ScoreDisplay.multiplierGoal *= 3;
        }

        if (collision.gameObject.tag.Equals("Player"))
        {
            boxBreakAudio.Play();
            ScoreDisplay.score += 5 * ScoreDisplay.scoreMultiplier;
            ShowFloatingText(5 * ScoreDisplay.scoreMultiplier, new Color32(0, 29, 26, 255));

            scoreTextPop.scoreText.fontSize = 100;

            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * 125));
            Die();
        }
    }

    private void Die()
    {
        DisableObject();
        spawnConfiner.GetComponent<SpawnObjects>().SpawnNormalObstacles(1);
        Destroy(gameObject, normalBoxBreakClip.length);    
        Instantiate(normalObjectDeathEffect, transform.position, Quaternion.identity);
    }

    private void ShowFloatingText(int hitScore, Color32 color)
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text = hitScore.ToString();
        go.GetComponent<TextMesh>().color = color;
    }

    private void DisableObject()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
            sr.enabled = false;

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}

