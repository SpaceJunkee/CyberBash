using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareHead : MonoBehaviour
{
    public int health = 100;

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
           
            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            health -= 25;

            collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * (125)));
                
        }
    }

    public void Die()
    {
        SpawnObjects.hasBossBeenKilled = true;
        Destroy(gameObject);
    }
}
