
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayerProjectiles : MonoBehaviour
{
    public int numberOfProjectiles;

    public GameObject orbBullet;

    Vector2 startPoint;
    private bool isSpawning;
    public float radius, moveSpeed;
    public float minWait;
    public float maxWait;

    public static bool hasPlayerProjectileBeenBought = false;


    private void Start()
    {
        isSpawning = false;
        radius = 5f;
    }

    private void Update()
    {
        if (!isSpawning && hasPlayerProjectileBeenBought)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnObject", timer);
            isSpawning = true;
        }
    }

    void SpawnObject()
    {
        startPoint = this.transform.position;
        SpawnProjectiles(numberOfProjectiles);
        isSpawning = false;
    }

    void SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMovementDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var projectileInstantite = Instantiate(orbBullet, startPoint, Quaternion.identity);
            projectileInstantite.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMovementDirection.x, projectileMovementDirection.y);

            angle += angleStep;
        }
        
    }
}
    

