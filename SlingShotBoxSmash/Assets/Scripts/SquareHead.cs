using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareHead : MonoBehaviour
{
    public float health = 100;
    public GameObject squareHeadDeathEffect;
    public GameObject hitEffect;
    public AudioSource hitSound;
    public Animator anim;
   

    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && SlingShot.isHeldDown == false)
        {
           
            anim.speed += 0.18f;
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

    private void StartAnimationAgain()
    {
        anim.StartPlayback();
    }

    public void Die()
    {
        GameObject newDeathEffect = (GameObject)Instantiate(squareHeadDeathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 4);
        SpawnObjects.hasBossBeenKilled = true;
        Destroy(gameObject);
    }
}
