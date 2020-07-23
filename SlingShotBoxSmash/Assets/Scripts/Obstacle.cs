using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Obstacle : MonoBehaviour
{
    public float upwardForce;
    public GameObject deathEffect;
    public bool hasBeenHit = false;
    public int score = 0;
    public TimeManager timeManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            CameraShake.Instance.ShakeCamera(13f, 0.2f);
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
        score++;
        SpawnRandomObstacles();
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    private void SpawnRandomObstacles()
    {

        if (score % 3 == 0)
        {
            HandleSpawn(2);
        }
        else
        {
            HandleSpawn(1);
        }
        
    }

    private void HandleSpawn(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
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
