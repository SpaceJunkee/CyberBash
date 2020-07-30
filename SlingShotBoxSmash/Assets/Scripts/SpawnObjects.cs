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

    public GameObject normalObstaclePrefab;
    public GameObject doublePointPrefab;
    public GameObject deathBoxPrefab;


    public void Start()
    {
        SpawnNormalObstacles(8);
        SpawnDoublePointObstacles(3);
    }

    private void Update()
    {
        if(ScoreDisplay.score >= 75 * doublePointSpawnMultiplier)
        {
            doublePointSpawnMultiplier++;
            SpawnDoublePointObstacles(1);
        }

        
        if(ScoreDisplay.score >= 200 * deathBoxSpawnMultiplier)
        {
            deathBoxSpawnMultiplier++;
            SpawnDeathBoxObstacles(1);
        }
    }
    public void SpawnNormalObstacles(int spawnRate)
    {
        for(int i = 0; i < spawnRate; i++)
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
}
