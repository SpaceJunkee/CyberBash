
using UnityEngine;

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

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
        renderers = GetComponentsInChildren<Renderer>();

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
        if(health <= 0)
        {
            GameObject explosion = (GameObject)Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity); ;
            Destroy(explosion, 3f);
            timeManager.StartSlowMotion(0.4f);
            timeManager.Invoke("StopSlowMotion", 0.75f);
            Die();
        }
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

            collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (150)));
            hitSound.Play();
        }

        
    }

    public void Die()
    {
        GameObject.Find("SquareHeadDeathSound").GetComponent<AudioSource>().Play();
        CameraShake.Instance.ShakeCamera(13f, 1.5f);          
        GameObject newDeathEffect = (GameObject)Instantiate(squareHeadDeathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 4);
        SpawnObjects.hasBossBeenKilled = true;
        Destroy(gameObject);
    }
}
