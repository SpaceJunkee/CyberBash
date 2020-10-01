
using UnityEngine;
using UnityEngine.UI;

public class SquareHead : MonoBehaviour
{
    public float health = 100;
    public GameObject squareHeadDeathEffect;
    public GameObject hitEffect;
    public AudioSource hitSound;
    public Animator anim;
    Renderer[] renderers;
    public TimeManager timeManager;
    public GameObject explosionCirclePrefab;
    public GameObject floatingTextPrefab;
    public GameObject bossHealthBar;

    private void Start()
    {

        bossHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar");
        GameObject.Find("Border").GetComponent<Image>().enabled = true;
        GameObject.Find("Health").GetComponent<Image>().enabled = true;

        bossHealthBar.GetComponent<BossHealthBar>().SetMaxHealth(health);

        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
        renderers = GetComponentsInChildren<Renderer>();

        Invoke("PlayEnterSound", 1f);
        Invoke("PlayLaserSound", 4.8f);
        Invoke("PlayRotateLoop", 5f);

        CameraShake.Instance.ShakeCamera(10f, 5f);

        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject.tag.Equals("HitPoint"))
            {
                continue;
            }
            renderer.material.color = new Color32(255, 0, 0, 255);
        }
    }

    private void Flash()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject.tag.Equals("HitPoint"))
            {
                continue;
            }
            renderer.material.color = new Color32(0, 245, 255, 255);
        }
        
        Invoke("ResetColor", 0.1f);
    }

    private void ResetColor()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject.tag.Equals("HitPoint"))
            {
                continue;
            }
            renderer.material.color = new Color32(255, 0, 0, 255);
        }
        
    }

    private void Update()
    {
        if (health <= 0)
        {
            GameObject.Find("SquareHeadRotateSound").GetComponent<AudioSource>().Stop();
            GameObject explosion = (GameObject)Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity); ;
            Destroy(explosion, 3f);
            timeManager.StartSlowMotion(0.4f);
            timeManager.Invoke("StopSlowMotion", 0.75f);
            ShowFloatingText(1000, new Color32(37, 0, 41, 255));
            GameObject.Find("Border").GetComponent<Image>().enabled = false;
            GameObject.Find("Health").GetComponent<Image>().enabled = false;
            Die();
        }
    }

    private void ShowFloatingText(int hitScore, Color32 color)
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text = hitScore.ToString();
        go.GetComponent<TextMesh>().fontSize = 75;
        go.GetComponent<TextMesh>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && SlingShot.isHeldDown == false)
        {
            Flash();
            anim.speed += 0.165f;
            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            GameObject newDeathEffect = (GameObject)Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
            Destroy(newDeathEffect, 2);

            health -= 12.5f;
            bossHealthBar.GetComponent<BossHealthBar>().SetHealth(health);

            collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (150)));
            hitSound.Play();
        }
        
    }

    private void PlayRotateLoop()
    {
        GameObject.Find("SquareHeadRotateSound").GetComponent<AudioSource>().Play();
    }

    private void PlayLaserSound()
    {
        GameObject.Find("SquareHeadLaserSound").GetComponent<AudioSource>().Play();
    }
    

    private void PlayEnterSound()
    {
        GameObject.Find("SquareHeadEnterSound").GetComponent<AudioSource>().Play();
    }

    public void Die()
    {
        GameObject.Find("SquareHeadRotateSound").GetComponent<AudioSource>().Stop();
        GameObject.Find("SquareHeadDeathSound").GetComponent<AudioSource>().Play();
        DeathRestart.bossScoreMoney += 1000;
        CameraShake.Instance.ShakeCamera(13f, 1.5f);          
        GameObject newDeathEffect = (GameObject)Instantiate(squareHeadDeathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 4);
        SpawnObjects.hasBossBeenKilled = true;
        Destroy(gameObject);
    }
}
