
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{
    public TimeManager timeManager;
    public ShieldWallBounce[] shieldWalls;

    public Vector2 center;
    public Vector2 size;

    //LeftOvers
    GameObject[] leftOverGreens;
    GameObject[] leftOverNormals;
    GameObject[] leftOverDoubles;
    GameObject[] leftOverDeathBoxes;

    //Boss conditions
    public static bool hasBossBeenKilled = false;

    public int doublePointSpawnMultiplier = 1;
    public int deathBoxSpawnMultiplier = 1;
    public int bombSpawnMultiplier = 1;
    public float greenGuySpawnMultiplier = 1f;
    public float bombPointGoal = 1000;
    public float squareHeadGoal = 5500;
    public float squareHeadMultiplier = 1f;
    public int greenGuyCount = 1;
    public static bool hasFirstBombGoneOff = false;
    public static bool hasBombGoBoom = false;

    public GameObject normalObstaclePrefab;
    public GameObject doublePointPrefab;
    public GameObject deathBoxPrefab;
    public GameObject bombPrefab;
    public GameObject bossBombPrefab;
    public GameObject greenGuyPrefab;
    public GameObject squareHeadPrefab;

    public Text bombGoalText;
    public Text bossGoalText;
    public Text incomingBombText;
    public Text incomingBossText;

    public AudioClip bossTrack;
    public AudioClip originalTrack;
    GameObject audio;
    GameObject audioBoss;

    public void Start()
    {
        audio = GameObject.Find("Music");
        audioBoss = GameObject.Find("BossMusic");
        SpawnNormalObstacles(10);
        SpawnDoublePointObstacles(3);
    }

    private void Update()
    {

        ShowWarningText();

        if (hasBossBeenKilled == true)
        {
            PlayerPrefs.SetInt("SpecialCurrency", PlayerPrefs.GetInt("SpecialCurrency") + 1);
            Invoke("StopBossMusic", 3f);
            Invoke("SpawnNormalObstaclesAfterBomb", 2.5f);
            hasBossBeenKilled = false;
        }

        if(ScoreDisplay.score >= (squareHeadGoal * squareHeadMultiplier))
        {
            squareHeadMultiplier++;
            SpawnSquareHead(1);
        }

        if (ScoreDisplay.score >= bombPointGoal * bombSpawnMultiplier)
        {
            hasFirstBombGoneOff = true;
            hasBombGoBoom = true;
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

        
        if(ScoreDisplay.score >= 400 * deathBoxSpawnMultiplier)
        {
            deathBoxSpawnMultiplier++;
            SpawnDeathBoxObstacles(1);
        }

        if(ScoreDisplay.score >= 475 * greenGuySpawnMultiplier && hasFirstBombGoneOff)
        {
            
            greenGuySpawnMultiplier++;
            
            SpawnGreenGuy(greenGuyCount);
        }
    }

    public void StopBossMusic()
    {
        audioBoss.GetComponent<AudioSource>().Stop();
        audio.GetComponent<AudioSource>().Play();
    }

    public void SpawnNormalObstacles(int spawnRate)
    {
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(normalObstaclePrefab, position, Quaternion.identity);
        }
        
    }

    public void SpawnSquareHead(int spawnRate)
    {
        audio.GetComponent<AudioSource>().Pause();
        audioBoss.GetComponent<AudioSource>().clip = bossTrack;
        audioBoss.GetComponent<AudioSource>().Play();

        DisableIncomingText(incomingBossText);
        Invoke("DestroyLeftOvers", 3f);

        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = center;
            Instantiate(squareHeadPrefab, position, Quaternion.identity);
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

    private void DestroyLeftOvers()
    {
        leftOverGreens = GameObject.FindGameObjectsWithTag("GreenGuy");
        leftOverNormals = GameObject.FindGameObjectsWithTag("NormalObstacle");
        leftOverDoubles = GameObject.FindGameObjectsWithTag("DoublePointObstacle");
        leftOverDeathBoxes = GameObject.FindGameObjectsWithTag("DeathBox");
        CameraShake.Instance.ShakeCamera(25f, 1.2f);

        for (int i = 0; i < 1; i++)
        {
            timeManager.StartSlowMotion(0.25f);
            
            Vector2 position = center;
            GameObject newBomb = (GameObject)Instantiate(bossBombPrefab, position, Quaternion.identity); ;          
            Destroy(newBomb, 2f);
        }

        timeManager.Invoke("StopSlowMotion", 0.15f);


        for (int i = 0; i < leftOverGreens.Length; i++)
        {
            Destroy(leftOverGreens[i]);
        }

        for (int i = 0; i < leftOverNormals.Length; i++)
        {
            Destroy(leftOverNormals[i]);
        }

        for (int i = 0; i < leftOverDoubles.Length; i++)
        {
            Destroy(leftOverDoubles[i]);
        }

        for (int i = 0; i < leftOverDeathBoxes.Length; i++)
        {
            Destroy(leftOverDeathBoxes[i]);
        }
    }

    public void SpawnBomb(int spawnRate)
    {
        foreach(ShieldWallBounce shield in shieldWalls)
        {
            shield.ResetShields();
        }

        timeManager.StartSlowMotion(0.05f);
        timeManager.Invoke("StopSlowMotion", 2f);
        DisableIncomingText(incomingBombText);
        CameraShake.Instance.ShakeCamera(25f, 1f);
        for (int i = 0; i < spawnRate; i++)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2)); ;
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

    private void DisableIncomingText(Text incomingText)
    {
        incomingText.enabled = false;
    }

    private void SpawnNormalObstaclesAfterBomb()
    {
        hasBombGoBoom = false;
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

    private void ShowWarningText()
    {

        bombGoalText.text = $"NEXT EMP: {bombPointGoal * bombSpawnMultiplier}";
        bossGoalText.text = $"NEXT BOSS: {squareHeadGoal * squareHeadMultiplier}";

        if (ScoreDisplay.score >= (bombPointGoal * bombSpawnMultiplier) - 150 && ScoreDisplay.score < (bombPointGoal * bombSpawnMultiplier))
        {
            incomingBombText.enabled = true;
        }

        if (ScoreDisplay.score >= (squareHeadGoal * squareHeadMultiplier) - 1000 && ScoreDisplay.score < (squareHeadGoal * squareHeadMultiplier))
        {
            incomingBossText.enabled = true;
        }
    }
}
