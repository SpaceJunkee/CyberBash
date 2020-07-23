using Cinemachine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public float upwardForce;
    public GameObject deathEffect;
    public bool hasBeenHit = false;
    public TimeManager timeManager;
    public ScoreDisplay scoreTextPop;

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            ScoreDisplay.score += 5 * ScoreDisplay.scoreMultiplier;

            scoreTextPop.scoreText.fontSize = 70;

            if (ScoreDisplay.score % 50 == 0)
            {
                ScoreDisplay.scoreMultiplier++;
                scoreTextPop.scoreMultiplierText.fontSize = 50;
            }

            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * (upwardForce));         
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((-force * vel.magnitude * 100));
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Lava"))
        {
            Die();
        }

        if (collision.gameObject.tag.Equals("Player"))
        {
            
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

        if (ScoreDisplay.score % 25 == 0)
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
