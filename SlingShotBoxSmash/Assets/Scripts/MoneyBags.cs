using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            boxBreakAudio.Play();
           
            ShowFloatingText(moneyBagsValue, new Color32(255, 0, 194, 255));

            scoreTextPop.scoreText.fontSize = 100;

            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (125 * comboSlowMo)));
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
        IncreaseComboFloatScoreSize();
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

    private void IncreaseComboFloatScoreSize()
    {

        if (ComboHandler.hitCount < 2)
        {
            boxBreakAudio.clip = combo1SoundNormal;
            boxBreakAudio.Play();
            comboSlowMo = 1f;
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 20;
        }
        else if (ComboHandler.hitCount == 2)
        {
            boxBreakAudio.clip = combo2SoundNormal;
            boxBreakAudio.Play();
            comboSlowMo = 1f;
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 30;
        }
        else if (ComboHandler.hitCount == 3)
        {
            boxBreakAudio.clip = combo3SoundNormal;
            boxBreakAudio.Play();
            comboSlowMo = 2.25f;
            timeManager.StartSlowMotion(0.2f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 40;
        }
        else if (ComboHandler.hitCount == 4)
        {
            boxBreakAudio.clip = combo4SoundNormal;
            boxBreakAudio.Play();
            comboSlowMo = 4.5f;
            timeManager.StartSlowMotion(0.1f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 50;
        }
        else if (ComboHandler.hitCount == 5)
        {
            boxBreakAudio.clip = combo5SoundNormal;
            boxBreakAudio.Play();
            comboSlowMo = 6f;
            timeManager.StartSlowMotion(0.07f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 60;
        }
        else if (ComboHandler.hitCount >= 6)
        {
            boxBreakAudio.clip = combo5SoundNormal;
            boxBreakAudio.Play();
            comboSlowMo = 6.75f;
            timeManager.StartSlowMotion(0.05f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 70;
        }


    }
}
