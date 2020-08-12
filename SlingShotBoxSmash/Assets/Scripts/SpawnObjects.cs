
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{
    public TimeManager timeManager;

    public Vector2 center;
    public Vector2 size;

    public int doublePointSpawnMultiplier = 1;
    public int deathBoxSpawnMultiplier = 1;
    public int bombSpawnMultiplier = 1;
    public float greenGuySpawnMultiplier = 1.2f;
    public float bombPointGoal = 1000;
    public int greenGuyCount = 1;
    public static bool hasBombGoneOff = false;

    public GameObject normalObstaclePrefab;
    public GameObject doublePointPrefab;
    public GameObject deathBoxPrefab;
    public GameObject bombPrefab;
    public GameObject greenGuyPrefab;

    public Text bombGoalText;
    public Text incomingBombText;

    public void Start()
    {
        SpawnNormalObstacles(10);
        SpawnDoublePointObstacles(3);
    }

    private void Update()
    {
        bombGoalText.text = $"NEXT BOMB: {bombPointGoal * bombSpawnMultiplier}";

        if(ScoreDisplay.score >= (bombPointGoal * bombSpawnMultiplier) - 150 && ScoreDisplay.score < (bombPointGoal * bombSpawnMultiplier))
        {
            incomingBombText.enabled = true;
        }

        if (ScoreDisplay.score >= bombPointGoal * bombSpawnMultiplier)
        {
            hasBombGoneOff = true;
            bombSpawnMultiplier++;
            bombPointGoal += 250;
            SpawnBomb(1);
        }

        
        if (ScoreDisplay.score >= 225 * doublePointSpawnMultiplier)
        {
            doublePointSpawnMultiplier++;
            SpawnDoublePointObstacles(2);
            SpawnNormalObstacles(1);
        }

        
        if(ScoreDisplay.score >= 300 * deathBoxSpawnMultiplier)
        {
            deathBoxSpawnMultiplier++;
            SpawnDeathBoxObstacles(1);
        }

        if(ScoreDisplay.score >= 600 * greenGuySpawnMultiplier && hasBombGoneOff)
        {
            
            greenGuySpawnMultiplier++;
            
            SpawnGreenGuy(greenGuyCount);
        }
    }
    public void SpawnNormalObstacles(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(normalObstaclePrefab, position, Quaternion.identity);
        }
        
    }

    public void SpawnDoublePointObstacles(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(doublePointPrefab, position, Quaternion.identity);
        }

    }

    public void SpawnBomb(int spawnRate)
    {
        ScoreDisplay.scoreMultiplier += 1;
        DisableIncomingBombText();
        CameraShake.Instance.ShakeCamera(25f, 1f);
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2)); ;
            Instantiate(bombPrefab, position, Quaternion.identity);
            GameObject newBomb = (GameObject)Instantiate(bombPrefab, position, Quaternion.identity); ;
            Destroy(newBomb, 2f);
        }

        Invoke("SpawnNormalObstaclesAfterBomb", 2.35f);

    }

    public void SpawnDeathBoxObstacles(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(deathBoxPrefab, position, Quaternion.identity);
        }
    }

    public void SpawnGreenGuy(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(greenGuyPrefab, position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,0,0.5f);
        Gizmos.DrawCube(center, size);
    }

    private void DisableIncomingBombText()
    {
        incomingBombText.enabled = false;
    }

    private void SpawnNormalObstaclesAfterBomb()
    {
        
        //Normal obstacles
        for (int i = 0; i < 10; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(normalObstaclePrefab, position, Quaternion.identity);
        }

        //Double Obstacles
        for (int i = 0; i < 3; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(doublePointPrefab, position, Quaternion.identity);
        }

        
    }
}
