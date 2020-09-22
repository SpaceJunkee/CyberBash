using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBags : MonoBehaviour
{
    public GameObject moneyBagsDeathEffect;

    public ScoreDisplay scoreTextPop;
    public GameObject floatingTextPrefab;
    public AudioSource boxBreakAudio;
    public AudioClip moneyBagsBoxClip;
    public AudioClip combo1SoundNormal;
    public AudioClip combo2SoundNormal;
    public AudioClip combo3SoundNormal;
    public AudioClip combo4SoundNormal;
    public AudioClip combo5SoundNormal;
    public GameObject spawnConfiner;
    public TimeManager timeManager;
    public static float comboSlowMo = 1;

    public static int moneyBagsValue = 25;


    private void Start()
    {
        spawnConfiner = GameObject.FindGameObjectWithTag("Confiner");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    
        if (collision.gameObject.tag.Equals("Bomb"))
        {
            Destroy(gameObject);
            ScoreDisplay.moneyBagsDrops += 10;
        }

        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!SlingShot.isHeldDown)
            {
                ComboHandler.hitCount++;
            }
            else
            {
                if (SlingShot.isHeldDown)
                {
                    ComboHandler.hitCount = 1;
                }
            }

            ScoreDisplay.moneyBagsDrops += 25;

            boxBreakAudio.Play();
           
            ShowFloatingText(moneyBagsValue, new Color32(9, 0, 7, 255));

            scoreTextPop.scoreText.fontSize = 100;

            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            if (SlingShot.isInBerzerkMode)
            {
                collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (125 * 0.15f)));
            }
            else
            {
                collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (125 * comboSlowMo)));
            }
            
            Die();
        }
    }

    public void Die()
    {
        if (SlingShot.isHeldDown == false)
        {
            timeManager.Invoke("StopSlowMotion", 0.05f);
        }

        DisableObject();
        GameObject newDeathEffect = (GameObject)Instantiate(moneyBagsDeathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 2);
        Destroy(gameObject, moneyBagsBoxClip.length);
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
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        particleSystem.Stop();
        foreach (SpriteRenderer sr in renderers)
            sr.enabled = false;

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}
