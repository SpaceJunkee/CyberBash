using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 50f;

    Rigidbody2D rigidbody;

    SlingShot player;
    Vector2 movementDirection;

    private void Start()
    {
       
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<SlingShot>();
        
        if(player != null)
        {
            movementDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        }
        
        rigidbody.velocity = new Vector2(movementDirection.x, movementDirection.y);
        Destroy(gameObject, 3.5f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Lava") || collision.gameObject.tag.Equals("Bomb"))
        {
            Destroy(gameObject);
        }
    }
}
