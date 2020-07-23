using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float upwardForce;
    public GameObject deathEffect;
    public bool hasBeenHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector3 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * (upwardForce));         
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((-force * vel.magnitude * 100));
            hasBeenHit = true;
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Lava"))
        {
            Die();
        }
    }

    private void Die()
    {
        SpawnRandomObstacles();
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    private void SpawnRandomObstacles()
    {
        for (int i = 0; i < 2; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height - 200)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(this.gameObject, spawnPosition, Quaternion.identity);
        }
    }
}
