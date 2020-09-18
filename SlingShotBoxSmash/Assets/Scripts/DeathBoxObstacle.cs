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

        if (collision.gameObject.tag.Equals("Bomb"))
        {
            Destroy(gameObject);
            ScoreDisplay.score += 10;
        }

        if (collision.gameObject.tag.Equals("Player") )
        {
            if (SlingShot.isInBerzerkMode)
            {
                CameraShake.Instance.ShakeCamera(13f, 0.2f);
                Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
                var force = transform.position - collision.transform.position;
                force.Normalize();
              
                collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (125 * 0.15f)));
                            
                Die();
            }
            else
            {
                if (SlingShot.isHeldDown == false)
                {
                    deathBoxAudio.Play();

                    Destroy(collision.gameObject);
                    Die();
                }
                else if (SlingShot.isHeldDown && !SlingShot.isInBerzerkMode)
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 30, 0, 255);
                }
            }
                     
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !SlingShot.isInBerzerkMode)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0,255,226,255);
        }
    }


    private void Die()
    {
        DisableObject();
        spawnConfiner.GetComponent<SpawnObjects>().SpawnNormalObstacles(1);

        if (!SlingShot.isInBerzerkMode)
        {
            playerDeath.GetComponent<DeathRestart>().DestroyPlayer();
        }
        
        GameObject newDeathEffect = (GameObject)Instantiate(deathObstacleDeathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 2);
        Destroy(gameObject, deathBoxAudioClip.length);
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
