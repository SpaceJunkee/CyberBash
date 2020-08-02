using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxObstacle : MonoBehaviour
{
    public GameObject deathObstacleDeathEffect;

    public AudioSource deathBoxAudio;
    public AudioClip deathBoxAudioClip;
    public GameObject spawnConfiner;
    public GameObject playerDeath;

    private void Start()
    {
        playerDeath = GameObject.FindGameObjectWithTag("Lava");
        spawnConfiner = GameObject.FindGameObjectWithTag("Confiner");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player"))
        {
           
            if (SlingShot.isHeldDown == false)
            { 
                deathBoxAudio.Play();

                CameraShake.Instance.ShakeCamera(13f, 0.2f);
                Destroy(collision.gameObject);
                Die();
            }
            else if (SlingShot.isHeldDown)
            {
                CameraShake.Instance.ShakeCamera(13f, 0.2f);
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 30, 0, 255);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0,255,226,255);
        }
    }


    private void Die()
    {
        DisableObject();
        spawnConfiner.GetComponent<SpawnObjects>().SpawnNormalObstacles(1);
        playerDeath.GetComponent<DeathRestart>().DestroyPlayer();
        Destroy(gameObject, deathBoxAudioClip.length);
        Instantiate(deathObstacleDeathEffect, transform.position, Quaternion.identity);
    }

    private void DisableObject()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
            sr.enabled = false;

        this.gameObject.GetComponent<ParticleSystem>().Stop();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
