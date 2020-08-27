using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 50f;

    Rigidbody2D rigidbody;

    SlingShot player;
    Vector2 movementDirection;
    public AudioSource audio;
    public GameObject projectileEndEffect;

    private void Start()
    {
        audio.Play();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<SlingShot>();
        
        if(player != null)
        {
            movementDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        }
        
        rigidbody.velocity = new Vector2(movementDirection.x, movementDirection.y);
        Destroy(gameObject, 3.5f);
        Invoke("PlayDeathSound", 3.3f);
        Invoke("PlayDeathEffect", 3.45f);

    }

    private void PlayDeathEffect()
    {
        GameObject newDeathEffect = (GameObject)Instantiate(projectileEndEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 2);
    }
    private void PlayDeathSound()
    {
        
        GameObject.Find("ProjectileEndSound").GetComponent<AudioSource>().Play();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Bomb"))
        {
            Destroy(gameObject);
        }
    }
}
