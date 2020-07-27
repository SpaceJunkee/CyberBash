
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Obstacle : MonoBehaviour
{
    public float upwardForce;
    public GameObject deathEffect;
    public bool hasBeenHit = false;
    public TimeManager timeManager;
    public ScoreDisplay scoreTextPop;
    public GameObject floatingTextPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (gameObject.tag.Equals("DoublePointObstacle"))
            {
                //Trigger floating text if prefab is not null
                if(floatingTextPrefab)
                {
                    ShowFloatingText(10 * ScoreDisplay.scoreMultiplier, new Color32(99,90,3,255));
                }

                ScoreDisplay.score += (5 * 2) * ScoreDisplay.scoreMultiplier;
            }
            else
            {
                ScoreDisplay.score += 5 * ScoreDisplay.scoreMultiplier;
                ShowFloatingText(5 * ScoreDisplay.scoreMultiplier, new Color32(8, 41, 39, 255));
            }
            
            scoreTextPop.scoreText.fontSize = 100;

            if (ScoreDisplay.score % 50 == 0)
            {
                ScoreDisplay.scoreMultiplier++;
                scoreTextPop.scoreMultiplierText.fontSize = 70;
            }

            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();
                  
            collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((-force * vel.magnitude * 125));
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
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(this.gameObject, spawnPosition, Quaternion.identity);
        }
    }

    private void ShowFloatingText(int hitScore, Color32 color)
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text = hitScore.ToString();
        go.GetComponent<TextMesh>().color = color;
    }
       
    
}
