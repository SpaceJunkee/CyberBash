using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenGuy : MonoBehaviour
{
    public GameObject projectile;

    float fireRate;
    float nextFireTime;

    public GameObject greenGuyDeathEffect;
    public ScoreDisplay scoreTextPop;
    public GameObject floatingTextPrefab;
    public AudioSource boxBreakAudio;
    public AudioClip greenGuyBoxBreakClip;
    public GameObject spawnConfiner;
    public GameObject player;
    public TimeManager timeManager;
    public static float comboSlowMo = 1;
    public bool isDead = false;
    public GameObject[] leftOverProjectiles;
    public Animator anim;

    private void Start()
    {
        spawnConfiner = GameObject.FindGameObjectWithTag("Confiner");
        player = GameObject.FindGameObjectWithTag("Player");
        fireRate = 4f;
        nextFireTime = Time.time;
    }

    private void Update()
    {
        CheckIfTimeToFire();
    }

    private void CheckIfTimeToFire()
    {
        if(Time.time > nextFireTime && isDead == false)
        {
            anim.SetBool("Fire", false);
            if (player != null)
            {
                Invoke("SpawnProjectile", 2f);
                
            }
            else
            {
                Restart();
            }
            
            nextFireTime = Time.time + fireRate;
        }
    }

    private void SpawnProjectile()
    {
        anim.SetBool("Fire", true);
        Instantiate(projectile, transform.position, Quaternion.identity);
        
    }

    private void Restart()
    {
        timeManager.StopSlowMotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ScoreDisplay.score = 0;
        ScoreDisplay.scoreMultiplier = 1;
        ScoreDisplay.multiplierGoal = 50;
        DestroyAllProjectiles();

        ComboHandler.ResetValues();
    }

    private void DestroyAllProjectiles()
    {
        leftOverProjectiles = GameObject.FindGameObjectsWithTag("Projectile");

        for(int i = 0; i < leftOverProjectiles.Length; i++)
        {
            Destroy(leftOverProjectiles[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (ScoreDisplay.score >= ScoreDisplay.multiplierGoal)
        {
            ScoreDisplay.scoreMultiplier++;
            scoreTextPop.scoreMultiplierText.fontSize = 70;
            ScoreDisplay.multiplierGoal *= 3;
        }

        if (collision.gameObject.tag.Equals("Bomb"))
        {
            isDead = true;
            Destroy(gameObject);
            ScoreDisplay.score += 10;
        }

        if (collision.gameObject.tag.Equals("Player"))
        {
            isDead = true;
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
            ScoreDisplay.score += ComboHandler.greenGuyScore * ScoreDisplay.scoreMultiplier * ComboHandler.hitCount;
            ShowFloatingText(ComboHandler.greenGuyScore * ScoreDisplay.scoreMultiplier * ComboHandler.hitCount, new Color32(24, 39, 10, 255));

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
        CancelInvoke("SpawnProjectile");

        if (SlingShot.isHeldDown == false)
        {
            timeManager.Invoke("StopSlowMotion", 0.05f);
        }

        DisableObject();
        spawnConfiner.GetComponent<SpawnObjects>().SpawnNormalObstacles(1);
        GameObject newDeathEffect = (GameObject)Instantiate(greenGuyDeathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 2);
        Destroy(gameObject, greenGuyBoxBreakClip.length);
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
        isDead = true;
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
            sr.enabled = false;

        nextFireTime = 100;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<ParticleSystem>().Stop();
    }

    private void IncreaseComboFloatScoreSize()
    {

        if (ComboHandler.hitCount < 3)
        {
            comboSlowMo = 1f;
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 20;
        }
        else if (ComboHandler.hitCount == 3)
        {
            comboSlowMo = 2.25f;
            timeManager.StartSlowMotion(0.2f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 40;
        }
        else if (ComboHandler.hitCount == 4)
        {
            comboSlowMo = 4.5f;
            timeManager.StartSlowMotion(0.1f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 50;
        }
        else if (ComboHandler.hitCount == 5)
        {
            comboSlowMo = 6f;
            timeManager.StartSlowMotion(0.07f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 60;
        }
        else if (ComboHandler.hitCount > 5)
        {
            comboSlowMo = 6.75f;
            timeManager.StartSlowMotion(0.05f);
            floatingTextPrefab.GetComponent<TextMesh>().fontSize = 70;
        }

    }
}
