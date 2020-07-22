using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float upwardForce;
    public GameObject deathEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector3 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * (upwardForce));         
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((-force * vel.magnitude * 100));

            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}
