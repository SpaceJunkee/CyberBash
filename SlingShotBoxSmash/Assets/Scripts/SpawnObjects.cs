using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public TimeManager timeManager;

    public Vector2 center;
    public Vector2 size;

    public int doublePointSpawnMultiplier = 1;
    public int deathBoxSpawnMultiplier = 1;
    public int bombSpawnMultiplier = 1;

    public GameObject normalObstaclePrefab;
    public GameObject doublePointPrefab;
    public GameObject deathBoxPrefab;
    public GameObject bombPrefab;
    private static bool hasBombGoneOff = false;

    public void Start()
    {
        SpawnNormalObstacles(10);
        SpawnDoublePointObstacles(3);
    }

    private void Update()
    {
        if(hasBombGoneOff)
        {
            hasBombGoneOff = false;
            Invoke("SpawnNormalObstaclesAfterBomb", 3f);
        }

        if(ScoreDisplay.score >= 2751 * bombSpawnMultiplier)
        {
            hasBombGoneOff = true;
            bombSpawnMultiplier++;
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
    }
    public void SpawnNormalObstacles(int spawnRate)
    {
        hasBombGoneOff = false;
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

        CameraShake.Instance.ShakeCamera(25f, 1f);
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(bombPrefab, position, Quaternion.identity);
            GameObject newBomb = (GameObject)Instantiate(bombPrefab, position, Quaternion.identity); ;
            Destroy(newBomb, 1);
        }

    }

    public void SpawnDeathBoxObstacles(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(deathBoxPrefab, position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,0,0.5f);
        Gizmos.DrawCube(center, size);
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
